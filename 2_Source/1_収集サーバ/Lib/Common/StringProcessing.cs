using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using 定数;
using Logging;
using AppDirectory;
using Common;



namespace Common
{
    // 文字列処理系
    public static class StringProcessing
    {
        //public static Regex 英語連結名詞有無判定Regex = new Regex(@"\w\s\w", RegexOptions.Compiled); // 部分一致
        private static Regex 英語連結名詞有無判定Regex = new Regex(@"[\x20-\x7E]+\s[\x20-\x7E]+", RegexOptions.Compiled); // 部分一致
        //private static Regex 半角判定Regex = new Regex(@"[\x20-\x7E]+", RegexOptions.Compiled);
        //private static Regex 全角判定Regex = new Regex(@"[^\x01-\x7E]", RegexOptions.Compiled);
        private static Regex スペース判定Regex = new Regex(@"\s", RegexOptions.Compiled);

        private static string 日本語 = "、。あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほまみむめもやゆよらりるれろわをん";

        // 記号の除去が上手くいかない。今後の課題
        //public static Regex 最初文字列記号のみ空白判定Regex = new Regex(@"^[*-?(=!]+\s", RegexOptions.Compiled);
        //public static Regex 最後文字列記号のみ空白判定Regex = new Regex(@"\s[*-?(=!]+$", RegexOptions.Compiled);
        //public static Regex 最初文字列記号のみ判定Regex = new Regex(@"^[*-?=!]+", RegexOptions.Compiled);
        //public static Regex 最後文字列記号のみ判定Regex = new Regex(@"[*-?=!]+$", RegexOptions.Compiled);

        // 両側が数値だったら文節区切りしない。じゃなかったら文節区切りする。
        private static List<char> 文節区切り文字List_第１段階 = new List<char>() { ',', '.', '/' };

        // 両側がアルファベットだったら文節区切りしない。じゃなかったら文節区切りする。
        //private static List<string> 文節区切り文字List_第２段階 = new List<string>() { "'", "’", "-" };
        //private static List<string> 文節区切り文字List_第２段階 = new List<string>() { "'", "’" };
        private static List<char> 文節区切り文字List_第２段階 = new List<char>() { '\'', '-' };

        // スペースが3つ連続している場合、英語連結名詞になる可能性は低いので、改行(\n)と同じ扱いで、文章を分割する。
        // 「.」を文区切り文字にすると「.Net」が抽出されなくなってしまう
        //public static List<string> 文区切り文字List = new List<string>() { "   ", ",", ".", "、", "。" };
        //private static List<string> 文節区切り文字List_第３段階 = new List<string>() { "   ", "|", "+", "*", ";", ":", "=", "[", "]", "(", ")", "<", ">", "«", "»", "｢", "｣", "~", "!", "\"", "?", "・", "，", "．", "、", "。", "”", "「", "」", "『", "』", "【", "】", "［", "］", "（", "）", "｛", "｝", "≪", "≫", "━", "―", "　", "！", "？" };
        //private static List<char> 文節区切り文字List_第３段階 = new List<char>() { "   ", "|", ";", ":", "=", "[", "]", "(", ")", "<", ">", "«", "»", "｢", "｣", "~", "!", "\"", "?", "・", "，", "．", "、", "。", "”", "「", "」", "『", "』", "【", "】", "［", "］", "（", "）", "｛", "｝", "≪", "≫", "━", "―", "　", "！", "？" };
        private static List<char> 文節区切り文字List_第３段階 = new List<char>() { '|', ';', ':', '=', '[', ']', '(', ')', '<', '>', '«', '»', '｢', '｣', '~', '!', '\'', '?', '・', '，', '．', '、', '。', '”', '「', '」', '『', '』', '【', '】', '［', '］', '（', '）', '｛', '｝', '≪', '≫', '━', '―', '　', '！', '？' };

        //// 英語連結名詞除外後は、半角スペースも文節区切りになる。
        //private static List<string> 文節区切り文字List_第４段階 = new List<string>() { " " };

        private static List<string> 連続スペース文字List = new List<string>() { "  ", "　　" };

        public static List<string> 記号毎に文を分割(string 文節, char 記号)
        {
            var aa = new List<string>();

            if (文節.IndexOf(記号) < 0)
            {
                aa.Add(文節);
                return aa;
            }

            var IndexStart = 0;
            var IndexEnd = 0;
            while ((IndexEnd = 文節.IndexOf(記号, IndexStart)) > -1)
            {
                IndexEnd += 1; // 記号はcharなので1。

                var 文1 = 文節.Substring(IndexStart, IndexEnd - IndexStart).Trim();

                if (string.IsNullOrEmpty(文1))
                {
                    IndexStart = IndexEnd;
                    continue;
                }

                aa.Add(文1);
                IndexStart = IndexEnd;
            }

            var 文2 = 文節.Substring(文節.LastIndexOf(記号)).Trim();

            if (string.IsNullOrEmpty(文2) == false)
            {
                aa.Add(文2);
            }

            return aa;
        }

        // 英語連結名詞の抽出処理で、文節毎に「\n」で分割済みなので、「\n」で分割するのみ。
        public static List<string> 文節抽出_MeCab用3(string 解析元)
        {
            解析元 = 解析元.Replace("\r\n", "\n");

            //ログ.ログ書き出し($"解析元\n{解析元}\n");

            var 文節抽出List = new List<string>();
            var indexStart = 0;

            while (indexStart < 解析元.Length)
            {
                var indexEnd = 解析元.IndexOf("\n", indexStart);

                if (indexStart == indexEnd)
                {
                    indexStart = indexEnd + "\n".Length;
                    continue;
                }

                if (indexEnd < 0)
                {
                    var bb = 解析元.Substring(indexStart);
                    if (!string.IsNullOrEmpty(bb))
                    {
                        文節抽出List.Add(bb);
                    }

                    break;
                }

                var aa = 解析元.Substring(indexStart, indexEnd - indexStart);
                if (!string.IsNullOrEmpty(aa))
                {
                    文節抽出List.Add(aa);
                }

                indexStart = indexEnd + "\n".Length;
            }

            return 文節抽出List;
        }

        public static List<string> 文節抽出_まとめ(List<string> 文節List)
        {
            // 文節区切り

            //解析元 = 解析元.Replace("\r\n", " ").Replace("\n", " ").Replace("\t", " ").Replace("　", " ");

            var 文節List2 = new List<string>();
            foreach (var tmp in 文節List)
            {
                文節List2.AddRange(文節分割_連続スペース(tmp));
            }

            var 文節List3 = new List<string>();
            foreach (var tmp in 文節List2)
            {
                文節List3.AddRange(文節抽出_記号区切り数値外(tmp));
            }

            var 文節List4 = new List<string>();
            foreach (var tmp in 文節List3)
            {
                文節List4.AddRange(文節抽出_英語省略形外(tmp));
            }

            var 文節List5 = new List<string>();
            foreach (var tmp in 文節List4)
            {
                文節List5.AddRange(文節分割(tmp));
            }

            return 文節List5;
        }


        //public static List<string> 文節抽出_MeCab用(string 解析元)
        //{
        //    //ログ.ログ書き出し($"解析元\n{解析元}\n");

        //    try
        //    {
        //        var 文節抽出List = new List<string>();

        //        // 英語連結名詞の抽出処理で、文節毎に「\n」で分割済みなので、「\n」毎に形態素解析する。
        //        var cnt最大実行数 = 0;
        //        while (解析元.IndexOf("\n") > -1)
        //        {
        //            var 行 = 解析元.Substring(0, 解析元.IndexOf("\n"));

        //            //// 大きな文字列を一気に置換すると、C#の限界でバグるので、文節単位に行う。
        //            //行 = 特殊記号除去(行, " ");
        //            //行 = 全角特殊記号除去(行, " ");

        //            文節抽出_MeCab用2(行, ref 文節抽出List);

        //            解析元 = 解析元.Substring(解析元.IndexOf("\n") + "\n".Length);

        //            if (cnt最大実行数++ > 1000)
        //            {
        //                // 最大実行数を超えたら、無限ループ回避の為に終了する。
        //                ログ.ログ書き出し($"文節抽出_MeCab用()失敗。 cnt最大実行数:{cnt最大実行数}   解析元: {解析元}\r\n");

        //                break;
        //            }
        //        }

        //        文節抽出_MeCab用2(解析元, ref 文節抽出List);

        //        return 文節抽出List;
        //    }
        //    catch (Exception ex)
        //    {
        //        ログ.ログ書き出し(@"ExtractEnglishConcatNoun.英語連結名詞を行毎に抽出() 失敗" + "\r\n" + "解析元: " + 解析元);
        //        ログ.ログ書き出し(ex);
        //        throw;
        //    }
        //}

        //// MeCabが半角スペースを除外してしまうので、半角スペースを文節区切りとして扱う。
        //private static void 文節抽出_MeCab用2(string 行, ref List<string> 文節抽出List)
        //{
        //    var cnt最大実行数 = 0;
        //    while (行.IndexOf(" ") > -1)
        //    {
        //        var 文 = 行.Substring(0, 行.IndexOf(" "));

        //        if (!string.IsNullOrEmpty(文))
        //        {
        //            文節抽出List.AddRange(文節分割(文));
        //        }

        //        行 = 行.Substring(行.IndexOf(" ") + " ".Length);

        //        if (cnt最大実行数++ > 100)
        //        {
        //            // 最大実行数を超えたら、無限ループ回避の為に終了する。
        //            ログ.ログ書き出し($"文節抽出_MeCab用2()失敗。 cnt最大実行数:{cnt最大実行数}   行: {行}\r\n");

        //            break;
        //        }
        //    }

        //    if (!string.IsNullOrEmpty(行))
        //    {
        //        文節抽出List.AddRange(文節分割(行));
        //    }
        //}

        // 「, . /」それぞれの両側が数値じゃなかったら、文節区切りする。下記のバグを解消したい。
        // Oracle Database SQL言語リファレンス, 11gリリース2  =>  , 11g
        public static List<string> 文節抽出_記号区切り数値外(string 解析元)
        {
            var 文節IndexList = new List<int>();

            foreach (var 記号 in 文節区切り文字List_第１段階)
            {
                var 記号Index = 0;
                while ((記号Index = 解析元.IndexOf(記号, 記号Index)) > -1)
                {
                    if (記号Index == 0)
                    {
                        文節IndexList.Add(記号Index);
                        記号Index++;
                        continue;
                    }
                    else if (記号Index + 1 == 解析元.Length)
                    {
                        文節IndexList.Add(記号Index);
                        記号Index++;
                        continue;
                    }

                    if (// 記号の1つ前が数値か判定
                        char.IsNumber(解析元.Substring(記号Index - 1, 1)[0]) &&
                        // 記号の1つ後が数値か判定
                        char.IsNumber(解析元.Substring(記号Index + 1, 1)[0]))
                    {
                        記号Index++;
                        continue;
                    }

                    文節IndexList.Add(記号Index);
                    記号Index++;

                    if (解析元.Length < 記号Index)
                    {
                        break;
                    }
                }
            }

            return 文節抽出(解析元, 文節IndexList);
        }

        // 「's」の英語複数系が、文節区切りされるバグを解消したい。
        // View all Oracle’s hardware and software products　=> View all Oracle
        // View all Oracle’s hardware and software products　=> s hardware and software products
        public static List<string> 文節抽出_英語省略形外(string 解析元)
        {
            var 文節IndexList = new List<int>();

            foreach (var 記号 in 文節区切り文字List_第２段階)
            {
                var 記号Index = 0;

                while ((記号Index = 解析元.IndexOf(記号, 記号Index)) > -1)
                {
                    if (記号Index == 0)
                    {
                        文節IndexList.Add(記号Index);
                        記号Index++;
                        continue;
                    }
                    else if (記号Index + 1 == 解析元.Length)
                    {
                        文節IndexList.Add(記号Index);
                        記号Index++;
                        continue;
                    }

                    var 前 = 解析元.Substring(記号Index - 1, 1);
                    var 後 = 解析元.Substring(記号Index + 1, 1);

                    if (// 記号の1つ前が数値か判定
                        Is半角アルファベット(前[0], 前) &&
                        // 記号の1つ後が数値か判定
                        Is半角アルファベット(前[0], 後))
                    {
                        記号Index++;
                        continue;
                    }

                    文節IndexList.Add(記号Index);
                    記号Index++;

                    if (解析元.Length < 記号Index)
                    {
                        break;
                    }
                }
            }

            return 文節抽出(解析元, 文節IndexList);
        }

        public static List<string> 文節抽出(string 解析元, List<int> 文節IndexList)
        {
            var 文節list = new List<string>();

            if (文節IndexList.Count() < 1)
            {
                文節list.Add(解析元.Trim());
                return 文節list;
            }
            else
            {
                文節IndexList.Sort();

                var 開始Index = 0;
                foreach (var index in 文節IndexList)
                {
                    var aaa = 解析元.Substring(開始Index, index - 開始Index).Trim();

                    if (string.IsNullOrEmpty(aaa))
                    {
                        開始Index = index + 1;
                        continue;
                    }

                    文節list.Add(aaa);
                    開始Index = index + 1;
                }

                var bbb = 解析元.Substring(開始Index).Trim();
                if (!string.IsNullOrEmpty(bbb))
                {
                    文節list.Add(bbb);
                }

                return 文節list;
            }
        }

        public static string クレンジング(string 解析元)
        {
            try
            {
                解析元 = HttpUtility.UrlDecode(HttpUtility.HtmlDecode(解析元));

                解析元 = 解析元.Replace("\r\n", "\n");
                解析元 = 解析元.Replace('…', '\n');

                // "’"の前後がアルファベットなら半角に置換する
                var 記号Index = 0;
                while ((記号Index = 解析元.IndexOf("’", 記号Index)) > -1)
                {
                    var 前 = 解析元.Substring(記号Index - 1, 1);
                    var 後 = 解析元.Substring(記号Index + 1, 1);
                
                    if (Is半角アルファベット(前[0], 前) &&    // 記号の1つ前が数値か判定
                        Is半角アルファベット(後[0], 後))      // 記号の1つ後が数値か判定
                    {
                        解析元 =  解析元.Substring(0, 記号Index) + "'" + 解析元.Substring(記号Index + 1);
                    }

                    記号Index++;
                }

                return 解析元;
            }
            catch (Exception ex)
            {
                ログ.ログ書き出し(ex.Message + @"\r\n[解析元 データ]\r\n" + 解析元 + "\r\n\r\n");

                throw;
            }
        }

        public static List<string> 改行区切りでList化(string 解析元)
        {
            // Windowsの改行コードを、Unixの改行コードに置換。
            // \tは改行コードに置換し、改行と見なす。
            解析元 = 解析元.Replace("\r\n", "\n").Replace("\t", "\n");

            var 文節抽出List = new List<string>();

            while (解析元.IndexOf("\n") > -1)
            {
                var 行 = 解析元.Substring(0, 解析元.IndexOf("\n"));

                if (!string.IsNullOrEmpty(行))
                {
                    文節抽出List.Add(行);
                }

                解析元 = 解析元.Substring(解析元.IndexOf("\n") + "\n".Length);
            }

            if (!string.IsNullOrEmpty(解析元))
            {
                文節抽出List.Add(解析元);
            }

            return 文節抽出List;
        }

        public static List<string> 文節分割_連続スペース(string 行)
        {
            var 文節List = new List<string>() { 行 };

            foreach (var 文区切り文字 in 連続スペース文字List)
            {
                var 解析単位文字列ListAfter = new List<string>();
                foreach (var 解析単位文字列2 in 文節List)
                {
                    var 解析単位文字列 = 解析単位文字列2;
                    while (解析単位文字列.IndexOf(文区切り文字) > -1)
                    {
                        解析単位文字列ListAfter.Add(解析単位文字列.Substring(0, 解析単位文字列.IndexOf(文区切り文字)).Trim());
                        解析単位文字列 = 解析単位文字列.Substring(解析単位文字列.IndexOf(文区切り文字) + 文区切り文字.Length).Trim();
                    }

                    if (!string.IsNullOrEmpty(解析単位文字列))
                    {
                        解析単位文字列ListAfter.Add(解析単位文字列);
                    }
                }

                if (解析単位文字列ListAfter.Count() > 0)
                {
                    文節List = 解析単位文字列ListAfter;
                }
                else
                {
                    文節List = new List<string>() { 行 };
                }
            }

            return 文節List;
        }

        public static List<string> 文節分割(string 行)
        {
            var 文節List = new List<string>() { 行 };

            foreach (var 文区切り文字 in 文節区切り文字List_第３段階)
            {
                var 解析単位文字列ListAfter = new List<string>();
                foreach (var 解析単位文字列2 in 文節List)
                {
                    var 解析単位文字列 = 解析単位文字列2;
                    while (解析単位文字列.IndexOf(文区切り文字) > -1)
                    {
                        解析単位文字列ListAfter.Add(解析単位文字列.Substring(0, 解析単位文字列.IndexOf(文区切り文字)));
                        解析単位文字列 = 解析単位文字列.Substring(解析単位文字列.IndexOf(文区切り文字) + 1); // 記号はcharなので1。
                    }

                    if (!string.IsNullOrEmpty(解析単位文字列))
                    {
                        解析単位文字列ListAfter.Add(解析単位文字列);
                    }
                }

                if (解析単位文字列ListAfter.Count() > 0)
                {
                    文節List = 解析単位文字列ListAfter;
                }
                else
                {
                    文節List = new List<string>() { 行 };
                }
            }

            // 不要なものは除去
            //文節List.RemoveAll(x => x.IndexOf("http") > -1);  // URL
            //文節List.RemoveAll(x => x.IndexOf("href=\"") > -1);  // TAG
            //文節List.RemoveAll(x => x.IndexOf("@") > -1 && x.IndexOf(".com") > -1);  // メールアドレス
            不要文字列除外(ref 文節List);

            return 文節List;
        }


        public static void 不要文字列除外(ref List<string> 文節List)
        {
            // URL
            //文節List.RemoveAll(x => x.IndexOf("http") > -1 && 全角半角判定(x) == 全角半角.半角のみ);
            文節List.RemoveAll(x => x.IndexOf("https://") > -1 && x.IndexOf('.') > -1);  // URLが含まれている行は全て除外。
            文節List.RemoveAll(x => x.IndexOf("http://") > -1 && x.IndexOf('.') > -1);  // URLが含まれている行は全て除外。
            文節List.RemoveAll(x => x == " · ");  // Googleの検索結果に含まれるゴミ

            //// TAG
            //文節List.RemoveAll(x => x.IndexOf("href=\"") > -1 && 全角半角判定(x) == 全角半角.半角のみ);

            // メールアドレス
            //文節List.RemoveAll(x => x.IndexOf("@") > -1 && x.IndexOf(".com") > -1 && 全角半角判定(x) == 全角半角.半角のみ);
            文節List.RemoveAll(x => x.IndexOf('@') > -1 && x.IndexOf('.') > -1);            
        }

        private static StringBuilder 英語連結名詞Extraction_解析後文節 = new StringBuilder(数値._10MB);

        public static string 英語連結名詞Extraction(List<string> 文節List, 
            ref List<KeywordRow> keywordRowList)
        {
            英語連結名詞Extraction_解析後文節.Clear();

            foreach (var 文節 in 文節List)
            {
                try
                {
                    var keywordRowList2 = new List<KeywordRow>();

                    英語連結名詞Extraction_文節毎(文節, ref keywordRowList2);

                    keywordRowList.AddRange(keywordRowList2);

                    // 英語連結名詞を除外した文節はMeCab解析で使う。
                    var 文節2 = 文節;
                    foreach (var keywordRowList3 in keywordRowList2)
                    {
                        // Replace文字は \n
                        // Replace文字が半角スペースだと、形態素解析連結名詞で意図しない連結になる可能性がある。
                        文節2 = 文節2.Replace(keywordRowList3.Word, "\n").Trim();
                    }

                    if (!string.IsNullOrEmpty(文節2))
                    {
                        英語連結名詞Extraction_解析後文節.Append(文節2 + "\r\n");  // SQLServerの実行結果をメモ帳で確認する為に \n ではなく \r\n 。
                    }
                }
                catch (Exception ex)
                {
                    ログ.ログ書き出し(ex);
                    ログ.ログ書き出し("文節: " + 文節);
                    throw;
                }
            }

            return 英語連結名詞Extraction_解析後文節.ToString();
        }

        // 英語連結名詞を抽出する。
        public static void 英語連結名詞Extraction_文節毎(string 文節, 
            ref List<KeywordRow> KeywordRowList)
        {
            if (!英語連結名詞有無判定Regex.IsMatch(文節))
            {
                // 「英数+半角スペース+英数」パターンの文字列は見つからなかった。
                return;
            }

            // 最初/最後にスペースが有ると例外が発生する
            var str = 文節.Trim();

            //// スペースが連続していると例外が発生する
            //var cnt最大実行数 = 0;
            //while (str.IndexOf("  ") > -1)
            //{
            //    str = str.Replace("   ", " ");
            //    str = str.Replace("  ", " ");

            //    if (cnt最大実行数++ > 1000)
            //    {
            //        // 最大実行数を超えたら、無限ループ回避の為に終了する。
            //        ログ.ログ書き出し($"英語連結名詞Extraction_文節毎()失敗。 cnt最大実行数:{cnt最大実行数}   文節: {文節}\r\n");

            //        break;
            //    }
            //}

            var 先頭不要文字列除去中 = true;
            while (先頭不要文字列除去中)
            {
                if (str.IndexOf(' ') < 0)
                {
                    // 先頭の不要文字列を除去した結果、英語連結名詞が無い事が分かったので終了
                    return;
                }
                
                if (全角半角判定(str.Substring(str.IndexOf(' ') - 1, 1)) == 全角半角.全角含む)
                {
                    // スペースの1つ前の文字が全角なら、そのスペースまでは除外する
                    str = str.Substring(str.IndexOf(' ') + 1);
                }
                else if (全角半角判定(str.Substring(str.IndexOf(' ') + 1, 1)) == 全角半角.全角含む)
                {
                    // スペースの1つ後の文字が全角なら、そのスペースまでは除外する
                    str = str.Substring(str.IndexOf(' ') + 1);
                }
                else
                {
                    先頭不要文字列除去中 = false;
                }
            }

            str = スペースの前の半角文字の前にある全角文字を除去(str);

            var startIndex = 0;
            var start = false;
            var spaceCnt = 0;
            var 連結名詞文字数Cnt = 0;

            // 1文字づつ解析。
            var cnt = 0;
            while (cnt < str.Length)
            {
                if (スペース判定Regex.IsMatch(str[cnt].ToString()))
                {
                    // 半角 連結名詞 継続中。
                    連結名詞文字数Cnt++;

                    // スペース
                    spaceCnt++;
                }
                else if (全角半角判定(str[cnt].ToString()) == 全角半角.半角のみ)
                {
                    if (start == false)
                    {
                        // 半角 連結名詞 開始。

                        // 最初の英単語の開始位置を記録
                        startIndex = cnt;
                        start = true;
                        連結名詞文字数Cnt = 1;
                    }
                    else
                    {
                        // 半角 連結名詞 継続中。
                        連結名詞文字数Cnt++;
                    }
                }
                else if (全角半角判定(str[cnt].ToString()) == 全角半角.全角含む)
                {
                    // スペースが1つも無い英語は、日本語と連結した名詞になっている可能性が高いので無視。
                    if (start && spaceCnt > 0)
                    {
                        // 英語連結名詞 終了位置。
                        //var 英語連結名詞 = 特殊記号除去(str.Substring(startIndex, 連結名詞文字数Cnt), " ");
                        var 英語連結名詞 = str.Substring(startIndex, 連結名詞文字数Cnt);
                        if (!string.IsNullOrEmpty(英語連結名詞))
                        {
                            KeywordRowList.Add(new KeywordRow()
                            {
                                名詞区分 = 名詞区分.英語連結名詞,
                                Word = 英語連結名詞.Trim(),
                                解析元データ = 文節
                            });
                        }

                        // 解析残切り出し
                        str = str.Substring(startIndex + 連結名詞文字数Cnt);
                        cnt = 0;

                        if (!英語連結名詞有無判定Regex.IsMatch(str))
                        {
                            // 「英数+半角スペース+英数」パターンの文字列は見つからなかった。
                            return;
                        }

                        str = スペースの前の半角文字の前にある全角文字を除去(str);

                        // 初期化
                        start = false;
                        spaceCnt = 0;
                        連結名詞文字数Cnt = 0;

                        continue;
                    }
                }
                else
                {
                    // それ以外は連結中の文字とみなす。

                    // 半角 連結名詞 継続中。
                    連結名詞文字数Cnt++;
                }

                cnt++;
            }

            if (start)
            {
                // 文字列の最後が英語連結名詞だった。
                //var 英語連結名詞 = 特殊記号除去(str.Substring(startIndex), " ");
                var 英語連結名詞 = str.Substring(startIndex);
                if (!string.IsNullOrEmpty(英語連結名詞))
                {
                    KeywordRowList.Add(new KeywordRow()
                    {
                        名詞区分 = 名詞区分.英語連結名詞,
                        Word = 英語連結名詞.Trim(),
                        解析元データ = 文節
                    });
                }
            }
        }

        // スペースの前の半角文字の前に、全角文字がある場合、その全角より前の文字列は除外する
        public static string スペースの前の半角文字の前にある全角文字を除去(string str)
        {
            var スペース開始位置 = str.IndexOf(' ');
            for (var 文字cnt = 0; 文字cnt < スペース開始位置; 文字cnt++)
            {
                if (全角半角判定(str.Substring(スペース開始位置 - 文字cnt, 1)) == 全角半角.全角含む)
                {
                    return str.Substring(スペース開始位置 - 文字cnt + 1);
                }
            }

            return str;
        }


        //// 名詞に使わないことが明らかな記号を除去
        //public static string 特殊記号除去(string str, string 置換文字)
        //{
        //    //while (最初文字列記号のみ判定Regex.IsMatch(str))
        //    //{
        //    //    str = 最初文字列記号のみ判定Regex.Replace(str, "");
        //    //}

        //    //while (最後文字列記号のみ判定Regex.IsMatch(str))
        //    //{
        //    //    str = 最後文字列記号のみ判定Regex.Replace(str, "");
        //    //}


        //    //while (最初文字列記号のみ空白判定Regex.IsMatch(str))
        //    //{
        //    //    str = str.Substring(str.IndexOf(" ") + " ".Length);
        //    //}

        //    //while (最後文字列記号のみ空白判定Regex.IsMatch(str))
        //    //{
        //    //    str = str.Substring(0, str.LastIndexOf(" "));
        //    //}

        //    str = 特殊記号除去_IndexOf(str, "[", 置換文字);
        //    str = 特殊記号除去_IndexOf(str, "]", 置換文字);
        //    str = 特殊記号除去_IndexOf(str, "?", 置換文字);
        //    str = 特殊記号除去_IndexOf(str, "!", 置換文字);
        //    str = 特殊記号除去_IndexOf(str, "-", 置換文字);
        //    str = 特殊記号除去_IndexOf(str, "=", 置換文字);
        //    //str = 特殊記号除去_IndexOf(str, "/", 置換文字);
        //    str = 特殊記号除去_IndexOf(str, "*", 置換文字);
        //    str = 特殊記号除去_IndexOf(str, "(c)", 置換文字);
        //    str = 特殊記号除去_IndexOf(str, "(", 置換文字);
        //    str = 特殊記号除去_IndexOf(str, ")", 置換文字);
        //    str = 特殊記号除去_IndexOf(str, "<", 置換文字);
        //    str = 特殊記号除去_IndexOf(str, ">", 置換文字);
        //    str = 特殊記号除去_IndexOf(str, ":", 置換文字);

        //    return str.Trim();
        //}

        //// 全角記号は、英語連結名詞の抽出、形態素解析結果からの抽出、どちらからも除外されるのと、
        //public static string 全角特殊記号除去(string str, string 置換文字)
        //{
        //    str = 特殊記号除去_IndexOf(str, "━", 置換文字);
        //    str = 特殊記号除去_IndexOf(str, "　", 置換文字);
        //    str = 特殊記号除去_IndexOf(str, "【", 置換文字);
        //    str = 特殊記号除去_IndexOf(str, "】", 置換文字);
        //    str = 特殊記号除去_IndexOf(str, "≪", 置換文字);
        //    str = 特殊記号除去_IndexOf(str, "≫", 置換文字);
        //    str = 特殊記号除去_IndexOf(str, "「", 置換文字);
        //    str = 特殊記号除去_IndexOf(str, "」", 置換文字);
        //    str = 特殊記号除去_IndexOf(str, "！", 置換文字);


        //    return str.Trim();
        //}

        public static string 特殊記号除去_IndexOf(string str, string 置換対象, string 置換文字)
        {
            var cnt最大実行数 = 0;
            while (str.IndexOf(置換対象) > -1)
            {
                str = str.Replace(置換対象, 置換文字);

                if (cnt最大実行数++ > 100)
                {
                    // 最大実行数を超えたら、無限ループ回避の為に終了する。
                    ログ.ログ書き出し($"置換対象除去失敗。 cnt最大実行数:{cnt最大実行数}   str: {str}   置換対象: {置換対象}   置換文字: {置換文字}\r\n");

                    break;
                }
            }

            return str;
        }


        public static 言語判定結果 言語判定(string str)
        {
            var count = 0;

            foreach (var a in 日本語)
            {
                if (str.IndexOf(a) > -1)
                {
                    count++;

                    if (count > 7)
                    {
                        // 「ひらがな 、 。」を8文字以上含んでいれば日本語とみなす。
                        return 言語判定結果.日本語;
                    }
                }
            }

            return 言語判定結果.英語;
        }

        private static StringBuilder str = new StringBuilder(数値._10MB);
        public static string 名詞連結(List<KeywordRow> keywordRowList)
        {
            str.Clear();

            foreach (var row in keywordRowList)
            {
                str.Append(row.Word + "\r\n");
            }

            return str.ToString();
        }

        // char c, string str どちらも同じ値。変換数を少なくする為に両方を渡してる。
        public static bool Is半角アルファベット(char c, string str)
        {
            if (str.Trim().Length < 1)
            {
                // 半角アルファベットじゃない
                return false;
            }

            if (char.IsLetter(c) == false)
            {
                // 半角アルファベットじゃない
                return false;
            }

            var byte_data = Encoding.UTF8.GetBytes(str);
            if (byte_data.Length == str.Length)
            {
                // 半角アルファベット
                return true;
            }
            else
            {
                // 半角アルファベットじゃない
                return false;
            }
        }

        // 全て半角だと英文だと見なす
        public static 全角半角 全角半角判定(string str)
        {
            var byte_data = Encoding.UTF8.GetBytes(str);
            if (byte_data.Length == str.Length)
            {
                return 全角半角.半角のみ;
            }
            else
            {
                return 全角半角.全角含む;
            }
        }

        //public static void 記号区切り数値抽出(ref string 解析元, ref List<string> list)
        //{
        //    var cnt = 0;
        //    var 開始Index = 0;
        //    var 記号Index = 0;
        //    var 終了Index = 0;

        //    foreach (var 記号 in 文節区切り文字List_第１段階)
        //    {
        //        cnt = 0;
        //        記号Index = 0;
        //        終了Index = 0;
        //        while ((記号Index = 解析元.IndexOf(記号)) > -1)
        //        {
        //            if (cnt++ < 100)
        //            {
        //                // 100回以上出るのは処理不可と見なし中断 
        //                break;
        //            }

        //            // 数値 前方Index
        //            開始Index = 1;
        //            while (char.IsNumber(解析元.Substring(記号Index - 開始Index, 1)[0]))
        //            {
        //                開始Index++;
        //            }
        //            開始Index--;

        //            // 数値 後方Index
        //            終了Index = 1;
        //            while (char.IsNumber(解析元.Substring(記号Index + 終了Index, 1)[0]))
        //            {
        //                終了Index++;
        //            }
        //            終了Index--;

        //            list.Add(解析元.Substring(記号Index - 開始Index, 終了Index - 開始Index));
        //            解析元 = 解析元.Remove(記号Index - 開始Index, 終了Index - 開始Index);
        //        }
        //    }
        //}

    }
}
