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

namespace ExtractEnglishConcatNoun
{
    public static class 抽出WebPage
    {
        private static int count = 0;

        public static void Exec(SqlConnection cn, DateTime now)
        {
            var table = DB.Collect.SP_HtmlParseWebPage.Select_State0(cn);
            count = table.Rows.Count;

            foreach (DataRow htmlParseRow in table.Rows)
            {
                var domainNo = (long)htmlParseRow["DomainNo"];
                var urlNo = (long)htmlParseRow["UrlNo"];
                var 言語判定 = (言語判定結果)htmlParseRow["言語判定"];
                var htmlTag除外後2段階目 = (string)htmlParseRow["HtmlTag除外後2段階目"];

                var keywordRowList = new List<KeywordRow>();

                var 抽出文 = StringProcessing.クレンジング(htmlTag除外後2段階目);

                // 文節Listの初回抽出と不要文字列除去

                var 文節List1 = new List<string>();
                文節List1.AddRange(StringProcessing.改行区切りでList化(抽出文));

                StringProcessing.不要文字列除外(ref 文節List1);
                var 不要文字列除外後 = string.Join("\r\n", 文節List1.ToArray());  // SQLServerの実行結果をメモ帳で確認する為に \n ではなく \r\n 。


                // 英語連結名詞抽出

                var 文節List = StringProcessing.文節抽出_まとめ(文節List1);

                string 英語連結名詞除外後 = "";
                string 英語連結名詞 = "";

                英語連結名詞除外後 = StringProcessing.英語連結名詞Extraction(文節List, ref keywordRowList);

                // WebPage検索結果と出現したキーワードの関連付けをDBに登録
                foreach (var keywordRow in keywordRowList)
                {
                    var keywordRow2 = DB.Collect.SP_Keyword.GetKeywordWithInsert(cn,
                        CollectTargetCategory.WebPage収集,
                        null,
                        null,
                        null,
                        domainNo,
                        urlNo,
                        keywordRow.名詞区分,
                        keywordRow.Word,
                        keywordRow.解析元データ);

                    英語連結名詞 += keywordRow.Word + "\r\n";  // SQLServerの実行結果をメモ帳で確認する為に \n ではなく \r\n 。
                    DB.Collect.BulkCopy_CollectTargetKeyword_WebPage_Tmp.Add(
                        domainNo,
                        urlNo,
                        keywordRow2.No,
                        now);
                }

                if (DB.Collect.SP_ExtractWebPage.GetCount(cn, domainNo, urlNo) < 1)
                {
                    // 初回
                    DB.Collect.BulkCopy_ExtractWebPage.Add(
                        domainNo,
                        urlNo,
                        now,
                        言語判定,
                        抽出文,
                        不要文字列除外後,
                        英語連結名詞,
                        英語連結名詞除外後);
                }
                else
                {
                    // 2回目以降
                    DB.Collect.SP_ExtractWebPage.Update_英語連結名詞(cn,
                        domainNo,
                        urlNo,
                        now,
                        言語判定,
                        抽出文,
                        不要文字列除外後,
                        英語連結名詞,
                        英語連結名詞除外後);
                }
            }

            DB.Collect.BulkCopy_CollectTargetKeyword_WebPage_Tmp.Flush(AppSetting.ConnectionString_UnikktleCollect);
            DB.Collect.BulkCopy_ExtractWebPage.Flush(AppSetting.ConnectionString_UnikktleCollect);

            DB.Collect.SP_TmpCollectTargetKeywordWebPage.InsertIntoSelect(cn);

            // 英語連結名詞 抽出済みフラグを設定
            foreach (DataRow row in table.Rows)
            {
                DB.Collect.SP_HtmlParseWebPage.Update_State(cn, row, State.解析済み);
            }

            ログ.ログ書き出し($"抽出WebPage End. Count{count}");
        }

    }
}
