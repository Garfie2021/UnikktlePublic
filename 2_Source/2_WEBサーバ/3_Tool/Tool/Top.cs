using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace Tool
{
    public partial class Top : Form
    {
        public Top()
        {
            InitializeComponent();
        }

        private void BtnHttpPost_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show("開始しました");

                //string url = "http://xxx:60003/PayPalIPN/Receive";
                string url = "http://xxx/PayPalIPN/Receive";


                //文字コードを指定する
                //var enc = Encoding.UTF8;

                //POST送信する文字列を作成
                //string postData = "id=1&word=" + HttpUtility.UrlEncode("インターネット", enc);

                var wc = new WebClient
                {
                    //文字コードを指定する
                    Encoding = Encoding.UTF8
                };

                //ヘッダにContent-Typeを加える
                wc.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                //データを送信し、また受信する
                string resText = wc.UploadString(url, "mc_gross=1000&protection_eligibility=Eligible&address_status=confirmed&payer_id=xxx&address_street=Nishi+4-chome%2C+Kita+55-jo%2C+Kita-ku&payment_date=06%3A59%3A45+Aug+15%2C+2019+PDT&payment_status=Completed&charset=Shift_JIS&address_zip=xxx&first_name=test&option_selection1=1000+Credit&mc_fee=76&address_country_code=JP&address_name=buyer+test&notify_version=3.9&custom=&payer_status=verified&business=xxx%40xxx&address_country=Japan&address_city=xxx&quantity=1&verify_sign=xxx&payer_email=xxx%40xxx&option_name1=Credit&txn_id=xxx&payment_type=instant&last_name=buyer&address_state=xxx&receiver_email=xxx%40xxx&payment_fee=&shipping_discount=0&insurance_amount=0&receiver_id=xxx&txn_type=web_accept&item_name=Credit&discount=0&mc_currency=JPY&item_number=xxx&residence_country=JP&test_ipn=1&shipping_method=Default&transaction_subject=&payment_gross=&ipn_track_id=xxx");
                wc.Dispose();

                //受信したデータを表示する
                Console.WriteLine(resText);

                //var form = new TestWebPost();
                //form.ShowDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnHTMLデコード_Click(object sender, EventArgs e)
        {
            try
            {
                string before;
                string after;


                before = "xxx%40xxx";


                var encoding = Encoding.GetEncoding("Shift_JIS");

                after = HttpUtility.HtmlDecode(before);


                after = HttpUtility.UrlDecode(before, encoding);




                string myEncodedString = HttpUtility.HtmlEncode("");
                string urlEnc = HttpUtility.UrlEncode("");

                before = "xxx";
                after = HttpUtility.UrlDecode(before, Encoding.GetEncoding("Shift_JIS"));




                //after = HttpUtility.HtmlDecode(before.Replace('&', '\n'));
                //after = HttpUtility.UrlDecode(after);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnHTMLデコード_IPN即時通知_Click(object sender, EventArgs e)
        {
            try
            {
                var before = "mc_gross=1000&protection_eligibility=Eligible&address_status=confirmed&payer_id=GG33K42VH2X2S&address_street=Nishi+4-chome%2C+Kita+55-jo%2C+Kita-ku&payment_date=05%3A49%3A24+Aug+23%2C+2019+PDT&payment_status=Completed&charset=Shift_JIS&address_zip=xxx&first_name=xxx&option_selection1=1000+Credit&mc_fee=76&address_country_code=JP&address_name=%8D%B2%93%A1+xxx&notify_version=3.9&custom=&payer_status=verified&business=xxx%40xxx&address_country=Japan&address_city=xxx&quantity=1&verify_sign=A05WP2oX1s0Fm5iMzcHYC5mvySGjAoBxjmf1.1TIY-rAl3sXhgQkX54Q&payer_email=xxx%40xxx&option_name1=Credit&txn_id=9GS413983Y164064V&payment_type=instant&last_name=%8D%B2%93%A1&address_state=xxx&receiver_email=xxx%40xxx&payment_fee=&shipping_discount=0&insurance_amount=0&receiver_id=xxx&txn_type=web_accept&item_name=Credit&discount=0&mc_currency=JPY&item_number=xxx&residence_country=JP&test_ipn=1&shipping_method=Default&transaction_subject=&payment_gross=&ipn_track_id=de459dacf618";

                before = before.Replace('&', '\n');

                // charset=Shift_JIS を切り出す
                var charsetIndex = before.IndexOf("charset=");
                var startIndex = before.IndexOf("=", charsetIndex) + 1;
                var endIndex = before.IndexOf("\n", startIndex);
                var encode = before.Substring(startIndex, endIndex - startIndex);

                var after = HttpUtility.UrlDecode(before, Encoding.GetEncoding(encode));

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
