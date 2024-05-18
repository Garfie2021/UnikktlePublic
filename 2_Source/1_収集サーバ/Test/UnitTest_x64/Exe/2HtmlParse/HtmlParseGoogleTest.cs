using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CollectGoogleSearch2;
using System.IO;
using System.Text;
using ExtractEnglishConcatNoun;
using _2HtmlParse;
using Common;

namespace UnitTest_x64
{
    [TestClass]
    public class HtmlParseGoogleTest
    {
        [TestMethod]
        public void TestHtmlTag除外_ToyCon01_VARIETY_KIT()
        {
            TestHtmlTag除外(
                @"..\..\..\TestData\GoogleResponse_Toy-Con01 VARIETY KIT.html",
                @"..\..\..\TestData\GoogleResponse_Toy-Con01 VARIETY KIT_HtmlTag除外1段階目.html",
                @"..\..\..\TestData\GoogleResponse_Toy-Con01 VARIETY KIT_HtmlTag除外2段階目.html");
        }

        [TestMethod]
        public void TestHtmlTag除外_C()
        {
            TestHtmlTag除外(
                @"..\..\..\TestData\GoogleResponse_C#.html",
                @"..\..\..\TestData\GoogleResponse_C#_HtmlTag除外1段階目.html",
                @"..\..\..\TestData\GoogleResponse_C#_HtmlTag除外2段階目.html");
        }

        private void TestHtmlTag除外(
            string inFile, string outFile1段階目, string outFile2段階目)
        {
            var file = inFile;

            var data = "";
            using (var sr = new StreamReader(file, Encoding.UTF8))
            {
                data = sr.ReadToEnd();
            }

            HtmlParseGoogle.HtmlTag除外1段階目_PaternG(data,
                out var html1段階目, out var status1段階目);

            File.WriteAllText(outFile1段階目, data, Encoding.UTF8);

            HtmlParse.一般HtmlTag除外(html1段階目,
                out var html2段階目, out var status2段階目);

            File.WriteAllText(outFile2段階目, data, Encoding.UTF8);
        }
    }
}
