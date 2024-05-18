using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using 定数;
using Common;

namespace UnitTest_x64
{
    [TestClass]
    public class AnalysisMeCabTest
    {
        [TestMethod]
        public void AnalysisMeCabTest_開始終了が名詞か判別_1()
        {
            var morph = "";
            var 開始が名詞 = false;
            var 終了が名詞 = false;

            morph = "C\t名詞,固有名詞,組織,*,*,*,*\nEOS\n";
            AnalysisMeCab.開始終了が名詞か判別(morph, out 開始が名詞, out 終了が名詞);
            Assert.AreEqual(開始が名詞, true);
            Assert.AreEqual(終了が名詞, true);

            morph = "?\t名詞,固有名詞,組織,*,*,*,*\nEOS\n";
            AnalysisMeCab.開始終了が名詞か判別(morph, out 開始が名詞, out 終了が名詞);
            Assert.AreEqual(開始が名詞, false);
            Assert.AreEqual(終了が名詞, false);

            morph = "C\t名詞,固有名詞,組織,*,*,*,*\n#\t名詞,サ変接続,*,*,*,*,*\nEOS\n";
            AnalysisMeCab.開始終了が名詞か判別(morph, out 開始が名詞, out 終了が名詞);
            Assert.AreEqual(開始が名詞, true);
            Assert.AreEqual(終了が名詞, true);

            // 全パターンをその内書く
        }

        [TestMethod]
        public void AnalysisMeCabTest_文節抽出_まとめ_1()
        {
            var 名詞 = "";

            名詞 = "y";
            AnalysisMeCab.名詞連結("y", "_", ref 名詞);
            Assert.AreEqual(名詞, "y_");

            名詞 = "_";
            AnalysisMeCab.名詞連結("_", "s", ref 名詞);
            Assert.AreEqual(名詞, "_s");
        }
    }
}
