using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using 定数;
using Common;


namespace UnitTest_x64
{
    [TestClass]
    public class StringProcessingTest
    {

        [TestMethod]
        public void StringProcessingTest_分割_1()
        {
            var 前 = "C#";
            var list = StringProcessing.記号毎に文を分割(前, ' ');
            Assert.AreEqual(list.Count, 1);
            Assert.AreEqual(list[0], "C#");

            前 = "View all";
            list = StringProcessing.記号毎に文を分割(前, ' ');
            Assert.AreEqual(list.Count, 2);
            Assert.AreEqual(list[0], "View");
            Assert.AreEqual(list[1], "all");

            前 = " View all Oracle ああ いい ";
            list = StringProcessing.記号毎に文を分割(前, ' ');
            Assert.AreEqual(list.Count, 5);
            Assert.AreEqual(list[0], "View");
            Assert.AreEqual(list[1], "all");
            Assert.AreEqual(list[2], "Oracle");
            Assert.AreEqual(list[3], "ああ");
            Assert.AreEqual(list[4], "いい");

            前 = "View all Oracle ああ いい";
            list = StringProcessing.記号毎に文を分割(前, ' ');
            Assert.AreEqual(list.Count, 5);
            Assert.AreEqual(list[0], "View");
            Assert.AreEqual(list[1], "all");
            Assert.AreEqual(list[2], "Oracle");
            Assert.AreEqual(list[3], "ああ");
            Assert.AreEqual(list[4], "いい");

            前 = " ";
            list = StringProcessing.記号毎に文を分割(前, ' ');
            Assert.AreEqual(list.Count, 0);
        }


        [TestMethod]
        public void StringProcessingTest_IsLetter_1()
        {
            bool a = false;
            a = Char.IsLetter('a');
            a = Char.IsLetter('あ');
            a = Char.IsLetter('　');
            a = Char.IsLetter('1');
            a = Char.IsLetter('3');
        }


        [TestMethod]
        public void StringProcessingTest_クレンジング_1()
        {
            var 前 = "View all Oracle’s hardware and software products";
            var 後 = StringProcessing.クレンジング(前);
            Assert.AreEqual(後, "View all Oracle's hardware and software products");

            前 = "View all Oracle’s hard’ware a'nd' software’ products";
            後 = StringProcessing.クレンジング(前);
            Assert.AreEqual(後, "View all Oracle's hard'ware a'nd' software’ products");
        }


        [TestMethod]
        public void StringProcessingTest_文節抽出_まとめ_1()
        {
            var 解析元list = new List<string>();
            var list = new List<string>();

            解析元list.Add("View all Oracle’s hardware and software products");
            list = StringProcessing.文節抽出_まとめ(解析元list);
            Assert.AreEqual(list.Count, 1);
            Assert.AreEqual(list[0], "View all Oracle’s hardware and software products");
        }

        [TestMethod]
        public void StringProcessingTest_文節抽出_MeCab用3_1()
        {
            string 解析元;
            var list = new List<string>();

            解析元 = "\n";
            list = StringProcessing.文節抽出_MeCab用3(解析元);
            Assert.AreEqual(list.Count, 0);

            解析元 = "a\n";
            list = StringProcessing.文節抽出_MeCab用3(解析元);
            Assert.AreEqual(list.Count, 1);
            Assert.AreEqual(list[0], "a");

            解析元 = "\na";
            list = StringProcessing.文節抽出_MeCab用3(解析元);
            Assert.AreEqual(list.Count, 1);
            Assert.AreEqual(list[0], "a");

            解析元 = "a\nb";
            list = StringProcessing.文節抽出_MeCab用3(解析元);
            Assert.AreEqual(list.Count, 2);
            Assert.AreEqual(list[0], "a");
            Assert.AreEqual(list[1], "b");

            解析元 = "a";
            list = StringProcessing.文節抽出_MeCab用3(解析元);
            Assert.AreEqual(list.Count, 1);
            Assert.AreEqual(list[0], "a");

            解析元 = "あ\nい\n";
            list = StringProcessing.文節抽出_MeCab用3(解析元);
            Assert.AreEqual(list.Count, 2);
            Assert.AreEqual(list[0], "あ");
            Assert.AreEqual(list[1], "い");

            解析元 = "あa\nいbb\n";
            list = StringProcessing.文節抽出_MeCab用3(解析元);
            Assert.AreEqual(list.Count, 2);
            Assert.AreEqual(list[0], "あa");
            Assert.AreEqual(list[1], "いbb");
        }

        [TestMethod]
        public void StringProcessingTest_連続スペース区切りでList化_1()
        {
            string 解析元;
            var list = new List<string>();

            解析元 = "ZDNet Japan Information Mail                                  2018年12月21日";
            list = StringProcessing.文節分割_連続スペース(解析元);
            Assert.AreEqual(list.Count, 2);
            Assert.AreEqual(list[0], "ZDNet Japan Information Mail");
            Assert.AreEqual(list[1], "2018年12月21日");

            解析元 = "ZDNet Japan Information Mail　　2018年12月21日";
            list = StringProcessing.文節分割_連続スペース(解析元);
            Assert.AreEqual(list.Count, 2);
            Assert.AreEqual(list[0], "ZDNet Japan Information Mail");
            Assert.AreEqual(list[1], "2018年12月21日");

            解析元 = "View all Oracle’s hardware and software products";
            list = StringProcessing.文節分割_連続スペース(解析元);
            Assert.AreEqual(list.Count, 1);
            Assert.AreEqual(list[0], "View all Oracle’s hardware and software products");
        }

        [TestMethod]
        public void StringProcessingTest_文節抽出_英語省略形外_1()
        {
            string 解析元;
            var list = new List<string>();

            解析元 = "View all Oracle’s hardware and software products";
            list = StringProcessing.文節抽出_英語省略形外(解析元);
            Assert.AreEqual(list.Count, 1);
            Assert.AreEqual(list[0], "View all Oracle’s hardware and software products");


            解析元 = "a'b a-b";
            list = StringProcessing.文節抽出_英語省略形外(解析元);
            Assert.AreEqual(list.Count, 1);
            Assert.AreEqual(list[0], "a'b a-b");

            解析元 = "'b a-";
            list = StringProcessing.文節抽出_英語省略形外(解析元);
            Assert.AreEqual(list.Count, 1);
            Assert.AreEqual(list[0], "b a");

            解析元 = "a -b a' b";
            list = StringProcessing.文節抽出_英語省略形外(解析元);
            Assert.AreEqual(list.Count, 3);
            Assert.AreEqual(list[0], "a");
            Assert.AreEqual(list[1], "b a");
            Assert.AreEqual(list[2], "b");

            解析元 = "a' -b";
            list = StringProcessing.文節抽出_英語省略形外(解析元);
            Assert.AreEqual(list.Count, 2);
            Assert.AreEqual(list[0], "a");
            Assert.AreEqual(list[1], "b");

            解析元 = "' -";
            list = StringProcessing.文節抽出_英語省略形外(解析元);
            Assert.AreEqual(list.Count, 0);

            解析元 = " 'b a- ";
            list = StringProcessing.文節抽出_英語省略形外(解析元);
            Assert.AreEqual(list.Count, 1);
            Assert.AreEqual(list[0], "b a");

            解析元 = "a'  -b";
            list = StringProcessing.文節抽出_英語省略形外(解析元);
            Assert.AreEqual(list.Count, 2);
            Assert.AreEqual(list[0], "a");
            Assert.AreEqual(list[1], "b");

            解析元 = "1'1 1-1";
            list = StringProcessing.文節抽出_英語省略形外(解析元);
            Assert.AreEqual(list.Count, 3);
            Assert.AreEqual(list[0], "1");
            Assert.AreEqual(list[1], "1 1");
            Assert.AreEqual(list[2], "1");
        }

        [TestMethod]
        public void StringProcessingTest_文節抽出_記号区切り数値外_1()
        {
            string 解析元;
            var list = new List<string>();

            解析元 = "aa/3 aaaggg,sa000.2うい.a";
            list = StringProcessing.文節抽出_記号区切り数値外(解析元);
            Assert.AreEqual(list.Count, 4);
            Assert.AreEqual(list[0], "aa");
            Assert.AreEqual(list[1], "3 aaaggg");
            Assert.AreEqual(list[2], "sa000.2うい");
            Assert.AreEqual(list[3], "a");

            解析元 = "Qiitあ,いお.うえ/え3。";
            list = StringProcessing.文節抽出_記号区切り数値外(解析元);
            Assert.AreEqual(list.Count, 4);
            Assert.AreEqual(list[0], "Qiitあ");
            Assert.AreEqual(list[1], "いお");
            Assert.AreEqual(list[2], "うえ");
            Assert.AreEqual(list[3], "え3。");

            解析元 = ",sa";
            list = StringProcessing.文節抽出_記号区切り数値外(解析元);
            Assert.AreEqual(list.Count, 1);
            Assert.AreEqual(list[0], "sa");

            解析元 = "sa,";
            list = StringProcessing.文節抽出_記号区切り数値外(解析元);
            Assert.AreEqual(list.Count, 1);
            Assert.AreEqual(list[0], "sa");

            解析元 = "Qiita,sa";
            list = StringProcessing.文節抽出_記号区切り数値外(解析元);
            Assert.AreEqual(list.Count, 2);
            Assert.AreEqual(list[0], "Qiita");
            Assert.AreEqual(list[1], "sa");

            解析元 = "11/12 aaa,sa";
            list = StringProcessing.文節抽出_記号区切り数値外(解析元);
            Assert.AreEqual(list.Count, 2);
            Assert.AreEqual(list[0], "11/12 aaa");
            Assert.AreEqual(list[1], "sa");

            解析元 = ".3a/w3、";
            list = StringProcessing.文節抽出_記号区切り数値外(解析元);
            Assert.AreEqual(list.Count, 2);
            Assert.AreEqual(list[0], "3a");
            Assert.AreEqual(list[1], "w3、");

            解析元 = " Net 0.9 ";
            list = StringProcessing.文節抽出_記号区切り数値外(解析元);
            Assert.AreEqual(list.Count, 1);
            Assert.AreEqual(list[0], "Net 0.9");

            解析元 = "1,000円";
            list = StringProcessing.文節抽出_記号区切り数値外(解析元);
            Assert.AreEqual(list.Count, 1);
            Assert.AreEqual(list[0], "1,000円");

            解析元 = " 11/12。";
            list = StringProcessing.文節抽出_記号区切り数値外(解析元);
            Assert.AreEqual(list.Count, 1);
            Assert.AreEqual(list[0], "11/12。");

            解析元 = "Qiita,";
            list = StringProcessing.文節抽出_記号区切り数値外(解析元);
            Assert.AreEqual(list.Count, 1);
            Assert.AreEqual(list[0], "Qiita");

            解析元 = " Net 0.9 1,000円 11/12。Qiita,sa.3a/w3、Qiitあ,いお.うえ/え3。";
            list = StringProcessing.文節抽出_記号区切り数値外(解析元);
            Assert.AreEqual(list.Count, 7);
            Assert.AreEqual(list[0], "Net 0.9 1,000円 11/12。Qiita");
            Assert.AreEqual(list[1], "sa");
            Assert.AreEqual(list[2], "3a");
            Assert.AreEqual(list[3], "w3、Qiitあ");
            Assert.AreEqual(list[4], "いお");
            Assert.AreEqual(list[5], "うえ");
            Assert.AreEqual(list[6], "え3。");
        }

        //[TestMethod]
        //public void StringProcessingTest_記号区切り数値抽出_1()
        //{
        //    string 解析元;
        //    List<string> list = new List<string>();

        //    解析元 = " Qiita あ &gt; &#183; 11.9 0.1の1\\1,000円 ";
        //    StringProcessing.記号区切り数値抽出(ref 解析元, ref list);
        //    Assert.AreEqual(list[0], "11.9");
        //    Assert.AreEqual(list[0], "0.1");
        //    Assert.AreEqual(list[0], "1");
        //    Assert.AreEqual(list[0], "1");
        //}

        [TestMethod]
        public void StringProcessingTest_タグ除去_1()
        {
            string 解析元;

            解析元 = " Qiita あ &gt; &#183;";
            var str = HtmlParse.Htmlタグ除去(解析元);
            Assert.AreEqual(str, " Qiita あ > ·");
        }

        [TestMethod]
        public void StringProcessingTest_全角半角判定_1()
        {
            string 解析元;
            全角半角 言語判定結果;

            解析元 = "\nNaptime\n - \nSuper Doze now for unrooted users too\n - \nGoogle Play\n の \nhttps://\nplay\n.\ngoogle\n.com/store/apps/details?id=com \ndoze\n&amp;hl \n‎\nキャッシュ\n類似ページ\n2018年10月2日\n Empower Android&#39;s \nDoze\n Mode to the limit! Why? Because why the hell not? \nDoze\n is amazing - arguably the best feature of Android™ Marshmallow, Nougat \nand Oreo. But with its caveats. It can take at least 2 hours to kick in ; \n";
            言語判定結果 = StringProcessing.全角半角判定(解析元);
            Assert.AreEqual(言語判定結果, 全角半角.全角含む);

            解析元 = "\nNaptime\n - \nSuper Doze now for unrooted users too\n - \nGoogle Play\n";
            言語判定結果 = StringProcessing.全角半角判定(解析元);
            Assert.AreEqual(言語判定結果, 全角半角.半角のみ);

            解析元 = "  \nhttps://\nplay\n.\ngoogle\n.com/store/apps/details?id=com";
            言語判定結果 = StringProcessing.全角半角判定(解析元);
            Assert.AreEqual(言語判定結果, 全角半角.半角のみ);

            解析元 = " \ndoze\n&amp;hl";
            言語判定結果 = StringProcessing.全角半角判定(解析元);
            Assert.AreEqual(言語判定結果, 全角半角.半角のみ);

            解析元 = " \n‎\n2018102\n";
            言語判定結果 = StringProcessing.全角半角判定(解析元);
            Assert.AreEqual(言語判定結果, 全角半角.全角含む);

            解析元 = " Empower Android&#39;s \nDoze\n";
            言語判定結果 = StringProcessing.全角半角判定(解析元);
            Assert.AreEqual(言語判定結果, 全角半角.半角のみ);

            解析元 = " Mode to the limit! Why? Because why the hell not? \nDoze\n";
            言語判定結果 = StringProcessing.全角半角判定(解析元);
            Assert.AreEqual(言語判定結果, 全角半角.半角のみ);

            解析元 = " is amazing - arguably the best feature of Android™ Marshmallow, ";
            言語判定結果 = StringProcessing.全角半角判定(解析元);
            Assert.AreEqual(言語判定結果, 全角半角.全角含む);

            解析元 = "Nougat \nand Oreo. But with its caveats. It can take at least 2 hours to kick in ; \n";
            言語判定結果 = StringProcessing.全角半角判定(解析元);
            Assert.AreEqual(言語判定結果, 全角半角.半角のみ);

        }

        [TestMethod]
        public void StringProcessingTest_言語判定_1()
        {
            string 解析元;
            言語判定結果 言語判定結果;

            // Google

            解析元 = "\nNaptime\n - \nSuper Doze now for unrooted users too\n - \nGoogle Play\n の \nhttps://\nplay\n.\ngoogle\n.com/store/apps/details?id=com \ndoze\n&amp;hl \n‎\nキャッシュ\n類似ページ\n2018年10月2日\n Empower Android&#39;s \nDoze\n Mode to the limit! Why? Because why the hell not? \nDoze\n is amazing - arguably the best feature of Android™ Marshmallow, Nougat \nand Oreo. But with its caveats. It can take at least 2 hours to kick in ; \n";
            言語判定結果 = StringProcessing.言語判定(解析元);
            Assert.AreEqual(言語判定結果, 言語判定結果.英語);
            Assert.AreEqual(解析元.IndexOf("このページを訳す") < 0, true);


            解析元 = "djwpojesadfこのページを訳すopaijapw";
            言語判定結果 = StringProcessing.言語判定(解析元);
            Assert.AreEqual(言語判定結果, 言語判定結果.英語);
            Assert.AreEqual(解析元.IndexOf("このページを訳す") < 0, true);

            解析元 = "djwpojesadfこのページを翻訳opaijapw";
            言語判定結果 = StringProcessing.言語判定(解析元);
            Assert.AreEqual(言語判定結果, 言語判定結果.英語);
            Assert.AreEqual(解析元.IndexOf("このページを翻訳") < 0, true);

            解析元 = "djwpojesadfopaijapw";
            言語判定結果 = StringProcessing.言語判定(解析元);
            Assert.AreEqual(言語判定結果, 言語判定結果.英語);

            解析元 = "djwpojesadfopai、。あいうえおかjapw";
            言語判定結果 = StringProcessing.言語判定(解析元);
            Assert.AreEqual(言語判定結果, 言語判定結果.日本語);

            解析元 = "djwpojesadfopai、。あいうえおのjapw";
            言語判定結果 = StringProcessing.言語判定(解析元);
            Assert.AreEqual(言語判定結果, 言語判定結果.日本語);

            // Bing

            解析元 = "djwpojesadfこのページを訳すopaijapw";
            言語判定結果 = StringProcessing.言語判定(解析元);
            Assert.AreEqual(言語判定結果, 言語判定結果.英語);
            Assert.AreEqual(解析元.IndexOf("このページを訳す") < 0, true);

            解析元 = "djwpojesadfこのページを翻訳opaijapw";
            言語判定結果 = StringProcessing.言語判定(解析元);
            Assert.AreEqual(言語判定結果, 言語判定結果.英語);
            Assert.AreEqual(解析元.IndexOf("このページを翻訳") < 0, true);

            解析元 = "djwpojesadfopaijapw";
            言語判定結果 = StringProcessing.言語判定(解析元);
            Assert.AreEqual(言語判定結果, 言語判定結果.英語);

            解析元 = "djwpojesadfopai、。あいうえおかjapw";
            言語判定結果 = StringProcessing.言語判定(解析元);
            Assert.AreEqual(言語判定結果, 言語判定結果.日本語);

            解析元 = "djwpojesadfopai、。あいうえおかのjapw";
            言語判定結果 = StringProcessing.言語判定(解析元);
            Assert.AreEqual(言語判定結果, 言語判定結果.日本語);

            // Yahoo

            解析元 = "djwpojesadfこのページを訳すわopaえおかえおかijapw";
            言語判定結果 = StringProcessing.言語判定(解析元);
            Assert.AreEqual(言語判定結果, 言語判定結果.日本語);
            Assert.AreEqual(解析元.IndexOf("このページを訳す") > -1, true);

            解析元 = "djwpojesadfこのページを翻訳するopaijえおかえおかapw";
            言語判定結果 = StringProcessing.言語判定(解析元);
            Assert.AreEqual(言語判定結果, 言語判定結果.日本語);
            Assert.AreEqual(解析元.IndexOf("このページを翻訳") > -1, true);

            解析元 = "djwpojesadfopaijapw";
            言語判定結果 = StringProcessing.言語判定(解析元);
            Assert.AreEqual(言語判定結果, 言語判定結果.英語);

            解析元 = "djwpojesadfopai、、。あいうえおかjapw";
            言語判定結果 = StringProcessing.言語判定(解析元);
            Assert.AreEqual(言語判定結果, 言語判定結果.日本語);

            解析元 = "djwpojesadfopai、。あいうえおかのjapw";
            言語判定結果 = StringProcessing.言語判定(解析元);
            Assert.AreEqual(言語判定結果, 言語判定結果.日本語);

            // メールマガジン

            解析元 = "djwpojesadfこのページを訳すわopaiえおかえおかjapw";
            言語判定結果 = StringProcessing.言語判定(解析元);
            Assert.AreEqual(言語判定結果, 言語判定結果.日本語);
            Assert.AreEqual(解析元.IndexOf("このページを訳す") > -1, true);

            解析元 = "djwpojesadfこのページを翻訳するopaijえおかえおかapw";
            言語判定結果 = StringProcessing.言語判定(解析元);
            Assert.AreEqual(言語判定結果, 言語判定結果.日本語);
            Assert.AreEqual(解析元.IndexOf("このページを翻訳") > -1, true);

            解析元 = "djwpojesadfopaijapw";
            言語判定結果 = StringProcessing.言語判定(解析元);
            Assert.AreEqual(言語判定結果, 言語判定結果.英語);

            解析元 = "djwpojesadfopai、、。あいうえおかjapw";
            言語判定結果 = StringProcessing.言語判定(解析元);
            Assert.AreEqual(言語判定結果, 言語判定結果.日本語);

            解析元 = "djwpojesadfopai、。あいうえおかのjapw";
            言語判定結果 = StringProcessing.言語判定(解析元);
            Assert.AreEqual(言語判定結果, 言語判定結果.日本語);
        }

        [TestMethod]
        public void StringProcessingTest_文節分割_1()
        {
            string 行;
            List<string> 文節List;

            行 = "Windows 1   Windows 2|Windows 3   Windows 4";
            文節List = StringProcessing.文節分割(行);
            Assert.AreEqual(文節List.Count, 4);
            Assert.AreEqual(文節List[0], "Windows 1");
            Assert.AreEqual(文節List[1], "Windows 2");
            Assert.AreEqual(文節List[2], "Windows 3");
            Assert.AreEqual(文節List[3], "Windows 4");

            行 = "Windows 1   Windows 2";
            文節List = StringProcessing.文節分割(行);
            Assert.AreEqual(文節List.Count, 2);
            Assert.AreEqual(文節List[0], "Windows 1");
            Assert.AreEqual(文節List[1], "Windows 2");

            行 = "Windows 1+Windows 2";
            文節List = StringProcessing.文節分割(行);
            Assert.AreEqual(文節List.Count, 2);
            Assert.AreEqual(文節List[0], "Windows 1");
            Assert.AreEqual(文節List[1], "Windows 2");

            行 = "Windows 1*Windows 2";
            文節List = StringProcessing.文節分割(行);
            Assert.AreEqual(文節List.Count, 2);
            Assert.AreEqual(文節List[0], "Windows 1");
            Assert.AreEqual(文節List[1], "Windows 2");

            行 = "Windows 1、Windows 2";
            文節List = StringProcessing.文節分割(行);
            Assert.AreEqual(文節List.Count, 2);
            Assert.AreEqual(文節List[0], "Windows 1");
            Assert.AreEqual(文節List[1], "Windows 2");

            行 = "Windows 1。Windows 2";
            文節List = StringProcessing.文節分割(行);
            Assert.AreEqual(文節List.Count, 2);
            Assert.AreEqual(文節List[0], "Windows 1");
            Assert.AreEqual(文節List[1], "Windows 2");

            
        }

        [TestMethod]
        public void StringProcessingTest_1()
        {
            string testData;
            List<KeywordRow> result;

            testData = "View all Oracle’s hardware and software products";
            result = new List<KeywordRow>();
            StringProcessing.英語連結名詞Extraction_文節毎(testData, ref result);
            Assert.AreEqual(result.Count, 1);
            Assert.AreEqual(result[0].Word, "View all Oracle’s hardware and software products");

            //testData = "■TechTargetジャパン/キーマンズネット Information [PR]---------------------";
            //result = new List<string>();
            //StringProcessing.英語連結名詞Extraction行(testData, ref result);
            //Assert.AreEqual(result.Count, 0);

            // 日本語のみデータテスト
            testData = "あうぃあけ";
            result = new List<KeywordRow>();
            StringProcessing.英語連結名詞Extraction_文節毎(testData, ref result);
            Assert.AreEqual(result.Count, 0);


            // 英語連結名詞のみデータテスト

            testData = "Google API Client Library for .NET";
            result = new List<KeywordRow>();
            StringProcessing.英語連結名詞Extraction_文節毎(testData, ref result);
            Assert.AreEqual(result.Count, 1);
            Assert.AreEqual(result[0].Word, "Google API Client Library for .NET");


            // 英語連結名詞 複数データテスト

            testData = "あGa Ae C+い、.Ne Feで使う。";
            result = new List<KeywordRow>();
            StringProcessing.英語連結名詞Extraction_文節毎(testData, ref result);
            Assert.AreEqual(result.Count, 2);
            Assert.AreEqual(result[0].Word, "Ga Ae C+");
            Assert.AreEqual(result[1].Word, ".Ne Fe");


            // 英語連結名詞 複数データテスト

            testData = "あGoogle API Client Library for .NETい、.Net Frameworkで使う。";
            result = new List<KeywordRow>();
            StringProcessing.英語連結名詞Extraction_文節毎(testData, ref result);
            Assert.AreEqual(result.Count, 2);
            Assert.AreEqual(result[0].Word, "Google API Client Library for .NET");
            Assert.AreEqual(result[1].Word, ".Net Framework");


            // 英語日本語連結名詞 テスト

            testData = "今は、DX時代。";
            result = new List<KeywordRow>();
            StringProcessing.英語連結名詞Extraction_文節毎(testData, ref result);
            Assert.AreEqual(result.Count, 0);


            testData = "Windows 10";
            result = new List<KeywordRow>();
            StringProcessing.英語連結名詞Extraction_文節毎(testData, ref result);
            Assert.AreEqual(result.Count, 1);
            Assert.AreEqual(result[0].Word, "Windows 10");

            testData = "Windows";
            result = new List<KeywordRow>();
            StringProcessing.英語連結名詞Extraction_文節毎(testData, ref result);
            Assert.AreEqual(result.Count, 0);

            testData = "あWindows 10";
            result = new List<KeywordRow>();
            StringProcessing.英語連結名詞Extraction_文節毎(testData, ref result);
            Assert.AreEqual(result.Count, 1);
            Assert.AreEqual(result[0].Word, "Windows 10");

            testData = "Windows 10あ";
            result = new List<KeywordRow>();
            StringProcessing.英語連結名詞Extraction_文節毎(testData, ref result);
            Assert.AreEqual(result.Count, 1);
            Assert.AreEqual(result[0].Word, "Windows 10");


            testData = "いい Windows 10";
            result = new List<KeywordRow>();
            StringProcessing.英語連結名詞Extraction_文節毎(testData, ref result);
            Assert.AreEqual(result.Count, 1);
            Assert.AreEqual(result[0].Word, "Windows 10");

            testData = "Windows 10 いい";
            result = new List<KeywordRow>();
            StringProcessing.英語連結名詞Extraction_文節毎(testData, ref result);
            Assert.AreEqual(result.Count, 1);
            Assert.AreEqual(result[0].Word, "Windows 10");

            testData = "読まれた「『Windows 10』を無料で高速化する4つの方法」です。Windows 10を高速";
            result = new List<KeywordRow>();
            StringProcessing.英語連結名詞Extraction_文節毎(testData, ref result);
            Assert.AreEqual(result.Count, 2);
            Assert.AreEqual(result[0].Word, "Windows 10");
            Assert.AreEqual(result[1].Word, "Windows 10");

            testData = "  無断転載／複製を禁じます。      Copyright(C)  ASAHI INTERACTIVE 2018";
            result = new List<KeywordRow>();
            StringProcessing.英語連結名詞Extraction_文節毎(testData, ref result);
            Assert.AreEqual(result.Count, 1);
            Assert.AreEqual(result[0].Word, "Copyright(C) ASAHI INTERACTIVE 2018");

            testData = "・講演資料：IoT×AI×リーンによる効果を出すConnected Industriesの取り組み";
            result = new List<KeywordRow>();
            StringProcessing.英語連結名詞Extraction_文節毎(testData, ref result);
            Assert.AreEqual(result.Count, 1);
            Assert.AreEqual(result[0].Word, "Connected Industries");


            testData = " ZDNet Japan & AWS Partner Network 全6回セミナーの講演資料をご紹介します。";
            result = new List<KeywordRow>();
            StringProcessing.英語連結名詞Extraction_文節毎(testData, ref result);
            Assert.AreEqual(result.Count, 1);
            Assert.AreEqual(result[0].Word, "ZDNet Japan & AWS Partner Network");

            testData = "ZDNet Japan Information Mail                                  年";
            result = new List<KeywordRow>();
            StringProcessing.英語連結名詞Extraction_文節毎(testData, ref result);
            Assert.AreEqual(result.Count, 1);
            Assert.AreEqual(result[0].Word, "ZDNet Japan Information Mail");

            testData = "                                  AWS環境におけるセキュリティ運用の勘どころ";
            result = new List<KeywordRow>();
            StringProcessing.英語連結名詞Extraction_文節毎(testData, ref result);
            Assert.AreEqual(result.Count, 0);

            testData = " TechTargetジャパン  2018/12/21";
            result = new List<KeywordRow>();
            StringProcessing.英語連結名詞Extraction_文節毎(testData, ref result);
            Assert.AreEqual(result.Count, 0);

            testData = "フィッシングメールの「うっかりクリック」を防ぐ自動ブロックの仕組み【TechTarget 経営とIT 18/12/21】";
            result = new List<KeywordRow>();
            StringProcessing.英語連結名詞Extraction_文節毎(testData, ref result);
            Assert.AreEqual(result.Count, 1);
            Assert.AreEqual(result[0].Word, "IT 18/12/21");


            testData = "■発行:株式会社翔泳社 SHOEISHA iD事務局";
            result = new List<KeywordRow>();
            StringProcessing.英語連結名詞Extraction_文節毎(testData, ref result);
            Assert.AreEqual(result.Count, 1);
            Assert.AreEqual(result[0].Word, "SHOEISHA iD");

            testData = "小川卓氏から学ぶ分析と改善ノウハウ／参加特典にオリジナルテキストあり[SE Event News]";
            result = new List<KeywordRow>();
            StringProcessing.英語連結名詞Extraction_文節毎(testData, ref result);
            Assert.AreEqual(result.Count, 1);
            Assert.AreEqual(result[0].Word, "[SE Event News]");

            // 
            testData = "■クラウドデータ統合入門　博士が教える6つのチェックポイント";
            result = new List<KeywordRow>();
            StringProcessing.英語連結名詞Extraction_文節毎(testData, ref result);
            Assert.AreEqual(result.Count, 0);

            // 空データテスト
            testData = "";
            result = new List<KeywordRow>();
            StringProcessing.英語連結名詞Extraction_文節毎(testData, ref result);
            Assert.AreEqual(result.Count, 0);


        }
    }
}
