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
    public static class ExtractGoogle
    {
        public static List<抽出文> 必要な文のみ抽出(HtmlParseGoogleRow row)
        {
            if (row.HtmlTag除外後2段階目.IndexOf("<div class=\"g\">") > -1)
            {
                return 必要な文のみ抽出_Pattern1(row.HtmlTag除外後2段階目, "<div class=\"g\">");
            }
            else if(row.HtmlTag除外後2段階目.IndexOf("<div class=\"ZINbbc xpd O9g5cc uUPGi\">") > -1)
            {
                return 必要な文のみ抽出_Pattern1(row.HtmlTag除外後2段階目, "<div class=\"ZINbbc xpd O9g5cc uUPGi\">");
            }
            else
            {
                ExtractEnglishConcatNounCommon.Warning("Google",
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

        private static List<抽出文> 必要な文のみ抽出_Pattern1(string html, string nodeString)
        {
            try
            {
                byte SearchResultNo;
                var 検索結果TopN = new List<抽出文>();
                html = TopNを抽出(html, nodeString, ref 検索結果TopN, out SearchResultNo);
                //return 関連キーワード(html, ref 検索結果TopN, SearchResultNo);
                return 検索結果TopN;
            }
            catch (Exception ex)
            {
                ログ.ログ書き出し(@"GoogleSearchHtmlParse.必要な文のみ抽出() 失敗" + "\r\n" + "html: " + html);
                ログ.ログ書き出し(ex);
                throw;
            }
        }

        private static string TopNを抽出(string html, string nodeString,
            ref List<抽出文> 検索結果TopN, out byte SearchResultNo)
        {
            SearchResultNo = 0;
            while (html.IndexOf(nodeString, nodeString.Length) > -1)
            {
                var result = "";
                //if (html.IndexOf("<div class=\"g\">", "<div class=\"g\">".Length) > -1)
                //{
                    result = html.Substring(0, html.IndexOf(nodeString, nodeString.Length));
                    html = html.Substring(html.IndexOf(nodeString, nodeString.Length));
                //}
                //else
                //{
                //    // 最後の1行
                //    result = html.Substring(0, html.IndexOf("</div>", "</div>".Length));
                //    html = html.Substring(html.IndexOf("</div>", "<div class=\"g\">".Length));
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

        private static (List<抽出文>, string) 関連キーワード(string html, ref List<抽出文> 検索結果TopN, byte SearchResultNo)
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

            return (検索結果TopN, HtmlParse.Htmlタグ除去(html));
        }

        private static (List<抽出文>, string) 必要な文のみ抽出_Pattern2(string html)
        {
            try
            {
                var 検索結果 = new List<抽出文>();

                検索結果.Add(new 抽出文()
                {
                    SearchResultNo = 0,
                    関連キーワード = 関連キーワードFlag.関連キーワードじゃない,
                    文 = HtmlParse.Htmlタグ除去(html)
                });

                return (検索結果, "");
            }
            catch (Exception ex)
            {
                ログ.ログ書き出し(@"GoogleSearchHtmlParse.必要な文のみ抽出() 失敗" + "\r\n" + "html: " + html);
                ログ.ログ書き出し(ex);
                throw;
            }
        }


    }
}
