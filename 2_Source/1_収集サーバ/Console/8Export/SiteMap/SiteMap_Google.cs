using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Renci.SshNet;
using 定数;
using DB;
using Logging;
using AppDirectory;
using Common;


namespace _8Export.SiteMap
{
    public static class SiteMap_Google
    {

        public static void ExecuteCreateFile()
        {
            ログ.ログ書き出し($"SiteMap CreateFile 開始");

            SiteMapCmn.FixedPage(AppSetting.OutputFolder_SiteMap_Google);

            var fileList = new List<string>();
            using (var cnCollect = new SqlConnection())
            {
                cnCollect.ConnectionString = AppSetting.ConnectionString_UnikktleWebCollectWork_CollectServer;
                cnCollect.Open();

                SearchPageWrite(cnCollect, ref fileList);
                MapPageWrite(cnCollect, ref fileList);
            }

            using (var cn = new SqlConnection())
            {
                cn.ConnectionString = AppSetting.ConnectionString_UnikktleWeb;
                cn.Open();

                MindPageWrite(cn, ref fileList);
            }

            IndexWrite(fileList);
        }

        private static void SearchPageWrite(SqlConnection cn, ref List<string> fileList)
        {
            var str = new StringBuilder(SiteMapCmn.MaxFileSize);

            long rowCntAll = 0;
            int rowCnt = 0;
            int fileCnt = 1;
            var start = true;

            //var testData = new long[3] { 1, 2, 3 };
            using (var reader = DB.WebCollectWork.SP_CollaborateKeywordCount_CollectServer.Select30RowOver_Word(cn))
            {
                while (reader.Read() == true)
                {
                    rowCnt++;
                    rowCntAll++;

                    if (start)
                    {
                        ログ.ログ書き出し($"SiteMap SearchPage Header .  rowCntAll : {rowCntAll}  rowCnt : {rowCnt}  fileCnt : {fileCnt}");

                        SiteMapCmn.HeaderWrite(ref str);

                        start = false;
                    }

                    str.Append(string.Format(SiteMapCmn.XmlTag_UrlFormat, SiteMapCmn.Url_Search + HttpUtility.UrlEncode((string)reader["Word"])));

                    if (rowCnt < 50000)    // Googleに登録可能な1ファイル辺りのURL最大数
                    {
                        continue;
                    }
                    else
                    {
                        ログ.ログ書き出し($"SiteMap SearchPage file Create.  rowCntAll : {rowCntAll}  rowCnt : {rowCnt}  fileCnt : {fileCnt}");

                        SiteMapCmn.FooterWrite(ref str);

                        var file = "Search_" + fileCnt.ToString() + ".xml";
                        fileList.Add(file);

                        File.WriteAllText(AppSetting.OutputFolder_SiteMap_Google + file, str.ToString(), Encoding.UTF8);

                        start = true;
                        rowCnt = 0;
                        fileCnt++;
                        str.Clear();
                    }
                }
            }

            var body = str.ToString();
            if (!string.IsNullOrEmpty(body))
            {
                ログ.ログ書き出し($"SiteMap SearchPage file Create.  rowCntAll : {rowCntAll}  rowCnt : {rowCnt}  fileCnt : {fileCnt}");

                var file = "Search_" + fileCnt.ToString() + ".xml";
                fileList.Add(file);

                File.WriteAllText(AppSetting.OutputFolder_SiteMap_Google + "Search_" + fileCnt.ToString() + ".xml",
                    body + SiteMapCmn.Footer,
                    Encoding.UTF8);
            }
        }

        private static void MapPageWrite(SqlConnection cn, ref List<string> fileList)
        {
            var str = new StringBuilder(SiteMapCmn.MaxFileSize);

            long rowCntAll = 0;
            int rowCnt = 0;
            long fileCnt = 1;
            var start = true;

            //var testData = new long[3] { 1, 2, 3 };
            using (var reader = DB.WebCollectWork.SP_CollaborateKeywordCount_CollectServer.Select30RowOver(cn))
            {
                while (reader.Read() == true)
                {
                    rowCnt++;
                    rowCntAll++;

                    if (start)
                    {
                        ログ.ログ書き出し($"SiteMap MapPage Header .  rowCntAll : {rowCntAll}  rowCnt : {rowCnt}  fileCnt : {fileCnt}");

                        SiteMapCmn.HeaderWrite(ref str);

                        start = false;
                    }

                    str.Append(string.Format(SiteMapCmn.XmlTag_UrlFormat, SiteMapCmn.Url_Map + reader["No"]));

                    if (rowCnt < 50000)    // Googleに登録可能な1ファイル辺りのURL最大数
                    {
                        continue;
                    }
                    else
                    {
                        ログ.ログ書き出し($"SiteMap MapPage file Create.  rowCntAll : {rowCntAll}  rowCnt : {rowCnt}  fileCnt : {fileCnt}");

                        SiteMapCmn.FooterWrite(ref str);

                        var file = "Map_" + fileCnt.ToString() + ".xml";
                        fileList.Add(file);

                        File.WriteAllText(AppSetting.OutputFolder_SiteMap_Google + file, str.ToString(), Encoding.UTF8);

                        start = true;
                        rowCnt = 0;
                        fileCnt++;
                        str.Clear();
                    }
                }
            }

            var body = str.ToString();
            if (!string.IsNullOrEmpty(body))
            {
                ログ.ログ書き出し($"SiteMap MapPage file Create.  rowCntAll : {rowCntAll}  rowCnt : {rowCnt}  fileCnt : {fileCnt}");

                var file = "Map_" + fileCnt.ToString() + ".xml";
                fileList.Add(file);

                File.WriteAllText(AppSetting.OutputFolder_SiteMap_Google + "Map_" + fileCnt.ToString() + ".xml",
                    body  + SiteMapCmn.Footer,
                    Encoding.UTF8);
            }
        }

        private static void MindPageWrite(SqlConnection cn, ref List<string> fileList)
        {
            var str = new StringBuilder(SiteMapCmn.MaxFileSize);

            long rowCntAll = 0;
            int rowCnt = 0;
            long fileCnt = 1;
            var start = true;

            //var testData = new long[3] { 1, 2, 3 };
            using (var reader = DB.Web.SP_Mind.Select_AllNo(cn))
            {
                while (reader.Read() == true)
                {
                    rowCnt++;
                    rowCntAll++;

                    if (start)
                    {
                        ログ.ログ書き出し($"[Mind] SiteMap MapPage Header .  rowCntAll : {rowCntAll}  rowCnt : {rowCnt}  fileCnt : {fileCnt}");

                        SiteMapCmn.HeaderWrite(ref str);

                        start = false;
                    }

                    str.Append(string.Format(SiteMapCmn.XmlTag_UrlFormat, SiteMapCmn.Url_Mind + reader["No"]));

                    if (rowCnt < 50000)    // Googleに登録可能な1ファイル辺りのURL最大数
                    {
                        continue;
                    }
                    else
                    {
                        ログ.ログ書き出し($"[Mind] SiteMap MapPage file Create.  rowCntAll : {rowCntAll}  rowCnt : {rowCnt}  fileCnt : {fileCnt}");

                        SiteMapCmn.FooterWrite(ref str);

                        var file = "Mind_" + fileCnt.ToString() + ".xml";
                        fileList.Add(file);

                        File.WriteAllText(AppSetting.OutputFolder_SiteMap_Google + file, str.ToString(), Encoding.UTF8);

                        start = true;
                        rowCnt = 0;
                        fileCnt++;
                        str.Clear();
                    }
                }
            }

            var body = str.ToString();
            if (!string.IsNullOrEmpty(body))
            {
                ログ.ログ書き出し($"[Mind] SiteMap MapPage file Create.  rowCntAll : {rowCntAll}  rowCnt : {rowCnt}  fileCnt : {fileCnt}");

                var file = "Mind_" + fileCnt.ToString() + ".xml";
                fileList.Add(file);

                File.WriteAllText(AppSetting.OutputFolder_SiteMap_Google + "Mind_" + fileCnt.ToString() + ".xml",
                    body + SiteMapCmn.Footer,
                    Encoding.UTF8);
            }
        }

        private static void IndexWrite(List<string> fileList)
        {
            var str = new StringBuilder(SiteMapCmn.MaxFileSize);

            str.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            str.Append("<sitemapindex xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\">");

            const string UrlFormat = "<sitemap><loc>https://xxx/sitemap/google/{0}</loc></sitemap>";

            foreach (var file in fileList)
            {
                str.Append(string.Format(UrlFormat, file));
            }

            str.Append("</sitemapindex>");

            File.WriteAllText(AppSetting.OutputFolder_SiteMap_Google + "Index.xml",
                str.ToString(),
                Encoding.UTF8);
        }
    }
}
