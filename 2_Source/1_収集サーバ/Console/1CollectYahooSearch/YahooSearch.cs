using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
//using Yahoo.Apis.Discovery.v1;
//using Yahoo.Apis.Discovery.v1.Data;
//using Yahoo.Apis.Services;
using 定数;
using DB;
using Logging;
using AppDirectory;


namespace CollectYahooSearch
{
    public static class YahooSearch
    {
        private const string URLテンプレート = "https://search.yahoo.co.jp/search?p={0}";
        private static WebClient wc = new WebClient();

        // HTMLをそのまま返す。
        public static string Search(KeywordRow keywordRow)
        {
            try
            {
                var url = string.Format(URLテンプレート, HttpUtility.UrlEncode(keywordRow.Word));

                ログ.ログ書き出し(url);

                using (var st = wc.OpenRead(url))
                using (var sr = new StreamReader(st, Encoding.UTF8))
                {
                    // &gt; を > に変換など。Decodeしてから返す。
                    //return HttpUtility.UrlDecode(HttpUtility.HtmlDecode(sr.ReadToEnd()));
                    return sr.ReadToEnd();
                }
            }
            catch
            {
                ログ.ログ書き出し(@"YahooSearch.Search() 失敗" + "\r\n" +
                    "searchWord: " + keywordRow.Word);
                throw;
            }
        }

    }
}
