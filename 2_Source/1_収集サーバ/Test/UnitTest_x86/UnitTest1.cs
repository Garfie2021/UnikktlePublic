using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using 定数;
//using Common;
using MeCabExec;


namespace UnitTest_x86
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void MeCab()
        {
            // MeCab初期化
            var ptrMeCab = MeCabConst.mecab_new2("");

            //var 解析元 = "━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━";
            //var 解析元 = "締切迫る！お急ぎください";
            var 解析元 = "≪日本マイクロソフト≫";

            // 形態素解析
            var 形態素解析結果 = Marshal.PtrToStringAnsi(MeCabConst.mecab_sparse_tostr(ptrMeCab, 解析元));

            // MeCab開放
            MeCabConst.mecab_destroy(ptrMeCab);
        }


        [TestMethod]
        public void MorphologicalAnalysis_Write()
        {
            var morph = new MorphologicalAnalysisInfo()
            {
                No = 0,
                メール送信日時 = DateTime.Parse("2018/04/28 13:10:44"),
                From_MailAddress = "itmid-membership@noreply.itmedia.jp",
                From_DisplayName = "アイティメディアID事務局",
                CurrentMessageID = "20180428131044.60774E058@itmidsh02.itmedia.co.jp",
                MorphologicalAnalysis = "テスト\t名詞,サ変接続,*,*,*,*,テスト,テスト,テスト\nデータ\t名詞,一般,*,*,*,*,データ,データ,データ\nEOS\n",
            };

            MeCabResult.Write(morph);
        }


        //[TestMethod]
        //public void 重要語抽出_実データサンプル()
        //{
        //    List<KeywordRow> keywordRow;
        //    string morph;

        //    keywordRow = new List<KeywordRow>();
        //    morph = "あ\tフィラー,*,*,*,*,*,あ,ア,ア\nHUION\t名詞,一般,*,*,*,*,*\n木\t名詞,一般,*,*,*,*,木,キ,キ\nKamvas\t名詞,一般,*,*,*,*,*\n時\t名詞,接尾,副詞可能,*,*,*,時,ジ,ジ\nPro\t名詞,固有名詞,組織,*,*,*,*\n\t記号,空白,*,*,*,*,　,　,　\nC\t名詞,固有名詞,組織,*,*,*,*\n#\t名詞,サ変接続,*,*,*,*,*\n";
        //    AnalysisMeCab.Exec(CollectTargetCategory.メールマガジン, "", morph, ref keywordRow);
        //    Assert.AreEqual(keywordRow.Count, 9);
        //    Assert.AreEqual(keywordRow[0].Word, "HUION");
        //    Assert.AreEqual(keywordRow[1].Word, "木");
        //    Assert.AreEqual(keywordRow[2].Word, "Kamvas");
        //    Assert.AreEqual(keywordRow[3].Word, "時");
        //    Assert.AreEqual(keywordRow[4].Word, "Pro");
        //    Assert.AreEqual(keywordRow[5].Word, "HUION木Kamvas時Pro");
        //    Assert.AreEqual(keywordRow[6].Word, "C");
        //    Assert.AreEqual(keywordRow[7].Word, "#");
        //    Assert.AreEqual(keywordRow[8].Word, "C#");

        //    keywordRow = new List<KeywordRow>();
        //    morph = "締切\t名詞,一般,*,*,*,*,締切,シメキリ,シメキリ\n間近\t名詞,副詞可能,*,*,*,*,間近,マヂカ,マジカ\n今\t名詞,副詞可能,*,*,*,*,今,イマ,イマ\nだけ\t助詞,副助詞,*,*,*,*,だけ,ダケ,ダケ\n最大\t名詞,一般,*,*,*,*,最大,サイダイ,サイダイ\n100\t名詞,数,*,*,*,*,*\n万\t名詞,数,*,*,*,*,万,マン,マン\n円\t名詞,接尾,助数詞,*,*,*,円,エン,エン\nキャッシュ\t名詞,一般,*,*,*,*,キャッシュ,キャッシュ,キャッシュ\nバック\t名詞,サ変接続,*,*,*,*,バック,バック,バック\n！\t記号,一般,*,*,*,*,！,！,！\nEOS\n";
        //    AnalysisMeCab.Exec(CollectTargetCategory.メールマガジン, "", morph, ref keywordRow);
        //    Assert.AreEqual(keywordRow.Count, 11);
        //    Assert.AreEqual(keywordRow[0].Word, "締切");
        //    Assert.AreEqual(keywordRow[1].Word, "間近");
        //    Assert.AreEqual(keywordRow[2].Word, "締切");
        //    Assert.AreEqual(keywordRow[3].Word, "今");
        //    Assert.AreEqual(keywordRow[4].Word, "最大");
        //    Assert.AreEqual(keywordRow[5].Word, "100");
        //    Assert.AreEqual(keywordRow[6].Word, "万");
        //    Assert.AreEqual(keywordRow[7].Word, "円");
        //    Assert.AreEqual(keywordRow[8].Word, "キャッシュ");
        //    Assert.AreEqual(keywordRow[9].Word, "バック");
        //    Assert.AreEqual(keywordRow[10].Word, "最大100万円キャッシュバック");

        //    keywordRow = new List<KeywordRow>();
        //    morph = "【\t記号,括弧開,*,*,*,*,【,【,【\nクーポン\t名詞,一般,*,*,*,*,クーポン,クーポン,クーポン\nご\t接頭詞,名詞接続,*,*,*,*,ご,ゴ,ゴ\n利用\t名詞,サ変接続,*,*,*,*,利用,リヨウ,リヨー\nは\t助詞,係助詞,*,*,*,*,は,ハ,ワ\n8\t名詞,数,*,*,*,*,*\n /\t名詞,サ変接続,*,*,*,*,*\n31\t名詞,数,*,*,*,*,*\nまで\t助詞,副助詞,*,*,*,*,まで,マデ,マデ\n】\t記号,括弧閉,*,*,*,*,】,】,】\n技術\t名詞,一般,*,*,*,*,技術,ギジュツ,ギジュツ\n者\t名詞,接尾,一般,*,*,*,者,シャ,シャ\n必見\t名詞,サ変接続,*,*,*,*,必見,ヒッケン,ヒッケン\n！\t記号,一般,*,*,*,*,！,！,！\n注目\t名詞,サ変接続,*,*,*,*,注目,チュウモク,チューモク\nの\t助詞,連体化,*,*,*,*,の,ノ,ノ\n新刊\t名詞,一般,*,*,*,*,新刊,シンカン,シンカン\n『\t記号,括弧開,*,*,*,*,『,『,『\n技術\t名詞,一般,*,*,*,*,技術,ギジュツ,ギジュツ\n者\t名詞,接尾,一般,*,*,*,者,シャ,シャ\nの\t助詞,連体化,*,*,*,*,の,ノ,ノ\nため\t名詞,非自立,副詞可能,*,*,*,ため,タメ,タメ\nの\t助詞,連体化,*,*,*,*,の,ノ,ノ\n線形\t名詞,一般,*,*,*,*,線形,センケイ,センケイ\n代数\t名詞,一般,*,*,*,*,代数,ダイスウ,ダイスー\n学\t名詞,接尾,一般,*,*,*,学,ガク,ガク\n』\t記号,括弧閉,*,*,*,*,』,』,』\nいよいよ\t副詞,一般,*,*,*,*,いよいよ,イヨイヨ,イヨイヨ\n発売\t名詞,サ変接続,*,*,*,*,発売,ハツバイ,ハツバイ\nメール\t名詞,サ変接続,*,*,*,*,メール,メール,メール\nが\t助詞,格助詞,一般,*,*,*,が,ガ,ガ\nうまく\t形容詞,自立,*,*,形容詞・アウオ段,連用テ接続,うまい,ウマク,ウマク\n表示\t名詞,サ変接続,*,*,*,*,表示,ヒョウジ,ヒョージ\nさ\t動詞,自立,*,*,サ変・スル,未然レル接続,する,サ,サ\nれ\t動詞,接尾,*,*,一段,未然形,れる,レ,レ\nない\t助動詞,*,*,*,特殊・ナイ,基本形,ない,ナイ,ナイ\n方\t名詞,非自立,一般,*,*,*,方,ホウ,ホー\nは\t助詞,係助詞,*,*,*,*,は,ハ,ワ\nこちら\t名詞,代名詞,一般,*,*,*,こちら,コチラ,コチラ\nを\t助詞,格助詞,一般,*,*,*,を,ヲ,ヲ\nご覧\t名詞,動詞非自立的,*,*,*,*,ご覧,ゴラン,ゴラン\nください\t動詞,自立,*,*,五段・ラ行特殊,命令ｉ,くださる,クダサイ,クダサイ\n\r\t記号,一般,*,*,*,*,*\nhttp\t名詞,固有名詞,組織,*,*,*,*\n://\t名詞,サ変接続,*,*,*,*,*\nfc\t名詞,一般,*,*,*,*,*\n6086\t名詞,数,*,*,*,*,*\n.\t名詞,サ変接続,*,*,*,*,*\ncuenote\t名詞,一般,*,*,*,*,*\n.\t名詞,サ変接続,*,*,*,*,*\njp\t名詞,一般,*,*,*,*,*\n/\t名詞,サ変接続,*,*,*,*,*\nh\t名詞,一般,*,*,*,*,*\n/\t名詞,サ変接続,*,*,*,*,*\naeeRac\t名詞,一般,*,*,*,*,*\n3\t名詞,数,*,*,*,*,*\nEsRcs\t名詞,一般,*,*,*,*,*\n13\t名詞,数,*,*,*,*,*\nab\t名詞,固有名詞,組織,*,*,*,*\nEOS\n";
        //    AnalysisMeCab.Exec(CollectTargetCategory.メールマガジン, "", morph, ref keywordRow);
        //}

        //[TestMethod]
        //public void 重要語抽出_ケース別()
        //{
        //    List<KeywordRow> keywordRow;

        //    keywordRow = new List<KeywordRow>();
        //    var morph = "クーポン\t名詞,一般,*,*,*,*,クーポン,クーポン,クーポン\n";
        //    AnalysisMeCab.Exec(CollectTargetCategory.メールマガジン, "", morph, ref keywordRow);
        //    Assert.IsTrue(keywordRow.Count == 2);
        //    Assert.IsTrue(keywordRow[0].Word == "クーポン");
        //    Assert.IsTrue(keywordRow[1].Word == "クーポン");


        //    keywordRow = new List<KeywordRow>();
        //    morph = "【\t記号,括弧開,*,*,*,*,【,【,【\nクーポン\t名詞,一般,*,*,*,*,クーポン,クーポン,クーポン\nご\t接頭詞,名詞接続,*,*,*,*,ご,ゴ,ゴ\n";
        //    AnalysisMeCab.Exec(CollectTargetCategory.メールマガジン, "", morph, ref keywordRow);
        //    Assert.IsTrue(keywordRow.Count == 2);
        //    Assert.IsTrue(keywordRow[0].Word == "クーポン");
        //    Assert.IsTrue(keywordRow[1].Word == "クーポン");

        //    keywordRow = new List<KeywordRow>();
        //    morph = "【\t記号,括弧開,*,*,*,*,【,【,【\nクーポン\t名詞,一般,*,*,*,*,クーポン,クーポン,クーポン\n利用\t名詞,サ変接続,*,*,*,*,利用,リヨウ,リヨー\nご\t接頭詞,名詞接続,*,*,*,*,ご,ゴ,ゴ\n";
        //    AnalysisMeCab.Exec(CollectTargetCategory.メールマガジン, "", morph, ref keywordRow);
        //    Assert.IsTrue(keywordRow.Count == 3);
        //    Assert.IsTrue(keywordRow[0].Word == "クーポン");
        //    Assert.IsTrue(keywordRow[1].Word == "利用");
        //    Assert.IsTrue(keywordRow[2].Word == "クーポン利用");


        //    keywordRow = new List<KeywordRow>();
        //    morph = "【\t記号,括弧開,*,*,*,*,【,【,【\nクーポン\t名詞,一般,*,*,*,*,クーポン,クーポン,クーポン\n利用\t名詞,サ変接続,*,*,*,*,利用,リヨウ,リヨー\nご\t接頭詞,名詞接続,*,*,*,*,ご,ゴ,ゴ\n【\t記号,括弧開,*,*,*,*,【,【,【\nクーポン\t名詞,一般,*,*,*,*,クーポン,クーポン,クーポン\nご\t接頭詞,名詞接続,*,*,*,*,ご,ゴ,ゴ\nそれ\t名詞,代名詞,一般,*,*,*,それ,ソレ,ソレ\nδ\t記号,アルファベット,*,*,*,*,δ,デルタ,デルタ\n";
        //    AnalysisMeCab.Exec(CollectTargetCategory.メールマガジン, "", morph, ref keywordRow);
        //    Assert.IsTrue(keywordRow.Count == 7);
        //    Assert.IsTrue(keywordRow[0].Word == "クーポン");
        //    Assert.IsTrue(keywordRow[1].Word == "利用");
        //    Assert.IsTrue(keywordRow[2].Word == "クーポン利用");
        //    Assert.IsTrue(keywordRow[3].Word == "クーポン");
        //    Assert.IsTrue(keywordRow[4].Word == "クーポン");
        //    Assert.IsTrue(keywordRow[5].Word == "それ");
        //    Assert.IsTrue(keywordRow[6].Word == "δ");

        //}

    }
}
