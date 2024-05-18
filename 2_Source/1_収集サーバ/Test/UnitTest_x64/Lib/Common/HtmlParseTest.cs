using System;
using CollectGoogleSearch2;
using System.IO;
using System.Text;
using System.Web;
using ExtractEnglishConcatNoun;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Common;

namespace UnitTest_x64
{
    [TestClass]
    public class HtmlParseTest
    {
        [TestMethod]
        public void HttpUtility_1()
        {
            var html = "<style><style></style>";
            var aa = HttpUtility.HtmlEncode(html);
            var bb = HttpUtility.HtmlDecode(aa);

            var file = @"..\..\..\TestData\GoogleResponse_C#.html";
            using (var sr = new StreamReader(file, Encoding.UTF8))
            {
                html = sr.ReadToEnd();
                var cc = HttpUtility.HtmlEncode(html);
                var dd = HttpUtility.HtmlDecode(aa);
            }
        }

        [TestMethod]
        public void HtmlParse_HtmlTag除外1段階目_1()
        {
            var file = @"..\..\..\TestData\GoogleResponse_C#.html";
            var html = "";
            using (var sr = new StreamReader(file, Encoding.UTF8))
            {
                html = sr.ReadToEnd();
            }

            var result = HtmlParse.一般HtmlTag除外(html,
                out var html2段階目, out var status2段階目);
        }

    }
}
