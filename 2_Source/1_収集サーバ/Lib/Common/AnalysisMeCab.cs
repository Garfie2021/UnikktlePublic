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
using Logging;
using AppDirectory;



namespace Common
{
    public static class AnalysisMeCab
    {
        //public static List<KeywordRow> 名詞を行毎に抽出(IntPtr ptrMeCab, string 解析元)
        //{
        //    var 名詞List = new List<KeywordRow>();

        //    while (解析元.IndexOf("\n") > -1)
        //    {
        //        var 行 = 解析元.Substring(0, 解析元.IndexOf("\n"));

        //        StringProcessing.英語連結名詞Extraction_行毎(行, ref 英語連結名詞List);

        //        // 形態素解析
        //        var MorphologicalAnalysis = Marshal.PtrToStringAnsi(MeCabConst.mecab_sparse_tostr(ptrMeCab, 解析元));

        //        // 重要語を抽出
        //        名詞List = Analysis.Exec(MorphologicalAnalysis);
        //    }

        //    return 名詞List;
        //}


        //public class Keyword
        //{
        //    public string Word;
        //    public string 形態素解析結果;
        //}
        public static void 開始終了が名詞か判別(string morph, out bool 開始が名詞, out bool 終了が名詞)
        {
            var morph2 = morph.Substring(0, morph.LastIndexOf("\nEOS"));

            // 解析結果を行毎の配列に分ける
            var row = morph2.Split('\n');

            var 表層 = "";

            // 開始が名詞か判別
            開始が名詞 = 名詞判別(row[0], ref 表層);

            if (row.Length == 1)
            {
                終了が名詞 = 開始が名詞;
                return;
            }

            // 終了が名詞か判別
            終了が名詞 = 名詞判別(row[row.Length - 1], ref 表層);
        }

        /// <returns>
        ///  true：名詞
        ///  false：名詞じゃない
        /// </returns>
        public static bool 名詞判別(string morphRow, ref string 表層)
        {
            // 表層形とその他に分ける。
            var Surfacetype = morphRow.Split('\t');

            if (Surfacetype.Length < 2)
            {
                // MeCabが形態素解析できなかった
                return false;
            }

            // 品詞・活用形などに分ける。
            var deteil = Surfacetype[1].Split(',');

            if (deteil[0] == "名詞")
            {
                //if (deteil[1] == "一般" || deteil[1] == "代名詞" || deteil[1] == "動詞非自立的" || 
                //    deteil[1] == "副詞可能" || deteil[1] == "非自立")
                if (deteil[1] == "代名詞" || deteil[1] == "動詞非自立的" || deteil[1] == "副詞可能" ||
                    deteil[1] == "非自立")
                {
                    return false;
                }
            }
            else if (deteil[0] == "記号")
            {
                if (deteil[1] != "アルファベット")
                {
                    return false;
                }
            }
            else if (deteil[0] == "接頭詞")
            {
                if (deteil[1] != "名詞接続")
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

            if (Surfacetype[0].IndexOf("?") > -1)
            {
                // MeCabが形態素解析できなかった
                return false;
            }

            表層 = Surfacetype[0];
            return true;
        }

        public static void Exec(CollectTargetCategory collectTargetCategory,
            long CollectNo, byte? SearchResultNo, DateTime? SendDate, string 文節, string morph, 
            ref List<KeywordRow> keywordRowList)
        {
            var 名詞_前 = "";
            //var 名詞_今 = "";
            var 名詞 = "";
            var 形態素解析結果 = "";
            var 名詞開始 = false;

            // 解析結果を行毎の配列に分ける。
            foreach (var row in morph.Split('\n'))
            {
                //// 表層形とその他に分ける。
                //var Surfacetype = row.Split('\t');

                //if (Surfacetype.Length < 2)
                //{
                //    if (名詞開始)
                //    {
                //        // 名詞の連続が終了
                //        _名詞連結開始(collectTargetCategory, 文節, 名詞, ref keywordRowList);
                //        名詞開始 = false;
                //    }

                //    // 解析スキップ
                //    continue;
                //}

                //// 品詞・活用形などに分ける。
                //var deteil = Surfacetype[1].Split(',');

                //if (deteil[0] == "名詞")
                //{
                //    //名詞Add(collectTargetCategory, 文節, Surfacetype[0], ref keywordRowList);

                //    //if (deteil[1] == "一般" || deteil[1] == "代名詞" || deteil[1] == "動詞非自立的" || 
                //    //    deteil[1] == "副詞可能" || deteil[1] == "非自立")
                //    if (deteil[1] == "代名詞" || deteil[1] == "動詞非自立的" || deteil[1] == "副詞可能" ||
                //        deteil[1] == "非自立")
                //    {
                //        if (名詞開始)
                //        {
                //            // 名詞の連続が終了
                //            _名詞連結開始(collectTargetCategory, 文節, 名詞, ref keywordRowList);
                //            名詞開始 = false;
                //        }

                //        // 名詞でも、代名詞/動詞非自立的/副詞可能/非自立はスキップ
                //        continue;
                //    }
                //}
                //else if (deteil[0] == "記号")
                //{
                //    if (deteil[1] != "アルファベット")
                //    {
                //        if (名詞開始)
                //        {
                //            // 名詞の連続が終了
                //            _名詞連結開始(collectTargetCategory, 文節, 名詞, ref keywordRowList);
                //            名詞開始 = false;
                //        }

                //        // 記号,アルファベット 以外はスキップ
                //        continue;
                //    }
                //}
                //else if (deteil[0] == "接頭詞")
                //{
                //    if (deteil[1] != "名詞接続")
                //    {
                //        if (名詞開始)
                //        {
                //            // 名詞の連続が終了
                //            _名詞連結開始(collectTargetCategory, 文節, 名詞, ref keywordRowList);
                //            名詞開始 = false;
                //        }

                //        // 記号,アルファベット 以外はスキップ
                //        continue;
                //    }
                //}
                //else
                //{
                //    if (名詞開始)
                //    {
                //        // 名詞の連続が終了
                //        _名詞連結開始(collectTargetCategory, 文節, 名詞, ref keywordRowList);
                //        名詞開始 = false;
                //    }

                //    // 他はスキップ
                //    continue;
                //}

                //if (Surfacetype[0].IndexOf("?") > -1)
                //{
                //    // MeCabが形態素解析できなかった
                //    if (名詞開始)
                //    {
                //        // 名詞の連続が終了
                //        _名詞連結開始(collectTargetCategory, 文節, 名詞, ref keywordRowList);
                //        名詞開始 = false;
                //    }

                //    // 解析スキップ
                //    continue;
                //}

                var 表層 = "";

                if (名詞判別(row, ref 表層) == false)
                {
                    if (名詞開始)
                    {
                        // 名詞の連続が終了
                        _名詞連結開始(collectTargetCategory, 文節, 名詞, ref keywordRowList);
                        名詞開始 = false;
                    }

                    continue;
                }

                if (名詞開始)
                {
                    // 名詞連続中
                    名詞連結(名詞_前, 表層, ref 名詞);
                    名詞_前 = 表層;

                    形態素解析結果 += row + "\n";
                }
                else
                {
                    // 名詞開始
                    名詞 = 表層;
                    名詞_前 = 表層;
                    形態素解析結果 = row + "\n";
                    名詞開始 = true;
                }
            }

        }

        public static void 名詞連結(string 名詞_前, string 名詞_今, ref string 名詞)
        {
            // 名詞が連続中
            //if (StringProcessing.全角半角判定(名詞) == 全角半角.半角のみ &&
            //    StringProcessing.全角半角判定(Surfacetype[0]) == 全角半角.半角のみ)
            //{
            //    // MeCab連結名詞が両サイド半角だったらスペース区切りにする

            //    if (// 1つ前
            //        名詞.EndsWith(".") || 名詞.EndsWith(",") || 名詞.EndsWith("@") || Surfacetype[0] == "&" || Surfacetype[0] == "$" || Surfacetype[0] == "\\" || 
            //        // 今回
            //        Surfacetype[0] == "." || Surfacetype[0] == "," || Surfacetype[0] == "@" || Surfacetype[0] == "&" || Surfacetype[0] == "#" || Surfacetype[0] == "%")
            //    {
            //        // MeCabの結果で . と , がスペース区切りになってるバグ対応。
            //        // 7.0対応 => 7. 0対応
            //        // 1,000円 => 1 , 000円
            //        // C#入門 => C #入門
            //        名詞 += Surfacetype[0];
            //    }
            //    else
            //    {
            //        名詞 += " " + Surfacetype[0];
            //    }
            //}
            //else
            //{
            //    名詞 += Surfacetype[0];
            //}

            //if (StringProcessing.全角半角判定(名詞_前) == 全角半角.全角含む ||
            //    StringProcessing.全角半角判定(名詞_今) == 全角半角.全角含む)
            //{
            //    // 前後のどちらかが全角ならそのまま連結
            //    名詞 += 名詞_今;
            //}
            //else
            //{
            //    if (StringProcessing.Is半角アルファベット(名詞_前[0], 名詞_前) &&
            //        StringProcessing.Is半角アルファベット(名詞_今[0], 名詞_今))
            //    {
            //        // 前後のどちらも半角アルファベット始まりなら、半角スペースを挟んで連結
            //        名詞 += " " + 名詞_今;
            //    }
            //    else
            //    {
            //        // それ以外はそのまま連結
            //        名詞 += 名詞_今;
            //    }
            //}

            //if (StringProcessing.Is半角アルファベット(名詞_前[0], 名詞_前) == false &&
            //    StringProcessing.Is半角アルファベット(名詞_今[0], 名詞_今) == false)
            //{
            //    // 両側が全角ならそのまま連結
            //    名詞 += 名詞_今;
            //}
            //else
            //{
            //    // 前後のどちらも、もしくはどちらかが半角アルファベットなら、半角スペースを挟んで連結
            //    名詞 += " " + 名詞_今;
            //}

            名詞 += 名詞_今;
        }

        private static void _名詞連結開始(CollectTargetCategory CollectTargetCategory, string 文節, 
            string 名詞, ref List<KeywordRow> 重要語List)
        {
            //if (名詞開始)
            //{
            //    if (StringProcessing.全角半角判定(名詞) == 全角半角.全角含む)
            //    {
            //        重要語List.Add(new KeywordRow()
            //        {
            //            //TargetCategory = TargetCategory.IT,
            //            CollectTargetCategory = CollectTargetCategory,
            //            名詞区分 = 名詞区分.形態素解析連結名詞,
            //            Word = 名詞,
            //            解析元データ = 文節
            //        });
            //    }
            //    名詞開始 = false;
            //}

            重要語List.Add(new KeywordRow()
            {
                //TargetCategory = TargetCategory.IT,
                CollectTargetCategory = CollectTargetCategory,
                名詞区分 = 名詞区分.形態素解析連結名詞,
                Word = 名詞,
                解析元データ = 文節
            });
        }

        private static void 名詞Add(CollectTargetCategory CollectTargetCategory, string 文節,
            string 名詞, ref List<KeywordRow> 重要語List)
        {
            重要語List.Add(new KeywordRow()
            {
                //TargetCategory = TargetCategory.IT,
                CollectTargetCategory = CollectTargetCategory,
                名詞区分 = 名詞区分.形態素解析名詞,
                Word = 名詞,
                解析元データ = 文節
            });
        }

        //// この後の処理を高速化する為に、この段階で不要な名詞を除外する
        //public static void Excluded(List<string> excludedKeywordListh, ref List<KeywordRow> 重要語List)
        //{
        //    foreach (var word in excludedKeywordListh)
        //    {
        //        // 除外文字列で開始してるキーワードは除外。
        //        重要語List.RemoveAll(x => x.Word.IndexOf(word) == 0);
        //    }
        //}

    }
}
