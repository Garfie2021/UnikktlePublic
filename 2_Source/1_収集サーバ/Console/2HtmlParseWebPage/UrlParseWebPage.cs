using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using 定数;
using DB;
using Logging;
using AppDirectory;
using Common;

namespace _2HtmlParseWebPage
{
    public static class UrlParseWebPage
    {
        //private static int count = 0;

        public static bool Exec(SqlConnection cn, DateTime now, ref long cnt)
        {
            var urlIsMore = false;
            // SqlDataReaderで1件づつ処理する作りだと、SQLServerから応答が返って来ない現象に遭遇したので、
            // 100件づつDataTableに入れて、0件になるまで処理を続行する。
            using (var dtTbl = DB.Collect.SP_CollectWebPage.Select_CutoutStateUrlNull(cn))
            {
                if (dtTbl.Rows.Count > 0)
                {
                    cnt += dtTbl.Rows.Count;
                    urlIsMore = true;
                }

                foreach (DataRow row in dtTbl.Rows)
                {
                    try
                    {
                        var domainNo = (long)row["DomainNo"];
                        var urlNo = (long)row["UrlNo"];
                        var html = (string)row["Html"];

                        try
                        {
                            UrlParse.Url切り出し(html, out List<string> urlList);

                            foreach (var url in urlList)
                            {
                                DB.Collect.SP_Url.Insert(cn, UrlParse.GetDomainNo(cn, url), url);
                            }

                            DB.Collect.SP_CollectWebPage.Update_CutoutStateUrl(cn, domainNo, urlNo, 解析結果.成功);
                        }
                        catch (Exception ex)
                        {
                            // ログ出力して処理は続行。
                            ログ.ログ書き出し(ex);
                            ログ.ログ書き出し("DomainNo:" + domainNo + " UrlNo:" + urlNo);

                            DB.Collect.SP_CollectWebPage.Update_CutoutStateUrl(cn, domainNo, urlNo, 解析結果.失敗);
                        }
                    }
                    catch (Exception ex)
                    {
                        // ログ出力して処理は続行。
                        ログ.ログ書き出し(ex);
                    }
                }

                return urlIsMore;
            }

            //ログ.ログ書き出し($"UrlParseWebPage exec. Cnt:{cnt}");
        }

    }
}
