using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Runtime.InteropServices;
using 定数;
using Common;
using Logging;
using AppDirectory;
using DB;


namespace MeCabExec
{
    public static class MeCabParseBing
    {
        public static void Exec(SqlConnection cn, IntPtr ptrMeCab, DateTime now, 
            out int cntList, out long CntMeCabState0, out long Cnt関連キーワード以外, out long Cnt日本語, out long Cnt英語)
        {
            ログ.ログ書き出し($"MeCabParseBing Exec [Start]");

            // 未解析のメールを取得
            //var excludedKeywordList = ExcludedKeywordDB.Select();

            // 未解析のBing検索結果を取得
            var bingList = DB.Collect.SP_ExtractBing.Select_MeCabState0(cn,
                out CntMeCabState0, out Cnt関連キーワード以外, out Cnt日本語, out Cnt英語);
            cntList = bingList.Count;

            ログ.ログ書き出し($"BingList.Count:{bingList.Count}");

            foreach (var extractBingRow in bingList)
            {
                var 文節List = StringProcessing.文節抽出_MeCab用3(extractBingRow.英語連結名詞除外後);

                //ログ.ログ書き出し($"文節List.Count:{文節List.Count}");

                var keywordRowList = new List<KeywordRow>();
                foreach (var 文節 in 文節List)
                {
                    keywordRowList.AddRange(KeywordListUp.Exec(ptrMeCab, CollectTargetCategory.Bing検索,
                        extractBingRow.SearchKeywordNo, extractBingRow.SearchResultNo, null, 文節));
                }

                //ログ.ログ書き出し($"MeCabParseBing Exec keywordRowList Count:{keywordRowList.Count}");

                // 重要語から不要な名詞を除外
                //Analysis.Excluded(excludedKeywordList, ref keywordRowList);

                var MeCab名詞 = "";
                foreach (var keywordRow in keywordRowList)
                {
                    // 照合順序がSQLServerとC#で違う為、キーワードの比較はSQLServerで行う。
                    var keywordRow2 = DB.Collect.SP_Keyword.GetKeywordWithInsert(cn,
                        keywordRow.CollectTargetCategory,
                        keywordRow.CollectNo,
                        keywordRow.SearchResultNo,
                        keywordRow.SendDate,
                        null,
                        null,
                        keywordRow.名詞区分,
                        keywordRow.Word,
                        keywordRow.解析元データ);

                    //if (keywordRow2.採用 == 0 && keywordRow2.採用判定済み == 1)
                    //{
                    //    // 不採用であればスキップ
                    //    continue;
                    //}

                    MeCab名詞 += keywordRow.Word + "\r\n";  // SQLServerの実行結果をメモ帳で確認する為に \n ではなく \r\n 

                    DB.Collect.BulkCopy_CollectTargetKeyword_Bing_Tmp.Add(extractBingRow.SearchKeywordNo,
                        extractBingRow.SearchDate, extractBingRow.SearchResultNo, keywordRow2.No, now);
                }

                //ログ.ログ書き出し($"MeCabParseBing Exec BulkCopy_CollectTargetKeyword_Bing_Tmp.Data.Rows.Count:{BulkCopy_CollectTargetKeyword_Bing_Tmp.Data.Rows.Count}");

                // メールと出現したキーワードの関連付けをDBに登録
                DB.Collect.BulkCopy_CollectTargetKeyword_Bing_Tmp.Flush(AppSetting.ConnectionString_UnikktleCollect);

                DB.Collect.SP_TmpCollectTargetKeywordBing.InsertIntoSelect(cn);

                DB.Collect.SP_ExtractBing.Update_MeCabState(cn, extractBingRow, State.解析済み, MeCab名詞);
            }

            ログ.ログ書き出し($"MeCabParseBing Exec [End] Count:{cntList}");
        }
    }
}
