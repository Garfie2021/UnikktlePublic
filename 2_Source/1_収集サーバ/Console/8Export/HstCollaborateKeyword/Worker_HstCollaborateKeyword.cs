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
using _8Export.Common;


namespace _8Export.HstCollaborateKeyword
{
    public static class Worker_HstCollaborateKeyword
    {
        public static bool Exec_FromWebServer_ToCollectServer()
        {
            ログ.ログ書き出し("WebServer上の前処理 開始");
            using (var cnWebCollectWork_WebServer = new SqlConnection())
            {
                cnWebCollectWork_WebServer.ConnectionString = AppSetting.ConnectionString_UnikktleWebCollectWork_WebServer;
                cnWebCollectWork_WebServer.Open();

                Exec_HstCollaborateKeywordCount_OnWebServer(cnWebCollectWork_WebServer);
            }

            ログ.ログ書き出し("WebServerの処理結果をCollectServerへ 開始");
            using (var cnWebCollectWork_CollectServer = new SqlConnection())
            {
                cnWebCollectWork_CollectServer.ConnectionString = AppSetting.ConnectionString_UnikktleWebCollectWork_CollectServer;
                cnWebCollectWork_CollectServer.Open();

                Exec_HstCollaborateKeywordCount_OnCollectServer(cnWebCollectWork_CollectServer);
            }

            //if (cntWebServer != cntCollectServer)
            //{
            //    ログ.ログ書き出し("インポート元と先で件数が不一致");

            //    Mail.SendMailAndLogErr("_8Export Error",
            //        "hst.tCollaborateKeywordCount インポート完了後の件数が不一致 \r\n" +
            //        $"Collect server row count：{cntCollectServer} \r\n" +
            //        $"Web server row count：{cntWebServer} \r\n");

            //    return false;
            //}

            //ログ.ログ書き出し("インポート元と先で件数が一致");

            return true;
        }

        public static void Exec_FromCollectServer_ToWebServer(DateTime now)
        {
            var log = "";
            using (var cnCollect = new SqlConnection())
            using (var cnWebCollectWork_WebServer = new SqlConnection())
            using (var cnWebCollectWork_CollectServer = new SqlConnection())
            using (var cnMsdb = new SqlConnection())
            {
                cnCollect.ConnectionString = AppSetting.ConnectionString_UnikktleCollect;
                cnCollect.Open();

                cnWebCollectWork_WebServer.ConnectionString = AppSetting.ConnectionString_UnikktleWebCollectWork_WebServer;
                cnWebCollectWork_WebServer.Open();

                cnWebCollectWork_CollectServer.ConnectionString = AppSetting.ConnectionString_UnikktleWebCollectWork_CollectServer;
                cnWebCollectWork_CollectServer.Open();

                cnMsdb.ConnectionString = AppSetting.ConnectionString_Msdb;
                cnMsdb.Open();

                //SP_ReCreateTempTable.ReCreateTemptable_CollectTargetKeyword();

                Exec_HstCollaborateKeyword_OnCollect(cnWebCollectWork_CollectServer, cnCollect, cnMsdb, cnWebCollectWork_WebServer, ref log);
            }

            //using (var cnCollect = new SqlConnection())
            //using (var cnWeb = new SqlConnection())
            //using (var cnWebCollectWork_WebServer = new SqlConnection())
            //using (var cnMsdb = new SqlConnection())
            //{
            //    UnikktleCollectDBConnection.Initialize(cnCollect, AppSetting.ConnectionString_UnikktleCollect);
            //    UnikktleWebDBConnection.Initialize(cnWeb, AppSetting.ConnectionString_UnikktleWeb);
            //    UnikktleWebCollectWorkDBConnection.Initialize(cnWebCollectWork_WebServer, AppSetting.ConnectionString_UnikktleWebCollectWork_WebServer);
            //    MsdbDBConnection.Initialize(cnMsdb, AppSetting.ConnectionString_Msdb);

            //    Exec_HstCollaborateKeyword_OnWeb(ref log);
            //}

        }


        private static void Exec_HstCollaborateKeywordCount_OnWebServer(SqlConnection cn)
        {
            ログ.ログ書き出し("Exec_HstCollaborateKeyword_WebServer 開始");

            ログ.ログ書き出し("SP_CollaborateKeywordCount_WebServer.Truncate 開始");
            DB.WebCollectWork.SP_CollaborateKeywordCount_WebServer.Truncate(cn);

            ログ.ログ書き出し("SP_CollaborateKeywordCount.Insert 開始");
            DB.WebCollectWork.SP_CollaborateKeywordCount_WebServer.Insert(cn);

            ログ.ログ書き出し("エクスポート元の件数取得 開始");
            var cnt = DB.WebCollectWork.SP_CollaborateKeywordCount_WebServer.GetCount(cn);
            ログ.ログ書き出し($"Hst.CollaborateKeywordCount エクスポート元の件数　WebServer：{cnt}");

            ログ.ログ書き出し($"前回のカウント結果ExportFileを削除 開始 : {AppSetting.BcpExportFilePath_hst_tCollaborateKeywordCount_WebServer}");
            File.Delete(AppSetting.BcpExportFilePath_hst_tCollaborateKeywordCount_WebServer);

            ログ.ログ書き出し("Webサーバのカウント結果をエクスポート 開始");
            BcpProcess.Exec(string.Format(AppSetting.BcpExportArgument_hst_tCollaborateKeywordCount_WebServer,
                AppSetting.BcpExportFilePath_hst_tCollaborateKeywordCount_WebServer));

            ログ.ログ書き出し("Exec_HstCollaborateKeyword_WebServer 終了");
        }


        // Collectサーバで稼働している、UnikktleWebCollectWorkデータベースの hst.tCollaborateKeywordCount_WebServer テーブルに、
        // Webサーバからエクスポートしたデータをインポートする。
        private static void Exec_HstCollaborateKeywordCount_OnCollectServer(SqlConnection cn)
        {
            ログ.ログ書き出し("Exec_HstCollaborateKeyword_CollectServer 開始");

            ログ.ログ書き出し("SP_CollaborateKeywordCount_WebServer.Truncate 開始");
            DB.WebCollectWork.SP_CollaborateKeywordCount_WebServer.Truncate(cn);

            ログ.ログ書き出し("CollectサーバにエクスポートしたWebサーバのカウント結果をインポート 開始");
            BcpProcess.Exec(string.Format(AppSetting.BcpImportArgument_hst_tCollaborateKeywordCount_WebServer,
                AppSetting.BcpExportFilePath_hst_tCollaborateKeywordCount_WebServer));

            ログ.ログ書き出し("インポート後の件数取得 開始");
            var cnt = DB.WebCollectWork.SP_CollaborateKeywordCount_WebServer.GetCount(cn);
            ログ.ログ書き出し($"Hst.CollaborateKeywordCount インポート後の件数　CollectServer：{cnt}");

            ログ.ログ書き出し("Exec_HstCollaborateKeyword_CollectServer 終了");
        }


        private static void Exec_HstCollaborateKeyword_OnCollect(
            SqlConnection cnWebCollectWork_CollectServer, SqlConnection cnCollect, 
            SqlConnection cnMsdb, SqlConnection cnWebCollectWork_WebServer,
            ref string log)
        {
            try
            {
                //ログ.ログ書き出し("エクスポート元の件数取得 開始");
                //var cntCollect = DB.Collect.SP_CollaborateKeyword.GetCount();
                //ログ.ログ書き出し($"Hst.CollaborateKeyword エクスポート元の件数　cntCollect：{cntCollect}");

                ログ.ログ書き出し("CollectサーバーのCollaborateKeywordCountテーブル更新 開始");
                ログ.ログ書き出し("SP_CollaborateKeywordCount_CollectServer.Truncate 開始");
                DB.WebCollectWork.SP_CollaborateKeywordCount_CollectServer.Truncate(cnWebCollectWork_CollectServer);
                ログ.ログ書き出し("SP_CollaborateKeywordCount_CollectServer.Insert 開始");
                DB.WebCollectWork.SP_CollaborateKeywordCount_CollectServer.Insert(cnWebCollectWork_CollectServer);

                ログ.ログ書き出し("WebサーバのWorkデータベースへインポートする前に古いデータを削除 開始");
                DB.WebCollectWork.SP_CollaborateKeyword.Truncate(cnWebCollectWork_WebServer);

                ログ.ログ書き出し("Collectサーバの計算結果をエクスポート 開始");


                int cnt = 0;
                long rowCnt = 0;
                using (var reader = DB.Collect.SP_CollaborateKeyword.Select_WebServerCollectServerDiff(cnCollect))
                {
                    while (reader.Read() == true)
                    {
                        DB.WebCollectWork.BulkCopy_CollaborateKeyword.Add(
                            (long)reader[0], (long)reader[1], (long)reader[2]);

                        cnt++;
                        rowCnt++;

                        if (cnt < 100000)
                        {
                            continue;
                        }
                        else
                        {
                            ログ.ログ書き出し($"SiteMap row count. rowCnt:{rowCnt} cnt:{cnt}");

                            DB.WebCollectWork.BulkCopy_CollaborateKeyword.Flush(AppSetting.ConnectionString_UnikktleWebCollectWork_WebServer);

                            cnt = 0;
                        }
                    }
                }

                if (DB.WebCollectWork.BulkCopy_CollaborateKeyword.Data.Rows.Count > 0)
                {
                    ログ.ログ書き出し($"SiteMap row count. rowCnt:{rowCnt} cnt:{cnt}");

                    DB.WebCollectWork.BulkCopy_CollaborateKeyword.Flush(AppSetting.ConnectionString_UnikktleWebCollectWork_WebServer);
                }

                ログ.ログ書き出し($"WebサーバのWorkデータベースを本データベースへ反映 開始");

                DB.Msdb.SP_StartJob.Exec_ExportToUnikktleWeb_CollaborateKeyword(cnMsdb);
            }
            finally
            {
                ログ.ログ書き出し("Hst.CollaborateKeyword 終了");
            }
        }


        //// Exec_HstCollaborateKeyword_OnCollect()メソッドのBCP版。トランザクションログが肥大化する為、今は使ってない。
        //private static void Exec_HstCollaborateKeyword_BCP_OnCollect(ref string log)
        //{
        //    try
        //    {
        //        //ログ.ログ書き出し("エクスポート元の件数取得 開始");
        //        //var cntCollect = DB.Collect.SP_CollaborateKeyword.GetCount();
        //        //ログ.ログ書き出し($"Hst.CollaborateKeyword エクスポート元の件数　cntCollect：{cntCollect}");

        //        ログ.ログ書き出し("SP_CollaborateKeywordCount_CollectServer.Truncate 開始");
        //        DB.WebCollectWork.SP_CollaborateKeywordCount_CollectServer.Truncate();

        //        ログ.ログ書き出し("SP_CollaborateKeywordCount_CollectServer.Insert 開始");
        //        DB.WebCollectWork.SP_CollaborateKeywordCount_CollectServer.Insert();

        //        ログ.ログ書き出し($"前回のExportFileを削除 開始 : {AppSetting.BcpExportFilePath_hst_t6CollaborateKeyword}");
        //        File.Delete(AppSetting.BcpExportFilePath_hst_t6CollaborateKeyword);

        //        ログ.ログ書き出し("Collectサーバの計算結果をエクスポート 開始");
        //        BcpProcess.Exec(string.Format(AppSetting.BcpExportArgument_hst_t6CollaborateKeyword,
        //            AppSetting.BcpExportFilePath_hst_t6CollaborateKeyword));

        //        if ((new FileInfo(AppSetting.BcpExportFilePath_hst_t6CollaborateKeyword)).Length < 1)
        //        {
        //            ログ.ログ書き出し("エクスポートしたファイルサイズが0バイト。何もしない");

        //            string sts = $"Hst.CollaborateKeyword Warning Export file is 0 byte ：{AppSetting.BcpExportFilePath_hst_t6CollaborateKeyword}";
        //            ログ.ログ書き出し(sts);
        //            log += sts + "\r\n";
        //            return;
        //        }

        //    }
        //    finally
        //    {
        //        ログ.ログ書き出し("Hst.CollaborateKeyword 終了");
        //    }
        //}

        //private static void Exec_HstCollaborateKeyword_OnWeb(ref string log)
        //{
        //    try
        //    {
        //        ログ.ログ書き出し("Hst.CollaborateKeyword 開始");

        //        ログ.ログ書き出し("WebサーバのWorkデータベースへインポートする前に古いデータを削除 開始");
        //        DB.WebCollectWork.SP_CollaborateKeyword.Truncate();

        //        ログ.ログ書き出し("WebサーバのWorkデータベースへインポート 開始");
        //        BcpProcess.Exec(string.Format(AppSetting.BcpImportArgument_hst_t6CollaborateKeyword,
        //            AppSetting.BcpExportFilePath_hst_t6CollaborateKeyword));

        //        ログ.ログ書き出し($"Hst.CollaborateKeyword Web server UnikktleWebCollectWork.hst.tCollaborateKeyword, Import after row count：{DB.WebCollectWork.SP_CollaborateKeyword.GetCount()} ");

        //        ログ.ログ書き出し($"WebサーバのWorkデータベースを本データベースへ反映 開始");
        //        //DB.WebCollectWork.SP_CollaborateKeyword.ExportToUnikktleWeb();
        //        DB.Msdb.SP_StartJob.Exec_ExportToUnikktleWeb_CollaborateKeyword();

        //        //var cntWeb = DB.Web.SP_CollaborateKeyword.GetCount();
        //        //ログ.ログ書き出し($"Hst.CollaborateKeyword インポート後の件数　cntWeb：{cntWeb}");

        //        //if (cntCollect != cntWeb)
        //        //{
        //        //    ログ.ログ書き出し($"インポート元と先で件数が不一致");
        //        //    log += "Hst.CollaborateKeyword インポート完了後の件数が不一致 \r\n" + 
        //        //           $"Collect server hst.tCollaborateKeyword row count：{cntCollect} \r\n" +
        //        //           $"Web server hst.tCollaborateKeyword row count：{cntWeb} \r\n";

        //        //    return false;
        //        //}

        //        //ログ.ログ書き出し("Hst.CollaborateKeyword エクスポート元とインポート後の件数は一致");

        //    }
        //    finally
        //    {
        //        ログ.ログ書き出し("Hst.CollaborateKeyword 終了");
        //    }
        //}

    }
}
