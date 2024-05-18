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

namespace _2HtmlParse
{
    public static class HtmlParseGoogle
    {
        //private static int count = 0;

        public static void Exec(SqlConnection cn, DateTime now)
        {
            ExecParse(cn, out var htmlParseResult, out var fail, out var rowCnt);

            if (fail.Count > 0)
            {
                HtmlParse.Fail("Google", fail);
            }

            if (htmlParseResult.Count > 0)
            {
                DB.Collect.BulkCopy_HtmlParseGoogle.Flush(AppSetting.ConnectionString_UnikktleCollect);

                // 英語連結名詞 抽出済みフラグを設定
                foreach (var row in htmlParseResult)
                {
                    DB.Collect.SP_CollectGoogle.Update_State(cn,
                        row.SearchKeywordNo,
                        row.SearchDate,
                        State.解析済み,
                        row.解析結果);
                }
            }

            ログ.ログ書き出し($"抽出Google End. Count{rowCnt}");
        }

        public static void ExecParse(SqlConnection cn,
            out List<HtmlParseResult> htmlParseResult, out List<HtmlParseFail> fail,
            out int rowCnt)
        {
            var dataTable = DB.Collect.SP_CollectGoogle.Select_State0(cn);
            rowCnt = dataTable.Rows.Count;

            htmlParseResult = new List<HtmlParseResult>();
            fail = new List<HtmlParseFail>();

            foreach (DataRow row in dataTable.Rows)
            {
                var html = (string)row[2];
                var html1段階目 = "";
                var status1段階目 = "";
                var html1段階目result = 解析結果.失敗;

                // 最初に不要な箇所を大きく削除しておく
                if (html.IndexOf("<div class=\"g\">") > -1)
                {
                    html1段階目result = HtmlTag除外1段階目_PaternG(html,
                        out html1段階目, out status1段階目);
                }
                else if (html.IndexOf("<div class=\"ZINbbc xpd O9g5cc uUPGi\"") > -1)
                {
                    html1段階目result = HtmlTag除外1段階目_PaternZINbbc(html,
                        out html1段階目, out status1段階目);
                }
                else
                {
                    // このTagが見つからないということは検索結果のフォーマットが大きく変わった。
                    html1段階目result = 解析結果.失敗;
                    status1段階目 = "tag not found";
                }

                if (html1段階目result == 解析結果.失敗)
                {
                    fail.Add(new HtmlParseFail()
                    {
                        SearchKeywordNo = (long)row[0],
                        SearchDate = (DateTime)row[1],
                        Status = "HtmlTag除外1段階目() Fail \r\n" + status1段階目,
                        Html = html
                    });
                }

                var html2段階目 = "";
                var html2段階目result = false;
                if (html1段階目result == 解析結果.成功)
                {
                    html2段階目result = HtmlParse.一般HtmlTag除外(html1段階目,
                        out html2段階目, out var status2段階目);

                    if (html2段階目result == false)
                    {
                        fail.Add(new HtmlParseFail()
                        {
                            SearchKeywordNo = (long)row[0],
                            SearchDate = (DateTime)row[1],
                            Status = "一般HtmlTag除外() Fail \r\n" + status2段階目,
                            Html = html1段階目
                        });
                    }
                }

                if (html2段階目result)
                {
                    DB.Collect.BulkCopy_HtmlParseGoogle.Add(
                        (long)row[0], (DateTime)row[1],
                        html1段階目, html2段階目);
                }

                htmlParseResult.Add(new HtmlParseResult()
                {
                    SearchKeywordNo = (long)row[0],
                    SearchDate = (DateTime)row[1],
                    解析結果 = html2段階目result ? 解析結果.成功 : 解析結果.失敗
                });
            }
        }

        public static 解析結果 HtmlTag除外1段階目_PaternG(string html, 
            out string outHtml, out string status)
        {
            var startIndex = html.IndexOf("<div class=\"g\">");
            if (startIndex < 0)
            {
                outHtml = "";
                status = "Html tag not found." + "\r\n " + "<div class=\"g\">";
                return 解析結果.失敗;
            }

            // 開始点までの文字列を削除
            html = html.Substring(startIndex);

            var endIndex = html.IndexOf("<!--z-->");
            if (endIndex < 0)
            {
                outHtml = "";
                status = "Html tag not found." + "\r\n " + "<!--z-->";
                return 解析結果.失敗;
            }

            // 終了点以降の文字列を削除
            html = html.Substring(0, endIndex);

            html = HtmlParse.HtmlNode除外(html, "<g-section-with-header", "</g-section-with-header>");
            html = HtmlParse.HtmlNode除外(html, "<div class=\"TXwUJf\"", "</div>");
            html = HtmlParse.HtmlNode除外(html, "<div class=\"action-menu-panel ab_dropdown\"", "</div>");
            html = HtmlParse.HtmlNode除外(html, "<cite", "</cite>");
            html = HtmlParse.HtmlNode除外(html, "<span class=\"f\"", "</span>");

            outHtml = html;
            status = "";
            return 解析結果.成功;
        }

        public static 解析結果 HtmlTag除外1段階目_PaternZINbbc(string html,
            out string outHtml, out string status)
        {
            var startIndex = html.IndexOf("<div class=\"ZINbbc xpd O9g5cc uUPGi\"");
            if (startIndex < 0)
            {
                outHtml = "";
                status = "Html tag not found.\r\n <div class=\"ZINbbc xpd O9g5cc uUPGi\"";
                return 解析結果.失敗;
            }

            // 開始点までの文字列を削除
            html = html.Substring(startIndex);

            var endIndex = html.IndexOf("<footer");
            if (endIndex < 0)
            {
                // このTagが見つからないということは検索結果のフォーマットが大きく変わった。
                outHtml = "";
                status = "Html tag not found.\r\n <footer";
                return 解析結果.失敗;
            }

            // 終了点以降の文字列を削除
            html = html.Substring(0, endIndex);

            html = html.Replace(">他の人はこちらも検索<", "><");
            html = html.Replace(">関連キーワード<", "><");
            html = html.Replace(">こちらを検索しますか<", "><");

            outHtml = html;
            status = "";
            return 解析結果.成功;
        }

        //public static string HtmlTag除外2段階目(string html)
        //{
        //    if (html.IndexOf("<div class=\"g\">") < 0)
        //    {
        //        // 検索結果が0件
        //        return "";
        //    }

        //    // 開始点までの文字列を削除
        //    html = html.Substring(html.IndexOf("<div class=\"g\">") + "<div class=\"g\">".Length);

        //    // 終了点以降の文字列を削除
        //    html = html.Substring(0, html.IndexOf("<div id=\"foot\">"));

        //    // Google固有の不要タグを削除
        //    html = HtmlParse.HtmlNode除外(html, "<cite>", "</cite>");
        //    html = HtmlParse.HtmlNode除外_終了タグは残す(html, "class=\"am-dropdown-menu\"", "</div>");

        //    return html;
        //}
    }
}
