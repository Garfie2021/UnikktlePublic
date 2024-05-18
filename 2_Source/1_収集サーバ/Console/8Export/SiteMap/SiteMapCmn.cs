using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8Export.SiteMap
{
    public static class SiteMapCmn
    {
        public const string XmlTag_UrlFormat = "<url><loc>{0}</loc><changefreq>monthly</changefreq></url>";

        public const string Url_Search = "https://xxx/Home/Search?s=";
        public const string Url_Map = "https://xxx/Home/WordMap/";
        public const string Url_Mind = "https://xxx/Mind/Mind?i=";

        public const int MaxFileSize = 500 * 1024 * 1024;

        public const string Footer = "</urlset>";

        // ヘッダー
        public static void HeaderWrite(ref StringBuilder str)
        {
            str.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?><urlset xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\">");
        }

        // 終了タグ
        public static void FooterWrite(ref StringBuilder str)
        {
            str.Append(Footer);
        }

        public static void FixedPage(string outputFolder)
        {
            var str = new StringBuilder(SiteMapCmn.MaxFileSize);

            SiteMapCmn.HeaderWrite(ref str);

            str.Append(string.Format(SiteMapCmn.XmlTag_UrlFormat, "https://xxx/"));
            str.Append(string.Format(SiteMapCmn.XmlTag_UrlFormat, "https://xxx/About"));
            str.Append(string.Format(SiteMapCmn.XmlTag_UrlFormat, "https://xxx/Privacy"));

            SiteMapCmn.FooterWrite(ref str);

            File.WriteAllText(outputFolder + "FixedPage.xml", str.ToString(), Encoding.UTF8);
        }
    }
}
