using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using 定数;
using Logging;
using AppDirectory;

namespace Common
{
    public static class ExtractEnglishConcatNounCommon
    {
        public static void Warning(string searchName,
            long SearchKeywordNo, DateTime SearchDate, string Status, string Html)
        {
            var mailBody = "";
            var outHtmlFileName = "";

            outHtmlFileName = searchName + "_" + SearchKeywordNo + "_" + SearchDate.ToString("yyyyMMddHHmmss") + ".html";

            ログ.Write_ExtractEnglishConcatNoun_WarningHtml(outHtmlFileName, Html);

            mailBody +=
                "SearchKeywordNo: " + SearchKeywordNo + "\r\n" +
                "SearchDate: " + SearchDate + "\r\n" +
                "status: " + Status + "\r\n" +
                "outHtmlFileName: " + outHtmlFileName + "\r\n\r\n";

            Mail.SendMailAndLogErr("[ExtractEnglishConcatNoun] " + searchName + " Warning", mailBody);
        }
    }
}
