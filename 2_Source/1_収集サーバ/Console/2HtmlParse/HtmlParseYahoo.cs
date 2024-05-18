﻿using System;
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
    public static class HtmlParseYahoo
    {
        //private static int count = 0;

        public static void Exec(SqlConnection cn, DateTime now)
        {
            ExecParse(cn, out var htmlParseResult, out var fail, out var rowCnt);

            if (fail.Count > 0)
            {
                HtmlParse.Fail("Yahoo", fail);
            }

            if (htmlParseResult.Count > 0)
            {
                DB.Collect.BulkCopy_HtmlParseYahoo.Flush(AppSetting.ConnectionString_UnikktleCollect);

                // 英語連結名詞 抽出済みフラグを設定
                foreach (var row in htmlParseResult)
                {
                    DB.Collect.SP_CollectYahoo.Update_State(cn,
                        row.SearchKeywordNo,
                        row.SearchDate,
                        State.解析済み,
                        row.解析結果);
                }
            }

            ログ.ログ書き出し($"抽出Yahoo End. Count{rowCnt}");
        }

        // Todo: Googleの解析と同様に、固有のHtmlタグ処理を先にする。
        public static void ExecParse(SqlConnection cn,
            out List<HtmlParseResult> htmlParseResult, out List<HtmlParseFail> fail,
            out int rowCnt)
        {
            var dataTable = DB.Collect.SP_CollectYahoo.Select_State0(cn);
            rowCnt = dataTable.Rows.Count;

            htmlParseResult = new List<HtmlParseResult>();
            fail = new List<HtmlParseFail>();

            foreach (DataRow row in dataTable.Rows)
            {
                var html = (string)row[2];
                var html1段階目 = "";
                var status1段階目 = "";
                //var html1段階目result = 解析結果.失敗;

                var html1段階目result = HtmlParse.一般HtmlTag除外(html,
                    out html1段階目, out status1段階目);

                if (html1段階目result == false)
                {
                    fail.Add(new HtmlParseFail()
                    {
                        SearchKeywordNo = (long)row[0],
                        SearchDate = (DateTime)row[1],
                        Status = "一般HtmlTag除外() Fail \r\n" + status1段階目,
                        Html = html
                    });
                }

                var html2段階目 = "";
                var html2段階目result = 解析結果.失敗;
                if (html1段階目result)
                {
                    html2段階目result = HtmlTag除外1段階目(html1段階目,
                        out html2段階目, out var status2段階目);

                    if (html2段階目result == 解析結果.失敗)
                    {
                        fail.Add(new HtmlParseFail()
                        {
                            SearchKeywordNo = (long)row[0],
                            SearchDate = (DateTime)row[1],
                            Status = "HtmlTag除外1段階目() Fail \r\n" + status2段階目,
                            Html = html1段階目
                        });
                    }
                }

                if (html2段階目result == 解析結果.成功)
                {
                    DB.Collect.BulkCopy_HtmlParseYahoo.Add(
                        (long)row[0], (DateTime)row[1],
                        html1段階目, html2段階目);
                }

                htmlParseResult.Add(new HtmlParseResult()
                {
                    SearchKeywordNo = (long)row[0],
                    SearchDate = (DateTime)row[1],
                    解析結果 = html2段階目result
                });
            }
        }

        private static 解析結果 HtmlTag除外1段階目(string html, 
            out string outHtml, out string status)
        {
            if (html.IndexOf("<li") < 0)
            {
                // 検索結果が0件
                outHtml = "";
                status = "Html tag not found.  \r\n  <li";
                return 解析結果.失敗;
            }

            // 開始点までの文字列を削除
            html = html.Substring(html.IndexOf("<ol>") + "<ol>".Length);

            // 終了点以降の文字列を削除
            if (html.IndexOf("<div id=\"rel\">") > -1)
            {
                html = html.Substring(0, html.IndexOf("<div id=\"rel\">"));
            }
            else if (html.IndexOf("<div id=\"pg\">") > -1)
            {
                html = html.Substring(0, html.IndexOf("<div id=\"pg\">"));
            }
            else
            {
                //throw new NotImplementedException("HtmlTag除外2段階目 else");
            }

            // 固有の不要タグを削除
            html = HtmlParse.HtmlNode除外(html, "<em>", "</em>");

            outHtml = html;
            status = "";
            return 解析結果.成功;
        }

        //private static string HtmlTag除外2段階目(string html)
        //{
        //    if (html.IndexOf("<li") < 0)
        //    {
        //        // 検索結果が0件
        //        return "";
        //    }

        //    // 開始点までの文字列を削除
        //    html = html.Substring(html.IndexOf("<ol>") + "<ol>".Length);

        //    // 終了点以降の文字列を削除
        //    if (html.IndexOf("<div id=\"rel\">") > -1)
        //    {
        //        html = html.Substring(0, html.IndexOf("<div id=\"rel\">"));
        //    }
        //    else if (html.IndexOf("<div id=\"pg\">") > -1)
        //    {
        //        html = html.Substring(0, html.IndexOf("<div id=\"pg\">"));
        //    }
        //    else
        //    {
        //        //throw new NotImplementedException("HtmlTag除外2段階目 else");
        //    }

        //    // Google固有の不要タグを削除
        //    html = HtmlParse.HtmlNode除外(html, "<em>", "</em>");            

        //    return html;
        //}
    }
}
