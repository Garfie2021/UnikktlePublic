using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Renci.SshNet;
using 定数;
using DB;
using Logging;
using AppDirectory;
using Common;

namespace _8Export.MstKeyword
{
    public static class Worker_MstKeyword
    {
        public static void Exec_FromCollectServer_ToWebServer_MstKeyword(DateTime now)
        {
            var strNow = now.ToString("yyyy/MM/dd HH:mm:ss");

            var log = "";
            using (var cnCollect = new SqlConnection())
            using (var cnWeb = new SqlConnection())
            using (var cnWebCollectWork_WebServer = new SqlConnection())
            using (var cnMsdb = new SqlConnection())
            {
                cnCollect.ConnectionString = AppSetting.ConnectionString_UnikktleCollect;
                cnCollect.Open();

                cnWeb.ConnectionString = AppSetting.ConnectionString_UnikktleWeb;
                cnWeb.Open();

                cnWebCollectWork_WebServer.ConnectionString = AppSetting.ConnectionString_UnikktleWebCollectWork_WebServer;
                cnWebCollectWork_WebServer.Open();

                cnMsdb.ConnectionString = AppSetting.ConnectionString_Msdb;
                cnMsdb.Open();


                //SP_ReCreateTempTable.ReCreateTemptable_CollectTargetKeyword();

                Exec_MstKeyword(cnCollect, cnMsdb, strNow, cnWebCollectWork_WebServer, ref log);
            }
        }

        private static void Exec_MstKeyword(SqlConnection cnCollect, SqlConnection cnMsdb, 
            string strNow, SqlConnection cnWebCollectWork_WebServer, ref string log)
        {
            try
            {
                ログ.ログ書き出し("[Mst.Keyword] 開始");

                ログ.ログ書き出し("エクスポート元の件数取得 開始");
                var cntCollect = DB.Collect.SP_Keyword.GetCount(cnCollect);
                ログ.ログ書き出し($"[Mst.Keyword] エクスポート元の件数　cntCollect：{cntCollect}");

                ログ.ログ書き出し($"WebサーバのWorkデータベースへインポートする前に古いデータを削除 開始");
                DB.WebCollectWork.SP_Keyword.Truncate(cnWebCollectWork_WebServer);

                ログ.ログ書き出し("Collectサーバの計算結果をエクスポート 開始");

                int cnt = 0;
                long rowCnt = 0;
                using (var reader = DB.Collect.SP_Keyword.Select_更新日時(cnCollect,
                    DateTime.Parse(AppSetting.AppData.BcpExportLastDate_MstKeyword), DateTime.Parse(strNow)))
                {
                    while (reader.Read() == true)
                    {
                        DB.WebCollectWork.BulkCopy_Keyword.Add(
                            (long)reader[0], (short)reader[1], (string)reader[2], (string)reader[3]);

                        cnt++;
                        rowCnt++;

                        if (cnt < 100000)
                        {
                            continue;
                        }
                        else
                        {
                            ログ.ログ書き出し($"SiteMap row count. rowCnt:{rowCnt} cnt:{cnt}");

                            DB.WebCollectWork.BulkCopy_Keyword.Flush(AppSetting.ConnectionString_UnikktleWebCollectWork_WebServer);

                            cnt = 0;
                        }
                    }
                }

                if (DB.WebCollectWork.BulkCopy_Keyword.Data.Rows.Count > 0)
                {
                    ログ.ログ書き出し($"SiteMap row count. rowCnt:{rowCnt} cnt:{cnt}");

                    DB.WebCollectWork.BulkCopy_Keyword.Flush(AppSetting.ConnectionString_UnikktleWebCollectWork_WebServer);
                }

                ログ.ログ書き出し($"[Mst.Keyword] Web server UnikktleWebCollectWork.mst.tkeyword, Import after row count：{DB.WebCollectWork.SP_Keyword.GetCount(cnWebCollectWork_WebServer)} ");

                ログ.ログ書き出し("WebサーバのWorkデータベースを本データベースへ反映 開始");
                //DB.WebCollectWork.SP_Keyword.ExportToUnikktleWeb();
                DB.Msdb.SP_StartJob.Exec_ExportToUnikktleWeb_Keyword(cnMsdb);

                // 次回の開始日時を更新する
                AppSetting.AppData.BcpExportLastDate_MstKeyword = strNow;
                App.WriteAllAppDate();
            }
            finally
            {
                ログ.ログ書き出し("[Mst.Keyword] 終了");
            }
        }

        //// Exec_MstKeyword()メソッドのBCP版。トランザクションログが肥大化する為、今は使ってない。
        //private static void Exec_MstKeyword_BCP(string strNow, ref string log)
        //{
        //    try
        //    {
        //        ログ.ログ書き出し("[Mst.Keyword] 開始");

        //        ログ.ログ書き出し("エクスポート元の件数取得 開始");
        //        var cntCollect = DB.Collect.SP_Keyword.GetCount();
        //        ログ.ログ書き出し($"[Mst.Keyword] エクスポート元の件数　cntCollect：{cntCollect}");

        //        ログ.ログ書き出し($"前回のExportFileを削除 開始 : {AppSetting.BcpExportFilePath_mst_tKeyword}");
        //        File.Delete(AppSetting.BcpExportFilePath_mst_tKeyword);

        //        ログ.ログ書き出し("Collectサーバの計算結果をエクスポート 開始");
        //        BcpProcess.Exec(string.Format(AppSetting.BcpExportArgument_mst_tKeyword,
        //            strNow, AppSetting.AppData.BcpExportLastDate_MstKeyword, AppSetting.BcpExportFilePath_mst_tKeyword));

        //        if ((new FileInfo(AppSetting.BcpExportFilePath_mst_tKeyword)).Length < 1)
        //        {
        //            ログ.ログ書き出し("エクスポートしたファイルサイズが0バイト。何もしない");

        //            string sts = $"[Mst.Keyword] Warning Export file is 0 byte ：{AppSetting.BcpExportFilePath_mst_tKeyword}";
        //            ログ.ログ書き出し(sts);
        //            log += sts + "\r\n";
        //            return;
        //        }

        //        ログ.ログ書き出し($"WebサーバのWorkデータベースへインポートする前に古いデータを削除 開始");
        //        DB.WebCollectWork.SP_Keyword.Truncate();

        //        ログ.ログ書き出し("WebサーバのWorkデータベースへインポート 開始");
        //        BcpProcess.Exec(string.Format(AppSetting.BcpImportArgument_mst_tKeyword,
        //            AppSetting.BcpExportFilePath_mst_tKeyword));

        //        ログ.ログ書き出し($"[Mst.Keyword] Web server UnikktleWebCollectWork.mst.tkeyword, Import after row count：{DB.WebCollectWork.SP_Keyword.GetCount()} ");

        //        ログ.ログ書き出し("WebサーバのWorkデータベースを本データベースへ反映 開始");
        //        //DB.WebCollectWork.SP_Keyword.ExportToUnikktleWeb();
        //        DB.Msdb.SP_StartJob.Exec_ExportToUnikktleWeb_Keyword();

        //        //var cntWeb = DB.Web.SP_Keyword.GetCount();
        //        //ログ.ログ書き出し($"[Mst.Keyword] インポート後の件数　cntWeb：{cntWeb}");

        //        //if (cntCollect != cntWeb)
        //        //{
        //        //    // 
        //        //    ログ.ログ書き出し("インポート元と先で件数が不一致");

        //        //    log += "[Mst.Keyword] インポート完了後の件数が不一致 \r\n" +
        //        //           $"Collect server mst.tkeyword row count：{cntCollect} \r\n" +
        //        //           $"Web server mst.tkeyword row count：{cntWeb} \r\n";

        //        //    return false;
        //        //}

        //        //ログ.ログ書き出し("[Mst.Keyword] エクスポート元とインポート後の件数は一致");

        //        // 次回の開始日時を更新する
        //        AppSetting.AppData.BcpExportLastDate_MstKeyword = strNow;
        //        App.WriteAllAppDate();
        //    }
        //    finally
        //    {
        //        ログ.ログ書き出し("[Mst.Keyword] 終了");
        //    }
        //}
    }
}
