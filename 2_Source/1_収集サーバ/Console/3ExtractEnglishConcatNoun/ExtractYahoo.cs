using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 定数;
using Logging;
using Common;

namespace ExtractEnglishConcatNoun
{
    public static class ExtractYahoo
    {
        public static List<抽出文> 必要な文のみ抽出(HtmlParseYahooRow row)
        {
            if (row.HtmlTag除外後2段階目.IndexOf("<li>") > -1)
            {
                return 必要な文のみ抽出_Pattern1(row.HtmlTag除外後2段階目);
            }
            //else if (html.IndexOf("<div class=\"w\">") > -1)
            //{
            //    throw new NotImplementedException("<div class=\"w\">");
            //    //return 必要な文のみ抽出_Pattern1(html);
            //}
            else
            {
                ExtractEnglishConcatNounCommon.Warning("Yahoo",
                    row.SearchKeywordNo, row.SearchDate, "必要な文のみ抽出() Warning", row.HtmlTag除外後2段階目);

                var 抽出文List = new List<抽出文>();
                抽出文List.Add(new 抽出文()
                {
                    SearchResultNo = 1,
                    関連キーワード = 関連キーワードFlag.関連キーワードじゃない,
                    文 = HtmlParse.Htmlタグ除去(row.HtmlTag除外後2段階目)
                });

                //throw new NotImplementedException("必要な文のみ抽出 else");
                return 抽出文List;
            }
        }

        private static List<抽出文> 必要な文のみ抽出_Pattern1(string html)
        {
            try
            {
                byte SearchResultNo;
                var 検索結果TopN = new List<抽出文>();
                html = TopNを抽出(html, ref 検索結果TopN, out SearchResultNo);
                //関連キーワード(html, ref 検索結果TopN, SearchResultNo);

                return 検索結果TopN;
            }
            catch (Exception ex)
            {
                ログ.ログ書き出し(@"YahooSearchHtmlParse.必要な文のみ抽出() 失敗" + "\r\n" + "html: " + html);
                ログ.ログ書き出し(ex);
                throw;
            }
        }

        private static string TopNを抽出(string html, ref List<抽出文> 検索結果TopN, out byte SearchResultNo)
        {
            try
            {
                SearchResultNo = 0;

                //// 先頭の不要文字列を除去
                //html = html.Substring(html.IndexOf("<li>"));

                //// 後方の不要文字列を除去
                //html = html.Remove(html.IndexOf("</ol>"));

                // 文毎に分割
                while (html.IndexOf("<li>", "<li>".Length) > -1)
                {
                    var result = "";
                    //if (html.IndexOf("<li>", "<li>".Length) > -1)
                    //{
                        result = html.Substring(0, html.IndexOf("<li>", "<li>".Length));
                        html = html.Substring(html.IndexOf("<li>", "<li>".Length));
                    //}
                    //else
                    //{
                    //    // 最後の1行
                    //    result = html.Substring(0, html.IndexOf("</li>", "</li>".Length));
                    //    html = "";
                    //}

                    検索結果TopN.Add(new 抽出文()
                    {
                        SearchResultNo = SearchResultNo++,
                        関連キーワード = 関連キーワードFlag.関連キーワードじゃない,
                        文 = HtmlParse.Htmlタグ除去(result)
                    });
                }

                検索結果TopN.Add(new 抽出文()
                {
                    SearchResultNo = SearchResultNo++,
                    関連キーワード = 関連キーワードFlag.関連キーワードじゃない,
                    文 = HtmlParse.Htmlタグ除去(html)
                });

                return html;
            }
            catch (Exception ex)
            {
                ログ.ログ書き出し(@"GoogleSearchHtmlParse.必要な文のみ抽出() 失敗" + "\r\n" + "html: " + html);
                ログ.ログ書き出し(ex);
                throw;
            }
        }

        private static void 関連キーワード(string html, ref List<抽出文> 検索結果TopN, byte SearchResultNo)
        {
            if (html.IndexOf("関連するキーワード") > -1)
            {
                html = html.Substring(html.IndexOf("関連するキーワード") + "関連するキーワード".Length);

                検索結果TopN.Add(new 抽出文()
                {
                    SearchResultNo = SearchResultNo,
                    関連キーワード = 関連キーワードFlag.関連キーワード,
                    文 = HtmlParse.Htmlタグ除去(html)
                });
            }
        }

        private static List<抽出文> 必要な文のみ抽出_Pattern2(string html)
        {
            try
            {
                // 先頭の不要文字列を除去
                html = html.Substring(html.IndexOf("<li>"));

                // 後方の不要文字列を除去
                html = html.Remove(html.IndexOf("</ol>"));

                // 文毎に分割
                var 検索結果TopN = new List<抽出文>();

                while (html.IndexOf("<li>") > -1)
                {
                    var result = "";
                    if (html.IndexOf("<li>", "<li>".Length) > -1)
                    {
                        result = html.Substring(0, html.IndexOf("<li>", "<li>".Length));
                        html = html.Substring(html.IndexOf("<li>", "<li>".Length));
                    }
                    else
                    {
                        // 最後の1行
                        result = html.Substring(0, html.IndexOf("</li>", "</li>".Length));
                        html = "";
                    }

                    検索結果TopN.Add(new 抽出文()
                    {
                        SearchResultNo = 0,
                        関連キーワード = 関連キーワードFlag.関連キーワード,
                        文 = HtmlParse.Htmlタグ除去(result)
                    });
                }

                return 検索結果TopN;
            }
            catch (Exception ex)
            {
                ログ.ログ書き出し(@"GoogleSearchHtmlParse.必要な文のみ抽出() 失敗" + "\r\n" +
                    "html: " + html);
                ログ.ログ書き出し(ex);
                throw;
            }
        }

        private static List<string> 必要な文のみ抽出_Pattern3(string html)
        {
            try
            {
                var 検索結果 = new List<string>();

                //検索結果.Add(htmlタグRegex.Replace(html, ""));
                検索結果.Add(HtmlParse.Htmlタグ除去(html));

                return 検索結果;
            }
            catch (Exception ex)
            {
                ログ.ログ書き出し(@"YahooSearchHtmlParse.必要な文のみ抽出() 失敗" + "\r\n" +
                    "html: " + html);
                ログ.ログ書き出し(ex);
                throw;
            }
        }

    }
}
