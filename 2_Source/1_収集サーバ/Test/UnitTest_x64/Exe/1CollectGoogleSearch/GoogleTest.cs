using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CollectGoogleSearch2;
using System.IO;
using System.Text;
using 定数;



namespace UnitTest_x64
{
    [TestClass]
    public class GoogleTest
    {
        [TestMethod]
        public void GoogleTest_1()
        {
            var keywordRow = new KeywordRow()
            {
                //TargetCategory = TargetCategory.IT,
                Word = "サクラ"
            };

            var result = GoogleSearch.Search(keywordRow);
        }

        [TestMethod]
        public void GoogleTest_2()
        {
            var file = @"..\..\..\TestData\GoogleResponse_サクラ_plane.html";

            var keywordRow = new KeywordRow()
            {
                //TargetCategory = TargetCategory.IT,
                Word = "サクラ"
            };

            var result = GoogleSearch.Search(keywordRow);

            File.AppendAllText(file, result, Encoding.UTF8);
        }

    }
}
