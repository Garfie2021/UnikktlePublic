using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using 定数;
using Logging;
using AppDirectory;
using Common;


namespace Common
{
    public static class UrlParse
    {
        public static 解析結果 Url切り出し(string html, out List<string> urlList)
        {
            urlList = new List<string>();

            try
            {
                // aタグが無くなるまでループ。
                var startIndex = 0;
                while ((startIndex = html.IndexOf("<a ", StringComparison.OrdinalIgnoreCase)) > -1)
                {
                    // aタグが無い文字列は捨てる。
                    html = html.Substring(startIndex);

                    var endIndex = html.IndexOf(">", StringComparison.OrdinalIgnoreCase);

                    // 解析用に、見つかったaタグを切り出す
                    var aTag = html.Substring(0, endIndex);

                    // aタグからurlを切り出す
                    var url = href解析(aTag);

                    if (url.StartsWith("http"))
                    {
                        // 検索エンジンのサイト内URLは捨てる。
                        urlList.Add(url);
                    }

                    // aタグ以降を切り出す
                    html = html.Substring(endIndex);
                }

                return 解析結果.成功;
            }
            catch(Exception ex)
            {
                return 解析結果.失敗;
            }

        }

        public static string href解析(string aTag)
        {
            var startIndex_href = aTag.IndexOf(" href=\"", StringComparison.OrdinalIgnoreCase);

            aTag = aTag.Substring(startIndex_href + " href=\"".Length);

            var url = aTag.Substring(0, aTag.IndexOf("\"", StringComparison.OrdinalIgnoreCase));

            return url;
        }

        public static long GetDomainNo(SqlConnection cn, string url)
        {
            try
            {
                try
                {
                    return DB.Collect.SP_Domain.GetWithInsert(cn, (new Uri(url)).Authority);
                }
                catch (Exception ex)
                {
                    ログ.ログ書き出し(ex);
                    ログ.ログ書き出し("url:" + url);
                }

                return DB.Collect.SP_Domain.GetWithInsert(cn, (new Uri(HttpUtility.UrlDecode(url))).Authority);
            }
            catch (Exception ex)
            {
                ログ.ログ書き出し(ex);
                ログ.ログ書き出し("url:" + url);

                throw;
            }

            
        }

    }
}
