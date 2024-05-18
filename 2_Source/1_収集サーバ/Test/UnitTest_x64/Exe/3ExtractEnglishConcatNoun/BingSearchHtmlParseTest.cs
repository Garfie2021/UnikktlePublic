using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Text;
using CollectBingSearch;
using ExtractEnglishConcatNoun;

namespace UnitTest_x64
{
    [TestClass]
    public class BingSearchHtmlParseTest
    {
        [TestMethod]
        public void BingSearchHtmlParseTest_2()
        {
            //var file = Directory.GetCurrentDirectory() + @"..\..\..\TestData\GoogleResponse_サクラ.txt";
            //var file = @"..\..\..\TestData\GoogleResponse_サクラ.html";
            var file = @"..\..\..\TestData\BingResponse_C#.html";
            //var fileAfter = @"..\..\..\TestData\BingResponse_C#_after.html";

            var data = "";
            using (var sr = new StreamReader(file, Encoding.UTF8))
            {
                data = sr.ReadToEnd();
            }

            var result = ExtractBing.必要な文のみ抽出(new 定数.HtmlParseBingRow()
            {
                HtmlTag除外後2段階目 = data
            });

            //File.WriteAllText(fileAfter, result, Encoding.UTF8);
        }

        [TestMethod]
        public void BingSearchHtmlParseTest_3()
        {
            //var file = Directory.GetCurrentDirectory() + @"..\..\..\TestData\GoogleResponse_サクラ.txt";
            //var file = @"..\..\..\TestData\GoogleResponse_サクラ.html";
            var file = @"..\..\..\TestData\BingResponse_Courses+Online+Updated+January+2019.html";
            //var fileAfter = @"..\..\..\TestData\BingResponse_C#_after.html";

            var data = "";
            using (var sr = new StreamReader(file, Encoding.UTF8))
            {
                data = sr.ReadToEnd();
            }

            var result = ExtractBing.必要な文のみ抽出(new 定数.HtmlParseBingRow()
            {
                HtmlTag除外後2段階目 = data
            });

            //File.WriteAllText(fileAfter, result, Encoding.UTF8);
        }

        [TestMethod]
        public void BingSearchHtmlParseTest_4()
        {
            //var file = Directory.GetCurrentDirectory() + @"..\..\..\TestData\GoogleResponse_サクラ.txt";
            //var file = @"..\..\..\TestData\GoogleResponse_サクラ.html";
            var file = @"..\..\..\TestData\BingResponse_CentOS6CentOS7+Qiita.html";
            //var fileAfter = @"..\..\..\TestData\BingResponse_C#_after.html";

            var data = "";
            using (var sr = new StreamReader(file, Encoding.UTF8))
            {
                data = sr.ReadToEnd();
            }

            var result = ExtractBing.必要な文のみ抽出(new 定数.HtmlParseBingRow()
            {
                HtmlTag除外後2段階目 = data
            });

            //File.WriteAllText(fileAfter, result, Encoding.UTF8);
        }

        [TestMethod]
        public void BingSearchHtmlParseTest_このページを翻訳()
        {
            //var file = Directory.GetCurrentDirectory() + @"..\..\..\TestData\GoogleResponse_サクラ.txt";
            //var file = @"..\..\..\TestData\GoogleResponse_サクラ.html";
            var file = @"..\..\..\TestData\BingResponse_このページを翻訳.html";
            //var fileAfter = @"..\..\..\TestData\BingResponse_C#_after.html";

            var data = "";
            using (var sr = new StreamReader(file, Encoding.UTF8))
            {
                data = sr.ReadToEnd();
            }

            var result = ExtractBing.必要な文のみ抽出(new 定数.HtmlParseBingRow()
            {
                HtmlTag除外後2段階目 = data
            });

            //File.WriteAllText(fileAfter, result, Encoding.UTF8);
        }

    }
}
