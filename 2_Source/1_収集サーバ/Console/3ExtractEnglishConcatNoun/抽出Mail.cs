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
    public static class 抽出Mail
    {
        private static int count = 0;

        public static void Exec(SqlConnection cn, DateTime now)
        {
            var list = DB.Collect.SP_CollectMail.Select_State0(cn);
            count = list.Count();

            foreach (var htmlParseRow in list)
            {
                try
                {
                    var keywordRowList = new List<KeywordRow>();

                    KeywordRow keywordRowTemplate = new KeywordRow()
                    {
                        CollectTargetCategory = CollectTargetCategory.メールマガジン,
                        CollectNo = htmlParseRow.CollectTargetNo,
                        SearchResultNo = null,
                        SendDate = htmlParseRow.SendDate,
                    };

                    var 解析元Subject = StringProcessing.クレンジング(htmlParseRow.CurrentSubject);
                    var 解析元Body = StringProcessing.クレンジング(htmlParseRow.CurrentBody);

                    // 言語判定
                    var 言語判定 = StringProcessing.言語判定(htmlParseRow.CurrentBody);


                    // 文節Listの初回抽出と不要文字列除去

                    var 文節List1 = new List<string>();
                    文節List1.AddRange(StringProcessing.改行区切りでList化(解析元Subject));
                    文節List1.AddRange(StringProcessing.改行区切りでList化(解析元Body));

                    StringProcessing.不要文字列除外(ref 文節List1);
                    var 不要文字列除外後 = string.Join("\r\n", 文節List1.ToArray());  // SQLServerの実行結果をメモ帳で確認する為に \n ではなく \r\n 。


                    // 英語連結名詞抽出

                    var 文節List = StringProcessing.文節抽出_まとめ(文節List1);

                    string 英語連結名詞除外後 = "";
                    string 英語連結名詞 = "";
                    if (言語判定 == 言語判定結果.日本語)
                    {
                        英語連結名詞除外後 = StringProcessing.英語連結名詞Extraction(文節List, ref keywordRowList);

                        // メールマガジンと出現したキーワードの関連付けをDBに登録
                        foreach (var keywordRow in keywordRowList)
                        {
                            var keywordRow2 = DB.Collect.SP_Keyword.GetKeywordWithInsert(cn,
                                CollectTargetCategory.メールマガジン,
                                htmlParseRow.CollectTargetNo,
                                null,
                                htmlParseRow.SendDate,
                                null,
                                null,
                                keywordRow.名詞区分,
                                keywordRow.Word,
                                keywordRow.解析元データ);

                            英語連結名詞 += keywordRow.Word + "\r\n";  // SQLServerの実行結果をメモ帳で確認する為に \n ではなく \r\n 。
                            DB.Collect.BulkCopy_CollectTargetKeyword_Mail_Tmp.Add(
                                htmlParseRow.CollectTargetNo,
                                htmlParseRow.SendDate,
                                keywordRow2.No,
                                now);
                        }
                    }
                    else
                    {
                        英語連結名詞除外後 = string.Join("\r\n", 文節List.ToArray());  // SQLServerの実行結果をメモ帳で確認する為に \n ではなく \r\n 。
                        英語連結名詞 = "";
                    }

                    DB.Collect.BulkCopy_ExtractMail.Add(
                        htmlParseRow.CollectTargetNo,
                        htmlParseRow.SendDate,
                        htmlParseRow.登録日時,
                        言語判定,
                        不要文字列除外後,
                        英語連結名詞,
                        英語連結名詞除外後);
                }
                catch (Exception ex)
                {
                    ログ.ログ書き出し("[Message]" + ex.Message + "[StackTrace]" + ex.StackTrace);

                    ログ.Write_ExtractEnglishConcatNoun_ErrorMail(htmlParseRow.CollectTargetNo + "_" + htmlParseRow.SendDate.ToLongDateString(), 
                        "\r\n[解析元 データ]\r\n" +
                        "CollectTargetNo: " + htmlParseRow.CollectTargetNo + "\r\n\r\n" +
                        "SendDate: " + htmlParseRow.SendDate + "\r\n\r\n" +
                        "CurrentSubject: " + htmlParseRow.CurrentSubject + "\r\n\r\n" +
                        "CurrentBody: " + htmlParseRow.CurrentBody + "\r\n\r\n" +
                        "\r\n\r\n");
                }
            }

            DB.Collect.BulkCopy_CollectTargetKeyword_Mail_Tmp.Flush(AppSetting.ConnectionString_UnikktleCollect);
            DB.Collect.BulkCopy_ExtractMail.Flush(AppSetting.ConnectionString_UnikktleCollect);

            DB.Collect.SP_TmpCollectTargetKeywordMail.InsertIntoSelect(cn);

            // 英語連結名詞 抽出済みフラグを設定
            foreach (var row in list)
            {
                DB.Collect.SP_CollectMail.Update_State(cn,
                    row.CollectTargetNo,
                    row.SendDate,
                    row.登録日時,
                    State.解析済み);
            }

            ログ.ログ書き出し($"抽出Mail End. Count{count}");

        }
    }
}
