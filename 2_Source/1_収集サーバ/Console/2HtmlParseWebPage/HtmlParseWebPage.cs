using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using 定数;
using DB;
using Logging;
using AppDirectory;
using Common;

namespace _2HtmlParseWebPage
{
    public static class HtmlParseWebPage
    {
        //private static int count = 0;

        public static bool Exec(SqlConnection cn, DateTime now, ref long cnt)
        {
            var htmlParseResult = new List<HtmlParseResultWebPage>();
            var fail = new List<HtmlParseFailWebPage>();

            var htmlIsMore = false;
            // SqlDataReaderで1件づつ処理する作りだと、SQLServerから応答が返って来ない現象に遭遇したので、
            // 100件づつDataTableに入れて、0件になるまで処理を続行する。

            using (var dtTbl = DB.Collect.SP_CollectWebPage.Select_CutoutStateHtmlNull(cn))
            {
                if (dtTbl.Rows.Count > 0)
                {
                    cnt += dtTbl.Rows.Count;
                    htmlIsMore = true;
                }

                foreach (DataRow row in dtTbl.Rows)
                {
                    var domainNo = (long)row["DomainNo"];
                    var urlNo = (long)row["UrlNo"];
                    var html = (string)row["Html"];

                    try
                    {
                        var html1段階目result = HtmlParse.一般HtmlTag除外(html,
                            out string html1段階目, out string status1段階目);

                        if (html1段階目result == false)
                        {
                            fail.Add(new HtmlParseFailWebPage()
                            {
                                DomainNo = domainNo,
                                UrlNo = urlNo,
                                Status = "一般HtmlTag除外() Fail \r\n" + status1段階目,
                                Html = html
                            });
                        }

                        var html2段階目 = "";
                        言語判定結果? 言語判定結果 = null;
                        if (html1段階目result)
                        {
                            try
                            {
                                html2段階目 = HtmlParse.Htmlタグ除去(html1段階目);
                            }
                            catch (Exception ex)
                            {
                                ログ.ログ書き出し("HtmlParse.Htmlタグ除去() Fail. DomainNo:" + domainNo + " UrlNo:" + urlNo);

                                htmlParseResult.Add(new HtmlParseResultWebPage()
                                {
                                    DomainNo = domainNo,
                                    UrlNo = urlNo,
                                    解析結果 = 解析結果.失敗
                                });

                                throw;
                            }

                            // &gt; を > に変換など。Decodeしてから返す。
                            html2段階目 = HttpUtility.UrlDecode(HttpUtility.HtmlDecode(html2段階目));

                            言語判定結果 = StringProcessing.言語判定(html2段階目);
                        }

                        if (DB.Collect.SP_HtmlParseWebPage.GetCount(cn, domainNo, urlNo) < 1)
                        {
                            // 初回
                            DB.Collect.BulkCopy_HtmlParseWebPage.Add(
                                domainNo, urlNo, 言語判定結果, html1段階目, html2段階目);
                        }
                        else
                        {
                            // 2回目以降
                            DB.Collect.SP_HtmlParseWebPage.Update_Html(cn,
                                domainNo, urlNo, 言語判定結果, html1段階目, html2段階目);
                        }

                        htmlParseResult.Add(new HtmlParseResultWebPage()
                        {
                            DomainNo = domainNo,
                            UrlNo = urlNo,
                            解析結果 = html1段階目result ? 解析結果.成功 : 解析結果.失敗
                        });

                    }
                    catch (Exception ex)
                    {
                        ログ.ログ書き出し(ex);
                        ログ.ログ書き出し("DomainNo:" + domainNo + " UrlNo:" + urlNo);
                    }
                }

                if (fail.Count > 0)
                {
                    HtmlParse.FailWebPage("WebPage", fail);
                }

                if (DB.Collect.BulkCopy_HtmlParseWebPage.Data.Rows.Count > 0)
                {
                    DB.Collect.BulkCopy_HtmlParseWebPage.Flush(AppSetting.ConnectionString_UnikktleCollect);
                }

                if (htmlParseResult.Count > 0)
                {
                    // 解析済みフラグを設定
                    foreach (var row in htmlParseResult)
                    {
                        DB.Collect.SP_CollectWebPage.Update_CutoutStateHtml(cn, row.DomainNo, row.UrlNo, row.解析結果);
                    }
                }
            }

            return htmlIsMore;
        }

        // Todo: Googleの解析と同様に、固有のHtmlタグ処理を先にする。

    }
}
