using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Text;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Identity.UI.Services;
using Unikktle.Common;
using PayPalListen.Common;
using PayPalListen.Data;
using PayPalListen.Models;


namespace PayPalListen.Common
{
    public static class PayPalCommon
    {
        public static void VerifyTask(ApplicationDbContext dbContext, IEmailSender emailSender, 
            PayPalIPNContext ipnContext)
        {
            var verificationRequest = WebRequest.Create("https://www.sandbox.paypal.com/cgi-bin/webscr");

            //Set values for the verification request
            verificationRequest.Method = "POST";
            verificationRequest.ContentType = "application/x-www-form-urlencoded";

            //Add cmd=_notify-validate to the payload
            var strRequest = "cmd=_notify-validate&" + ipnContext.RequestBody;
            verificationRequest.ContentLength = strRequest.Length;

            //Attach payload to the verification request
            using (var writer = new StreamWriter(verificationRequest.GetRequestStream(), Encoding.ASCII))
            {
                writer.Write(strRequest);
            }

            //Send the request to PayPal and get the response
            using (var reader = new StreamReader(verificationRequest.GetResponse().GetResponseStream()))
            {
                ipnContext.Verification = reader.ReadToEnd();
            }

            if (!ipnContext.Verification.Equals("VERIFIED"))
            {
                // PayPal側で検証エラー。

                //if (ipnContext.Verification.Equals("INVALID"))
                //{
                //    // 無効。Log for manual investigation
                //    //_emailSender.SendEmailAsync("xxx@xxx.com", "[Unikktle] PayPalIPN/Receive INVALID", decode);
                //}
                //else
                //{
                //    // 検証失敗。Log error
                //    //_emailSender.SendEmailAsync("xxx@xxx.com", "[Unikktle] PayPalIPN/Receive Error", decode);
                //}

                return;
            }

            ProcessVerificationResponse(dbContext, emailSender, ipnContext.RequestBody, 
                out string payer_email);
        }

        private static PayPalIpnStatus ProcessVerificationResponse(ApplicationDbContext dbContext,
            IEmailSender emailSender, string RequestBody, out string payer_email)
        {

            // PayPal側で検証成功。

            var now = DateTime.Now;

            // 変換に失敗する可能性を考慮して、変換前の値をDBに登録しておく。
            var PayPalIPNlogNo = SP_PayPalIPNlog.Insert(dbContext, now, RequestBody);

            // 変換実行
            var convert = PayPalCommon.Decoding(RequestBody);

            payer_email = convert.payer_email;

            if (convert.item_number != "xxx")
            {
                // Unikktleと関係が無い通知はスキップ
                return PayPalIpnStatus.Excluded;
            }

            if (convert.receiver_email != "xxx@xxx.com" &&
                convert.receiver_email != "paypal@xxx" &&
                convert.receiver_email != "xxx@xxx.com")
            {
                // Unikktleと関係が無い通知はスキップ
                // Receiver_emailがプライマリPayPalメールであることを確認します
                return PayPalIpnStatus.Excluded;
            }

            if (convert.mc_currency != "JPY")
            {
                // Payment_currencyが正しくない
                return PayPalIpnStatus.Excluded;
            }

            if (convert.payment_status != "Completed")
            {
                // Payment_status = Completedではない
                return PayPalIpnStatus.Excluded;
            }


            // Payment_amount が正しいことを確認します ※保留


            // Txn_idが以前に処理されていないことを確認します
            if (SP_PayPalIPN.GetCount(dbContext, convert) > 0)
            {
                return PayPalIpnStatus.Excluded;
            }

            // 列分割した値をDBに登録
            var payPalIpnNo = SP_PayPalIPN.Insert(dbContext, PayPalIPNlogNo, now, convert);

            var userNo = SP_AspNetUsers.GetNo_FromEmail(dbContext, convert.payer_email);

            if (userNo == null)
            {
                emailSender.SendEmailAsync(payer_email,
                    "[Unikktle] クレジット登録に失敗しました",
                    "クレジットの購入手続きが失敗しました。<br />PayPalにログインし購入を返品した後、UnikktleとPayPalに登録しているメールアドレスが正しいことを確認後、クレジット購入をやり直して下さい。");

                return PayPalIpnStatus.UserNotFound;
            }

            // ユーザの所有クレジットに反映
            SP_CreditHistory.Insert(dbContext, (long)userNo, payPalIpnNo,
                now, convert.txn_id, int.Parse(convert.mc_gross));

            emailSender.SendEmailAsync(payer_email,
                "[Unikktle] クレジット購入手続完了",
                $"お支払いいただき、ありがとうございました。< br /> {convert.mc_gross} クレジットの購入手続きが完了しました。< br /> 取引詳細を表示するには、PayPalアカウントにログインしてください。");

            return PayPalIpnStatus.Completed;
        }



        public static PayPalIPN Decoding(string before)
        {
            // 文字コードを切り出す（charset=Shift_JIS ）
            var charsetIndex = before.IndexOf("charset=");
            var startIndex = before.IndexOf('=', charsetIndex) + 1;
            var endIndex = before.IndexOf('&', startIndex);
            var encode = before.Substring(startIndex, endIndex - startIndex);
            var encoding = Encoding.GetEncoding(encode);

            return Convert(before, encoding);
        }


        private static PayPalIPN Convert(string decode, Encoding encoding)
        {
            var payPalIPN = new PayPalIPN();

            // デコードして記号が復活する前に、区切り文字（&）で分割しておく
            foreach (var row in decode.Split('&'))
            {
                // デコードして記号が復活する前に、区切り文字（=）で分割しておく
                var item = row.Split('=');

                switch (item[0])
                {
                    case "mc_gross":
                        payPalIPN.mc_gross = HttpUtility.UrlDecode(item[1], encoding);
                        break;
                    case "protection_eligibility":
                        payPalIPN.protection_eligibility = HttpUtility.UrlDecode(item[1], encoding);
                        break;
                    case "address_status":
                        payPalIPN.address_status = HttpUtility.UrlDecode(item[1], encoding);
                        break;
                    case "payer_id":
                        payPalIPN.payer_id = HttpUtility.UrlDecode(item[1], encoding);
                        break;
                    case "address_street":
                        payPalIPN.address_street = HttpUtility.UrlDecode(item[1], encoding);
                        break;
                    case "payment_date":
                        payPalIPN.payment_date = HttpUtility.UrlDecode(item[1], encoding);
                        break;
                    case "payment_status":
                        payPalIPN.payment_status = HttpUtility.UrlDecode(item[1], encoding);
                        break;
                    case "charset":
                        payPalIPN.charset = HttpUtility.UrlDecode(item[1], encoding);
                        break;
                    case "address_zip":
                        payPalIPN.address_zip = HttpUtility.UrlDecode(item[1], encoding);
                        break;
                    case "first_name":
                        payPalIPN.first_name = HttpUtility.UrlDecode(item[1], encoding);
                        break;
                    case "option_selection1":
                        payPalIPN.option_selection1 = HttpUtility.UrlDecode(item[1], encoding);
                        break;
                    case "mc_fee":
                        payPalIPN.mc_fee = HttpUtility.UrlDecode(item[1], encoding);
                        break;
                    case "address_country_code":
                        payPalIPN.address_country_code = HttpUtility.UrlDecode(item[1], encoding);
                        break;
                    case "address_name":
                        payPalIPN.address_name = HttpUtility.UrlDecode(item[1], encoding);
                        break;
                    case "notify_version":
                        payPalIPN.notify_version = HttpUtility.UrlDecode(item[1], encoding);
                        break;
                    case "custom":
                        payPalIPN.custom = HttpUtility.UrlDecode(item[1], encoding);
                        break;
                    case "payer_status":
                        payPalIPN.payer_status = HttpUtility.UrlDecode(item[1], encoding);
                        break;
                    case "business":
                        payPalIPN.business = HttpUtility.UrlDecode(item[1], encoding);
                        break;
                    case "address_country":
                        payPalIPN.address_country = HttpUtility.UrlDecode(item[1], encoding);
                        break;
                    case "address_city":
                        payPalIPN.address_city = HttpUtility.UrlDecode(item[1], encoding);
                        break;
                    case "quantity":
                        payPalIPN.quantity = HttpUtility.UrlDecode(item[1], encoding);
                        break;
                    case "verify_sign":
                        payPalIPN.verify_sign = HttpUtility.UrlDecode(item[1], encoding);
                        break;
                    case "payer_email":
                        payPalIPN.payer_email = HttpUtility.UrlDecode(item[1], encoding);
                        break;
                    case "option_name1":
                        payPalIPN.option_name1 = HttpUtility.UrlDecode(item[1], encoding);
                        break;
                    case "txn_id":
                        payPalIPN.txn_id = HttpUtility.UrlDecode(item[1], encoding);
                        break;
                    case "payment_type":
                        payPalIPN.payment_type = HttpUtility.UrlDecode(item[1], encoding);
                        break;
                    case "last_name":
                        payPalIPN.last_name = HttpUtility.UrlDecode(item[1], encoding);
                        break;
                    case "address_state":
                        payPalIPN.address_state = HttpUtility.UrlDecode(item[1], encoding);
                        break;
                    case "receiver_email":
                        payPalIPN.receiver_email = HttpUtility.UrlDecode(item[1], encoding);
                        break;
                    case "payment_fee":
                        payPalIPN.payment_fee = HttpUtility.UrlDecode(item[1], encoding);
                        break;
                    case "shipping_discount":
                        payPalIPN.shipping_discount = HttpUtility.UrlDecode(item[1], encoding);
                        break;
                    case "insurance_amount":
                        payPalIPN.insurance_amount = HttpUtility.UrlDecode(item[1], encoding);
                        break;
                    case "receiver_id":
                        payPalIPN.receiver_id = HttpUtility.UrlDecode(item[1], encoding);
                        break;
                    case "txn_type":
                        payPalIPN.txn_type = HttpUtility.UrlDecode(item[1], encoding);
                        break;
                    case "item_name":
                        payPalIPN.item_name = HttpUtility.UrlDecode(item[1], encoding);
                        break;
                    case "discount":
                        payPalIPN.discount = HttpUtility.UrlDecode(item[1], encoding);
                        break;
                    case "mc_currency":
                        payPalIPN.mc_currency = HttpUtility.UrlDecode(item[1], encoding);
                        break;
                    case "item_number":
                        payPalIPN.item_number = HttpUtility.UrlDecode(item[1], encoding);
                        break;
                    case "residence_country":
                        payPalIPN.residence_country = HttpUtility.UrlDecode(item[1], encoding);
                        break;
                    case "test_ipn":
                        payPalIPN.test_ipn = HttpUtility.UrlDecode(item[1], encoding);
                        break;
                    case "shipping_method":
                        payPalIPN.shipping_method = HttpUtility.UrlDecode(item[1], encoding);
                        break;
                    case "transaction_subject":
                        payPalIPN.transaction_subject = HttpUtility.UrlDecode(item[1], encoding);
                        break;
                    case "payment_gross":
                        payPalIPN.payment_gross = HttpUtility.UrlDecode(item[1], encoding);
                        break;
                    case "ipn_track_id":
                        payPalIPN.ipn_track_id = HttpUtility.UrlDecode(item[1], encoding);
                        break;
                }
            }

            return payPalIPN;

        }

    }
}
