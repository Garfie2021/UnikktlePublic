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
    public static class ExtractBing
    {
        public static List<抽出文> 必要な文のみ抽出(HtmlParseBingRow row)
        {
            if (row.HtmlTag除外後2段階目.IndexOf("<li class=\"b_algo\">") > -1)
            {
                return 必要な文のみ抽出_Pattern1(row.HtmlTag除外後2段階目);
            }
            else
            {
                ExtractEnglishConcatNounCommon.Warning("Bing",
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
                ログ.ログ書き出し(@"GoogleSearchHtmlParse.必要な文のみ抽出() 失敗" + "\r\n" + "html: " + html);
                ログ.ログ書き出し(ex);
                throw;
            }
        }

        private static string TopNを抽出(string html, ref List<抽出文> 検索結果TopN, out byte SearchResultNo)
        {
            // 1件毎に分割
            SearchResultNo = 0;
            while (html.IndexOf("<li class=\"b_algo\">", "<li class=\"b_algo\">".Length) > -1)
            {
                var result = "";
                //if (html.IndexOf("<li class=\"b_algo\">", "<li class=\"b_algo\">".Length) > -1)
                //{
                    result = html.Substring(0, html.IndexOf("<li class=\"b_algo\">", "<li class=\"b_algo\">".Length));
                    html = html.Substring(html.IndexOf("<li class=\"b_algo\">", "<li class=\"b_algo\">".Length));
                //}
                //else
                //{
                //    // 最後の1行
                //    result = html.Substring(0, html.IndexOf("</li>", "</li>".Length));
                //    html = html.Substring(html.IndexOf("</li>", "</li>".Length));
                //}

                //// 「このページを翻訳」リンクを除去
                //if (result.IndexOf("<div class=\"b_attribution\"") > -1)
                //{
                //    var result2 = result.Substring(0, result.IndexOf("<div class=\"b_attribution\""));
                //    result = result.Substring(result.IndexOf("<div class=\"b_attribution\"") + "<div class=\"b_attribution\"".Length);
                //    result2 += result.Substring(result.IndexOf("</div>") + "</div>".Length);
                //    result = result2;
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

        private static void 関連キーワード(string html, ref List<抽出文> 検索結果TopN, byte SearchResultNo)
        {
            // フッター
            if (html.IndexOf("<div class=\"b_rs\">") > -1)
            {
                html = html.Substring(html.IndexOf("<div class=\"b_rs\">"));

                if (html.IndexOf("<div class=\"b_rich\">") > -1)
                {
                    // 「関連する検索」セクション
                    html = html.Substring(html.IndexOf("<div class=\"b_rich\">"));

                    検索結果TopN.Add(new 抽出文()
                    {
                        SearchResultNo = SearchResultNo,
                        関連キーワード = 関連キーワードFlag.関連キーワード,
                        文 = HtmlParse.Htmlタグ除去(html)
                    });
                }
                else if (html.IndexOf("<ul class=\"b_vList\">") > -1)
                {
                    // 「関連キーワード」セクション
                    html = html.Substring(html.IndexOf("<ul class=\"b_vList\">"));

                    検索結果TopN.Add(new 抽出文()
                    {
                        SearchResultNo = SearchResultNo,
                        関連キーワード = 関連キーワードFlag.関連キーワード,
                        文 = HtmlParse.Htmlタグ除去(html)
                    });
                }
            }
        }


    }
}
