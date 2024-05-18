using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CollectGoogleSearch2;
using System.IO;
using System.Text;
using ExtractEnglishConcatNoun;


namespace UnitTest_x64
{
    [TestClass]
    public class GoogleSearchHtmlParseTest
    {
        [TestMethod]
        public void GoogleSearchHtmlParseTest_1()
        {
            var result = ExtractGoogle.必要な文のみ抽出(new 定数.HtmlParseGoogleRow()
            {
                HtmlTag除外後2段階目 = "<head>aaaa</head>"
            });

            Assert.AreEqual(result, "\r\n");

            Assert.AreEqual(ExtractGoogle.必要な文のみ抽出(
                new 定数.HtmlParseGoogleRow()
                {
                    HtmlTag除外後2段階目 = "<a>"
                }),
                "\r\n");

            Assert.AreEqual(ExtractGoogle.必要な文のみ抽出(
                new 定数.HtmlParseGoogleRow()
                {
                    HtmlTag除外後2段階目 = "</a>"
                }),
                "\r\n");

        }

        [TestMethod]
        public void GoogleSearchHtmlParseTest_2()
        {
            //var file = Directory.GetCurrentDirectory() + @"..\..\..\TestData\GoogleResponse_サクラ.txt";
            //var file = @"..\..\..\TestData\GoogleResponse_サクラ.html";
            var file = @"..\..\..\TestData\GoogleResponse_サクラ_plane.html";
            var fileAfter = @"..\..\..\TestData\GoogleResponse_サクラ_after.html";

            var data = "";
            using (var sr = new StreamReader(file, Encoding.UTF8))
            {
                data = sr.ReadToEnd();
            }

            var result = ExtractGoogle.必要な文のみ抽出(new 定数.HtmlParseGoogleRow()
            {
                HtmlTag除外後2段階目 = data
            });

            //File.WriteAllText(fileAfter, result, Encoding.UTF8);
        }

        [TestMethod]
        public void GoogleSearchHtmlParseTest_3()
        {
            var html = "</div><div style=\"display:none\" class=\"am-dropdown-menu\" role=\"menu\" tabindex=\"-1\"><ul><li class=\"mUpfKd\"><a class=\"imx0m\" href=\"/url?q=http://webcache.googleusercontent.com/search%3Fq%3Dcache:OFV_MxX9368J:http://chtgkato3.med.hokudai.ac.jp/kougi/Tommy/C1.pdf%252BC%2523%26num%3D20%26oe%3DUTF-8%26hl%3Dja%26ct%3Dclnk&amp;sa=U&amp;ved=0ahUKEwiio8-P6rPfAhWO62EKHeoJCxEQIAiSATAT&amp;usg=AOvVaw05Ym7s5wJPflGnGcA8qFaL\">キャッシュ</a></li><li class=\"mUpfKd\"><a class=\"imx0m\" href=\"/search?ie=UTF-8&amp;oe=UTF-8&amp;q=related:chtgkato3.med.hokudai.ac.jp/kougi/Tommy/C1.pdf+C%23&amp;tbo=1&amp;sa=X&amp;ved=0ahUKEwiio8-P6rPfAhWO62EKHeoJCxEQHwiTATAT\">類似ページ</a></li></ul></div></div></div><span class=\"st\"><b>C#</b>とは. • プログラミング言語のひとつでありC、C++、. Java等に並ぶ代表的な言語の<br>\n一つである. • 容易にGUI（グラフィックやボタンとの連携が. できる）プログラミングが可能<br>\nである. • メモリ管理等の煩雑な操作が必要なく、比較. 的初心者向きの言語である&nbsp;...</span><br></div></div></ol></div></div></div></div>";

            var result = ExtractGoogle.必要な文のみ抽出(new 定数.HtmlParseGoogleRow()
            {
                HtmlTag除外後2段階目 = html
            });
        }

        [TestMethod]
        public void GoogleSearchHtmlParseTest_4()
        {
            var file = @"..\..\..\TestData\GoogleResponse_C#.html";

            var data = "";
            using (var sr = new StreamReader(file, Encoding.UTF8))
            {
                data = sr.ReadToEnd();
            }

            var result = ExtractGoogle.必要な文のみ抽出(new 定数.HtmlParseGoogleRow()
            {
                HtmlTag除外後2段階目 = data
            });

            var fileAfter = @"..\..\..\TestData\GoogleResponse_C#_after.html";
            //File.WriteAllText(fileAfter, string.Join("\r\n", result.Item1.ToArray()) + "\r\n" + result.Item2, Encoding.UTF8);
        }

        [TestMethod]
        public void GoogleSearchHtmlParseTest_5()
        {
            var file = @"..\..\..\TestData\GoogleResponse_サクラ.html";

            var data = "";
            using (var sr = new StreamReader(file, Encoding.UTF8))
            {
                data = sr.ReadToEnd();
            }

            var result = ExtractGoogle.必要な文のみ抽出(new 定数.HtmlParseGoogleRow()
            {
                HtmlTag除外後2段階目 = data
            });

            //File.WriteAllText(fileAfter, result, Encoding.UTF8);
        }

        [TestMethod]
        public void GoogleSearchHtmlParseTest_6()
        {
            var file = @"..\..\..\TestData\GoogleResponse_Toy-Con01 VARIETY KIT_HtmlTag除外2段階目.html";

            var data = "";
            using (var sr = new StreamReader(file, Encoding.UTF8))
            {
                data = sr.ReadToEnd();
            }

            var result = ExtractGoogle.必要な文のみ抽出(new 定数.HtmlParseGoogleRow()
            {
                HtmlTag除外後2段階目 = data
            });

            //File.WriteAllText(fileAfter, result, Encoding.UTF8);
        }

        [TestMethod]
        public void GoogleSearchHtmlParseTest_7()
        {
            var file = @"..\..\..\TestData\GoogleResponse_C#_HtmlTag除外2段階目.html";

            var data = "";
            using (var sr = new StreamReader(file, Encoding.UTF8))
            {
                data = sr.ReadToEnd();
            }

            var result = ExtractGoogle.必要な文のみ抽出(new 定数.HtmlParseGoogleRow()
            {
                HtmlTag除外後2段階目 = data
            });

            //File.WriteAllText(fileAfter, result, Encoding.UTF8);
        }

    }
}
