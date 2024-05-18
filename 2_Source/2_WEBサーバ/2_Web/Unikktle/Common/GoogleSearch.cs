//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Web;
//using System.Threading.Tasks;

//namespace Unikktle.Common
//{
//    public static class GoogleSearch
//    {

//        // HTMLをそのまま返す。
//        public static string Search(string keyword)
//        {
//            try
//            {
//                var wc = new WebClient();

//                var url = URLConst.UrlFormat_Google + HttpUtility.UrlEncode(keywordRow.Word);

//                ログ.ログ書き出し(url);

//                using (var st = wc.OpenRead(url))
//                using (var sr = new StreamReader(st, Encoding.UTF8))
//                {
//                    // &gt; を > に変換など。Decodeしてから返す。
//                    //return HttpUtility.UrlDecode(HttpUtility.HtmlDecode(sr.ReadToEnd()));
//                    return sr.ReadToEnd();
//                }
//            }
//            catch
//            {
//                ログ.ログ書き出し(@"GoogleSearch.Search() 失敗" + "\r\n" +
//                    "searchWord: " + keywordRow.Word);
//                throw;
//            }
//        }


//    }
//}
