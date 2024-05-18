using System;
using System.IO;
using System.Text;


namespace Logging
{
    public static class ログ
    {
        private static object _lockObj = new object();

        public static string ログフォルダ;
        public static string HtmlParse_ErrorHtmlFolder;
        public static string ExtractEnglishConcatNoun_ErrorHtmlFolder;
        public static string ExtractEnglishConcatNoun_WarningHtmlFolder;
        public static string ExtractEnglishConcatNoun_ErrorMailFolder;
        public static string プログラム名;


        public static string ログファイルPath()
        {
            return ログフォルダ + @"\" + DateTime.Now.ToString("yyyyMMdd") + $"_{プログラム名}.log";
        }

        public static void ログ書き出し(Exception ex)
        {
            lock(_lockObj)
            {
                ログ.ログ書き出し("【Exception】" + "\r\n" + ex.Data + "\r\n" + ex.HelpLink + "\r\n" + ex.InnerException + "\r\n" + ex.Message + "\r\n" + ex.Source + "\r\n" + ex.StackTrace + "\r\n" + ex.TargetSite + "\r\n");
            }
        }

        public static void ログ書き出し(string ログ)
        {
            lock (_lockObj)
            {
                File.AppendAllText(ログファイルPath(), DateTime.Now.ToString() + " [" + プログラム名 + "] " + ログ + "\r\n", Encoding.UTF8);
            }
        }

        public static void Write_HtmlParse_ErrorHtml(string fileName, string html)
        {
            lock (_lockObj)
            {
                File.AppendAllText(HtmlParse_ErrorHtmlFolder + "\\" + fileName, html, Encoding.UTF8);
            }
        }

        public static void Write_ExtractEnglishConcatNoun_ErrorHtml(string fileName, string html)
        {
            lock (_lockObj)
            {
                File.AppendAllText(ExtractEnglishConcatNoun_ErrorHtmlFolder + "\\" + fileName, html, Encoding.UTF8);
            }
        }
        public static void Write_ExtractEnglishConcatNoun_WarningHtml(string fileName, string html)
        {
            lock (_lockObj)
            {
                File.AppendAllText(ExtractEnglishConcatNoun_WarningHtmlFolder + "\\" + fileName, html, Encoding.UTF8);
            }
        }

        public static void Write_ExtractEnglishConcatNoun_ErrorMail(string fileName, string Mail)
        {
            lock (_lockObj)
            {
                File.AppendAllText(ExtractEnglishConcatNoun_ErrorMailFolder + "\\" + fileName, Mail, Encoding.UTF8);
            }
        }
    }
}
