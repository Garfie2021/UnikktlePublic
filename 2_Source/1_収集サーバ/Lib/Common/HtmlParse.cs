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
    public static class HtmlParse
    {
        public static void Fail(string searchName, List<HtmlParseFail> fail)
        {
            var mailBody = "";
            foreach (var row in fail)
            {
                string outHtmlFileName = searchName + "_" + row.SearchKeywordNo + "_" + row.SearchDate.ToString("yyyyMMddHHmmss") + ".html";

                ログ.Write_HtmlParse_ErrorHtml(outHtmlFileName, row.Html);

                mailBody +=
                    "SearchKeywordNo: " + row.SearchKeywordNo + "\r\n" +
                    "SearchDate: " + row.SearchDate + "\r\n" +
                    "status: " + row.Status + "\r\n" +
                    "outHtmlFileName: " + outHtmlFileName + "\r\n\r\n";
            }

            Mail.SendMailAndLogErr("[2HtmlParse] " + searchName + " error", mailBody);
        }

        public static void FailWebPage(string searchName, List<HtmlParseFailWebPage> fail)
        {
            var mailBody = "";
            foreach (var row in fail)
            {
                string outHtmlFileName = searchName + "_" + row.UrlNo + ".html";

                ログ.Write_HtmlParse_ErrorHtml(outHtmlFileName, row.Html);

                mailBody +=
                    "UrlNo: " + row.UrlNo + "\r\n" +
                    "status: " + row.Status + "\r\n" +
                    "outHtmlFileName: " + outHtmlFileName + "\r\n\r\n";
            }

            Mail.SendMailAndLogErr("[2HtmlParseWebPage] " + searchName + " error", mailBody);
        }


        public static bool 一般HtmlTag除外(string html, 
            out string outHtml, out string status)
        {
            try
            {
                var index = html.IndexOf("</head>");
                if (index > -1)
                {
                    html = html.Substring(index + "</head>".Length);
                }

                index = html.IndexOf("<body");
                if (index > -1)
                {
                    html = html.Substring(html.IndexOf("<body"));
                }

                html = HtmlNode除外(html, "<script", "</script>");
                html = HtmlNode除外(html, "<style", "</style>");
                html = HtmlNode除外(html, "<!--", "-->");
                html = HtmlNode除外(html, "href=\"", "\"");
                html = HtmlNode除外(html, "style=\"", "\"");
                html = HtmlNode除外(html, "src=\"", "\"");
                html = HtmlNode除外(html, "data-ved=\"", "\"");
                html = HtmlNode除外(html, "onclick=\"", "\"");
                html = HtmlNode除外(html, "bgcolor=\"", "\"");
                html = HtmlNode除外(html, "marginheight=\"", "\"");
                html = HtmlNode除外(html, "marginwidth=\"", "\"");
                html = HtmlNode除外(html, "topmargin=\"", "\"");
                html = HtmlNode除外(html, "bgcolor=\"", "\"");
                html = HtmlNode除外(html, "title=\"", "\"");
                html = HtmlNode除外(html, "border=\"", "\"");
                html = HtmlNode除外(html, "cellpadding=\"", "\"");
                html = HtmlNode除外(html, "cellspacing=\"", "\"");
                html = HtmlNode除外(html, "width=\"", "\"");
                html = HtmlNode除外(html, "valign=\"", "\"");
                html = HtmlNode除外(html, "maxlength=\"", "\"");
                html = HtmlNode除外(html, "type=\"", "\"");
                html = HtmlNode除外(html, "value=\"", "\"");
                html = HtmlNode除外(html, "alt=\"", "\"");
                html = HtmlNode除外(html, "aria-level=\"", "\"");
                html = HtmlNode除外(html, "role=\"", "\"");
                html = HtmlNode除外(html, "aria-expanded=\"", "\"");
                html = HtmlNode除外(html, "aria-haspopup=\"", "\"");
                html = HtmlNode除外(html, "tabindex=\"", "\"");
                html = HtmlNode除外(html, " h=\"", "\"");
                html = HtmlNode除外(html, " u=\"", "\"");
                html = HtmlNode除外(html, " k=\"", "\"");

                outHtml = html;
                status = "";
                return true;
            }
            catch(Exception ex)
            {
                outHtml = "";
                status = ex.Message + "\r\n" + ex.StackTrace;
                return false;
            }

        }

        public static string HtmlNode除外(string html, string startTag, string endTag)
        {
            var IndexStart = 0;
            var IndexEnd = 0;
            // アルファベットの大文字小文字を区別しない。
            //while ((IndexStart = html.IndexOf(startTag, IndexStart, StringComparison.OrdinalIgnoreCase)) > -1 &&
            //                     html.IndexOf(endTag, IndexStart, StringComparison.OrdinalIgnoreCase) > -1)
            while ((IndexStart = html.IndexOf(startTag, IndexStart, StringComparison.OrdinalIgnoreCase)) > -1)
            {
                IndexEnd = html.IndexOf(endTag, IndexStart + startTag.Length, StringComparison.OrdinalIgnoreCase) + endTag.Length;

                html = html.Substring(0, IndexStart) + html.Substring(IndexEnd);
            }

            return html;
        }

        public static string HtmlNode除外_終了タグは残す(string html, string startTag, string endTag)
        {
            var IndexStart = 0;
            var IndexEnd = 0;
            // アルファベットの大文字小文字を区別しない。
            while ((IndexStart = html.IndexOf(startTag, IndexStart, StringComparison.OrdinalIgnoreCase)) > -1)
            {
                IndexEnd = html.IndexOf(endTag, IndexStart + startTag.Length, StringComparison.OrdinalIgnoreCase);

                html = html.Substring(0, IndexStart) + ">" + html.Substring(IndexEnd);
            }

            return html;
        }

        // 正規表現では上手く除去できないのでIndexOf()を使う
        public static string Htmlタグ除去(string html)
        {
                // HTMLタグを除去
                int start;
                int end;
                while (html.IndexOf('<') > -1)
                {
                    start = html.IndexOf('<');
                    end = html.IndexOf('>', start) + ">".Length;

                    html = html.Remove(start, end - start);
                    html = html.Insert(start, "\n");
                }

                // 不要なタグを除去
                //html = html.Replace("&nbsp", " ");
                html = html.Replace("...", " ");

                // 改行コードを統一
                html = html.Replace("\r\n", "\n");

                // 多過ぎるスペースと改行を除去
                var cnt = 0;
                while (cnt < 5)
                {
                    html = html.Replace("   ", " ");
                    html = html.Replace("  ", " ");

                    html = html.Replace("\n\n\n", "\n");
                    html = html.Replace("\n\n", "\n");

                    html = html.Replace(" \n ", "\n");
                    html = html.Replace("\n \n", "\n");

                    cnt++;
                }




                ////var result = "";
                //while (html.IndexOf("<") > -1)
                //{
                //    try
                //    {
                //        var htmlTag = html.Substring(html.IndexOf("<"), html.IndexOf(">") + ">".Length - html.IndexOf("<"));
                //        html = html.Replace(htmlTag, "\n");
                //    }
                //    catch (Exception ex)
                //    {
                //        var a = ex;
                //    }
                //}


                //// 連続し過ぎてるスペースを削除
                //var result = "";
                //foreach (var row in html.Split('  '))
                //{
                //    if (string.IsNullOrEmpty(row))
                //    {
                //        // 無視
                //        continue;
                //    }

                //    result += row + "\n";
                //}

                //// 連続し過ぎてる\r\nを削除
                //var result = "";
                //foreach (var row in html.Split('\n'))
                //{
                //    if (string.IsNullOrEmpty(row))
                //    {
                //        // 無視
                //        continue;
                //    }

                //    result += row + "\n";
                //}


                //while (html.IndexOf("\r\n\r\n") > -1)
                //{
                //    html = html.Substring(html.IndexOf("\r\n\r\n"));

                //    html = html.Replace("\r\n\r\n", "\r\n");
                //}
                //while (html.IndexOf("\r\n\r\n") > -1)
                //{
                //    html = 改行Regex.Replace(html, "\r\n");
                //}


                return html;
        }

    }
}
