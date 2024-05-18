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
    public static class MeCabParseMail
    {
        public static void Exec(SqlConnection cn, IntPtr ptrMeCab, DateTime now, out int cnt)
        {
            ログ.ログ書き出し($"MeCabParseMail Exec [Start]");

            // 除外対象文字列を取得
            //var excludedKeywordList = ExcludedKeywordDB.Select();

            // 未解析のメールを取得
            var mailList = DB.Collect.SP_ExtractMail.Select_MeCabState0(cn);
            cnt = mailList.Count;

            foreach (var extractMailRow in mailList)
            {
                var 文節List = StringProcessing.文節抽出_MeCab用3(extractMailRow.英語連結名詞除外後);

                var keywordRowList = new List<KeywordRow>();
                foreach (var 文節 in 文節List)
                {
                    keywordRowList.AddRange(KeywordListUp.Exec(ptrMeCab, CollectTargetCategory.メールマガジン,
                        extractMailRow.CollectTargetNo, null, extractMailRow.SendDate, 文節));
                }

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

                    DB.Collect.BulkCopy_CollectTargetKeyword_Mail_Tmp.Add(extractMailRow.CollectTargetNo,
                        extractMailRow.SendDate, keywordRow2.No, now);
                }

                // メールと出現したキーワードの関連付けをDBに登録
                DB.Collect.BulkCopy_CollectTargetKeyword_Mail_Tmp.Flush(AppSetting.ConnectionString_UnikktleCollect);

                DB.Collect.SP_TmpCollectTargetKeywordMail.InsertIntoSelect(cn);

                DB.Collect.SP_ExtractMail.Update_MeCabState(cn, extractMailRow, State.解析済み, MeCab名詞);
            }

            ログ.ログ書き出し($"MeCabParseMail Exec [End] Count:{cnt}");
        }

    }
}
