//using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using CollectYahooSearch;
//using System.IO;
//using System.Text;

//namespace UnitTest_x64
//{
//    public class YahooSearchHtmlParseTest
//    {
//        [TestMethod]
//        public void YahooSearchHtmlParseTest_2()
//        {
//            //var file = Directory.GetCurrentDirectory() + @"..\..\..\TestData\GoogleResponse_サクラ.txt";
//            //var file = @"..\..\..\TestData\GoogleResponse_サクラ.html";
//            var file = @"..\..\..\TestData\YahooResponse_C#_wタグ無し.html";
//            var fileAfter = @"..\..\..\TestData\YahooResponse_C#_wタグ無し_after.html";

//            var data = "";
//            using (var sr = new StreamReader(file, Encoding.UTF8))
//            {
//                data = sr.ReadToEnd();
//            }

//            var result = YahooSearchHtmlParse.必要な文のみ抽出(data);

//            //File.WriteAllText(fileAfter, result, Encoding.UTF8);
//        }

//    }
//}
