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
    public static class KeywordListUp
    {

        public static List<KeywordRow> Exec(IntPtr ptrMeCab, CollectTargetCategory collectTargetCategory, 
            long SearchKeywordNo, byte? SearchResultNo, DateTime? SendDate, string 文節)
        {
            // ※MeCabが半角スペースを除外する対策。
            // 半角スペース毎に分割して形態素解析し、半角スペース前後の単語が名詞対象だったら、
            // 半角スペースを間に挟んで名詞連結する。
            //var 半角スペースIndexStart = 0;
            var 形態素解析結果List = new List<string>();
            var 文節List = StringProcessing.記号毎に文を分割(文節, ' ');

            // 形態素解析
            foreach (var 文 in 文節List)
            {
                形態素解析結果List.Add(Marshal.PtrToStringAnsi(MeCabConst.mecab_sparse_tostr(ptrMeCab, 文)));
            }

            // 重要語を抽出

            var 前回_開始が名詞 = false;
            var 前回_終了が名詞 = false;
            var 今回_開始が名詞 = false;
            var 今回_終了が名詞 = false;
            var keywordRowList = new List<KeywordRow>();
            foreach (var morph in 形態素解析結果List)
            {
                var keywordRowListtmp = new List<KeywordRow>();

                AnalysisMeCab.Exec(collectTargetCategory, SearchKeywordNo,
                    SearchResultNo, SendDate, 文節, morph, ref keywordRowListtmp);

                AnalysisMeCab.開始終了が名詞か判別(morph, out 今回_開始が名詞, out 今回_終了が名詞);

                if (前回_終了が名詞 && 今回_開始が名詞)
                {
                    // 前回、最後の単語が名詞判定で、今回、最初の単語が名詞判定なら、
                    // 半角スペースを挟んで連結する。

                    // 一つ前のkeywordRowに追記して入れ替える

                    var aa = keywordRowList[keywordRowList.Count - 1];
                    var bb = keywordRowListtmp[0];

                    keywordRowList.RemoveAt(keywordRowList.Count - 1);

                    keywordRowList.Add(new KeywordRow()
                    {
                        //No = 
                        CollectTargetCategory = aa.CollectTargetCategory,
                        CollectNo = aa.CollectNo,
                        SearchResultNo = aa.SearchResultNo,
                        SendDate = aa.SendDate,
                        //採用 = 
                        //採用判定済み = 
                        名詞区分 = aa.名詞区分,
                        登録日時 = aa.登録日時,
                        更新日時 = aa.更新日時,
                        //Google検索日時 = aa.
                        //Yahoo検索日時 = aa.
                        //Bing検索日時 = aa.
                        Word = aa.Word + " " + bb.Word,
                        解析元データ = aa.解析元データ + " " + bb.解析元データ,
                    });

                    // 連結した最初のkeywordRowを削除して、後は全て追加する
                    keywordRowListtmp.RemoveAt(0);
                    keywordRowList.AddRange(keywordRowListtmp);
                }
                else
                {
                    keywordRowList.AddRange(keywordRowListtmp);
                }

                前回_終了が名詞 = 今回_終了が名詞;
                前回_開始が名詞 = 今回_開始が名詞;
            }

            return keywordRowList;
        }


    }
}
