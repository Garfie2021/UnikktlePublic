using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using 定数;
using Common;
using Logging;
using AppDirectory;
using DB;
using MeCabExec;


namespace UnitTest_x86
{
    [TestClass]
    public class KeywordListUpTest
    {
        [TestMethod]
        public void KeywordListUp_Exec_1()
        {
            var ptrMeCab = MeCabConst.mecab_new2("");

            var 文節 = "C#";
            var list = KeywordListUp.Exec(ptrMeCab, CollectTargetCategory.Bing検索, 0, 0, null, 文節);
            Assert.AreEqual(list.Count, 1);
            Assert.AreEqual(list[0].Word, "C#");

            文節 = "C# 未確認 C Sharp 画像";
            list = KeywordListUp.Exec(ptrMeCab, CollectTargetCategory.Bing検索, 0, 0, null, 文節);
            Assert.AreEqual(list.Count, 1);
            Assert.AreEqual(list[0].Word, "C# 未確認 C Sharp 画像");

            文節 = " 演算子 - C# リファレンス ";
            list = KeywordListUp.Exec(ptrMeCab, CollectTargetCategory.Bing検索, 0, 0, null, 文節);
            Assert.AreEqual(list.Count, 1);
            Assert.AreEqual(list[0].Word, "演算子 - C# リファレンス");


            // MeCab開放
            MeCabConst.mecab_destroy(ptrMeCab);

        }
    }
}
