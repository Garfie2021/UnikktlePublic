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


namespace PayPalListen.Models
{
    public class PayPalIPNContext
    {
        public HttpRequest IPNRequest { get; set; }

        public string RequestBody { get; set; }

        public string Verification { get; set; } = String.Empty;
    }


    // PayPal住所無し（「顧客の配送先住所が必要ですか： いいえ）対応で、デフォルト値をnullではなく、空文字列にする必要がある。
    public class PayPalIPN
    {
        public string mc_gross = "";                 // サンプルデータ > 1000
        public string protection_eligibility = "";   // サンプルデータ > Eligible
        public string address_status = "";           // サンプルデータ > confirmed
        public string payer_id = "";                 // サンプルデータ > xxx
        public string address_street = "";           // サンプルデータ > xxx
        public string payment_date = "";             // サンプルデータ > 06:59:45 Aug 15, 2019 PDT
        public string payment_status = "";           // サンプルデータ > Completed
        public string charset = "";                  // サンプルデータ > Shift_JIS
        public string address_zip = "";              // サンプルデータ > xxx
        public string first_name = "";               // サンプルデータ > test
        public string option_selection1 = "";        // サンプルデータ > 1000 Credit
        public string mc_fee = "";                   // サンプルデータ > 76
        public string address_country_code = "";     // サンプルデータ > JP
        public string address_name = "";             // サンプルデータ > buyer test
        public string notify_version = "";           // サンプルデータ > 3.9
        public string custom = "";                   // サンプルデータ > 
        public string payer_status = "";             // サンプルデータ > verified
        public string business = "";                 // サンプルデータ > xxx @xxx.com

        public string address_country = "";          // サンプルデータ > Japan
        public string address_city = "";             // サンプルデータ > xxx
        public string quantity = "";                 // サンプルデータ > 1
        public string verify_sign = "";              // サンプルデータ > xxx
        public string payer_email = "";              // サンプルデータ > Unikktle-buyer @xxx.com

        public string option_name1 = "";             // サンプルデータ > Credit
        public string txn_id = "";                   // サンプルデータ > xxx
        public string payment_type = "";             // サンプルデータ > instant
        public string last_name = "";                // サンプルデータ > buyer
        public string address_state = "";            // サンプルデータ > xxx
        public string receiver_email = "";           // サンプルデータ > xxx @xxx.com

        public string payment_fee = "";              // サンプルデータ > 
        public string shipping_discount = "";        // サンプルデータ > 0
        public string insurance_amount = "";         // サンプルデータ > 0
        public string receiver_id = "";              // サンプルデータ > xxx
        public string txn_type = "";                 // サンプルデータ > web_accept
        public string item_name = "";                // サンプルデータ > Credit
        public string discount = "";                 // サンプルデータ > 0
        public string mc_currency = "";              // サンプルデータ > JPY
        public string item_number = "";              // サンプルデータ > c1
        public string residence_country = "";        // サンプルデータ > JP
        public string test_ipn = "";                 // サンプルデータ > 1
        public string shipping_method = "";          // サンプルデータ > Default
        public string transaction_subject = "";      // サンプルデータ > 
        public string payment_gross = "";            // サンプルデータ > 
        public string ipn_track_id = "";             // サンプルデータ > xxx
    }
}
