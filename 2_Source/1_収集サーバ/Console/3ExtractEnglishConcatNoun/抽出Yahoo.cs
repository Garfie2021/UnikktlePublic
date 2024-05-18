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
    public static class 抽出Yahoo
    {
        private static int count = 0;

        public static void Exec(SqlConnection cn, DateTime now)
        {
            var list = DB.Collect.SP_HtmlParseYahoo.Select_State0(cn);
            count = list.Count();

            foreach (var htmlParseRow in list)
            {
                // HTMLタグ除去
                var 抽出文List = ExtractYahoo.必要な文のみ抽出(htmlParseRow);

                // Yahoo検索結果のTop20をDBに登録する。
                foreach (var 抽出文 in 抽出文List)
                {
                    var keywordRowList = new List<KeywordRow>();

                    抽出文.文 = StringProcessing.クレンジング(抽出文.文);

                    var 言語判定 = StringProcessing.言語判定(抽出文.文);

                    // 文節Listの初回抽出と不要文字列除去

                    var 文節List1 = new List<string>();
                    文節List1.AddRange(StringProcessing.改行区切りでList化(抽出文.文));

                    StringProcessing.不要文字列除外(ref 文節List1);
                    var 不要文字列除外後 = string.Join("\r\n", 文節List1.ToArray());  // SQLServerの実行結果をメモ帳で確認する為に \n ではなく \r\n 。


                    // 記号区切り数値抽出

                    var 文節List = StringProcessing.文節抽出_まとめ(文節List1);

                    string 英語連結名詞除外後 = "";
                    string 英語連結名詞 = "";
                    if (言語判定 == 言語判定結果.日本語)
                    {
                        英語連結名詞除外後 = StringProcessing.英語連結名詞Extraction(文節List, ref keywordRowList);

                        // Yahoo検索結果と出現したキーワードの関連付けをDBに登録
                        foreach (var keywordRow in keywordRowList)
                        {
                            var keywordRow2 = DB.Collect.SP_Keyword.GetKeywordWithInsert(cn,
                                CollectTargetCategory.Yahoo検索,
                                htmlParseRow.SearchKeywordNo,
                                抽出文.SearchResultNo,
                                htmlParseRow.SearchDate,
                                null,
                                null,
                                keywordRow.名詞区分,
                                keywordRow.Word,
                                keywordRow.解析元データ);

                            英語連結名詞 += keywordRow.Word + "\r\n";  // SQLServerの実行結果をメモ帳で確認する為に \n ではなく \r\n 。
                            DB.Collect.BulkCopy_CollectTargetKeyword_Yahoo_Tmp.Add(
                                htmlParseRow.SearchKeywordNo,
                                htmlParseRow.SearchDate,
                                抽出文.SearchResultNo, 
                                keywordRow2.No, 
                                now);
                        }
                    }
                    else
                    {
                        英語連結名詞除外後 = string.Join("\r\n", 文節List.ToArray());  // SQLServerの実行結果をメモ帳で確認する為に \n ではなく \r\n 。
                        英語連結名詞 = "";
                    }

                    DB.Collect.BulkCopy_ExtractYahoo.Add(
                        htmlParseRow.SearchKeywordNo,
                        htmlParseRow.SearchDate,
                        抽出文.SearchResultNo,
                        抽出文.関連キーワード,
                        now,
                        言語判定,
                        抽出文.文,
                        不要文字列除外後,
                        英語連結名詞,
                        英語連結名詞除外後);
                }
            }

            DB.Collect.BulkCopy_CollectTargetKeyword_Yahoo_Tmp.Flush(AppSetting.ConnectionString_UnikktleCollect);
            DB.Collect.BulkCopy_ExtractYahoo.Flush(AppSetting.ConnectionString_UnikktleCollect);

            DB.Collect.SP_TmpCollectTargetKeywordYahoo.InsertIntoSelect(cn);

            // 英語連結名詞 抽出済みフラグを設定
            foreach (var row in list)
            {
                DB.Collect.SP_HtmlParseYahoo.Update_State(cn, row, State.解析済み);
            }

            ログ.ログ書き出し($"抽出Yahoo End. Count{count}");
        }
    }
}
