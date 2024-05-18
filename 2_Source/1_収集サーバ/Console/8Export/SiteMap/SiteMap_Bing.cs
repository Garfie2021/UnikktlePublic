using System;
using System.Collections.Generic;
using System.IO;
using System.Data.SqlClient;
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
    public static class SiteMap_Bing
    {
        public static void ExecuteCreateFile()
        {
            ログ.ログ書き出し($"SiteMap CreateFile 開始");

            SiteMapCmn.FixedPage(AppSetting.OutputFolder_SiteMap_Bing);

            using (var cn = new SqlConnection())
            {
                cn.ConnectionString = AppSetting.ConnectionString_UnikktleWebCollectWork_CollectServer;
                cn.Open();

                SearchPageWrite(cn);
                MapPageWrite(cn);
            }

            using (var cn = new SqlConnection())
            {
                cn.ConnectionString = AppSetting.ConnectionString_UnikktleWeb;
                cn.Open();

                MindPageWrite(cn);
            }
        }

        private static void SearchPageWrite(SqlConnection cn)
        {
            var str = new StringBuilder(SiteMapCmn.MaxFileSize);

            SiteMapCmn.HeaderWrite(ref str);

            using (var reader = DB.WebCollectWork.SP_CollaborateKeywordCount_CollectServer.Select30RowOver_Word(cn))
            {
                int cnt = 0;
                long rowCnt = 0;
                while (reader.Read() == true)
                {
                    str.Append(string.Format(SiteMapCmn.XmlTag_UrlFormat, SiteMapCmn.Url_Search + HttpUtility.UrlEncode((string)reader["Word"])));

                    cnt++;
                    rowCnt++;

                    if (cnt < 100000)
                    {
                        continue;
                    }
                    else
                    {
                        ログ.ログ書き出し($"SiteMap row count. rowCnt:{rowCnt} cnt:{cnt}");

                        cnt = 0;
                    }
                }
            }

            SiteMapCmn.FooterWrite(ref str);

            File.WriteAllText(AppSetting.OutputFolder_SiteMap_Bing + "Search_All.xml", str.ToString(), Encoding.UTF8);
        }

        private static void MapPageWrite(SqlConnection cn)
        {
            var str = new StringBuilder(SiteMapCmn.MaxFileSize);

            SiteMapCmn.HeaderWrite(ref str);

            //var testData = new long[3] { 1, 2, 3 };
            using (var reader = DB.WebCollectWork.SP_CollaborateKeywordCount_CollectServer.Select30RowOver(cn))
            {
                int cnt = 0;
                long rowCnt = 0;
                while (reader.Read() == true)
                {
                    str.Append(string.Format(SiteMapCmn.XmlTag_UrlFormat, SiteMapCmn.Url_Map + reader["No"]));

                    cnt++;
                    rowCnt++;

                    if (cnt < 100000)
                    {
                        continue;
                    }
                    else
                    {
                        ログ.ログ書き出し($"SiteMap row count. rowCnt:{rowCnt} cnt:{cnt}");

                        cnt = 0;
                    }
                }
            }

            SiteMapCmn.FooterWrite(ref str);

            File.WriteAllText(AppSetting.OutputFolder_SiteMap_Bing + "Map_All.xml", str.ToString(), Encoding.UTF8);
        }

        private static void MindPageWrite(SqlConnection cn)
        {
            var str = new StringBuilder(SiteMapCmn.MaxFileSize);

            SiteMapCmn.HeaderWrite(ref str);

            //var testData = new long[3] { 1, 2, 3 };
            using (var reader = DB.Web.SP_Mind.Select_AllNo(cn))
            {
                int cnt = 0;
                long rowCnt = 0;
                while (reader.Read() == true)
                {
                    str.Append(string.Format(SiteMapCmn.XmlTag_UrlFormat, SiteMapCmn.Url_Mind + reader["No"]));

                    cnt++;
                    rowCnt++;

                    if (cnt < 100000)
                    {
                        continue;
                    }
                    else
                    {
                        ログ.ログ書き出し($"[Mind] SiteMap row count. rowCnt:{rowCnt} cnt:{cnt}");

                        cnt = 0;
                    }
                }
            }

            SiteMapCmn.FooterWrite(ref str);

            File.WriteAllText(AppSetting.OutputFolder_SiteMap_Bing + "Mind_All.xml", str.ToString(), Encoding.UTF8);

            ログ.ログ書き出し($"[Mind] 作成終了。");
        }

    }
}
