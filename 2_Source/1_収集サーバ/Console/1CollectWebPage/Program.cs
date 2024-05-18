using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.IO;
using System.Net;
using System.Text;
using System.Timers;
using System.Threading;
using System.Threading.Tasks;

using 定数;
using Common;
using DB;
using Logging;
using AppDirectory;


namespace CollectWebPage
{
    public class Program
    {
        private static System.Timers.Timer aTimer;
        private static DateTime _Now = DateTime.Now;

        private static async Task Main(string[] args)
        {
            try
            {
                App.Initialize(args[0], typeof(Program).Namespace);

                // HtmlParseジョブ開始時刻までに必ず終わらせる。ジョブは21時開始-5時終了を想定。
                aTimer = new System.Timers.Timer(28800000); // 28800000=8時間。
                aTimer.Elapsed += OnTimedEvent;
                aTimer.AutoReset = false;
                aTimer.Enabled = true;

                using (var cnCmn = new SqlConnection())
                using (var cnCollect = new SqlConnection())
                {
                    cnCmn.ConnectionString = AppSetting.ConnectionString_UnikktleCmn;
                    cnCmn.Open();

                    cnCollect.ConnectionString = AppSetting.ConnectionString_UnikktleCollect;
                    cnCollect.Open();

                    // 未収集のURLを含むドメイン。
                    var domainNoList = DB.Collect.SP_Url.Select_DomainNo(cnCollect);

                    // ドメイン毎に並列でWebPageを収集。
                    var taskList = new List<Task>();
                    foreach (DataRow row in domainNoList.Rows)
                    {
                        taskList.Add((new WebPage()).CollectAsync((long)row["DomainNo"], _Now));
                    }

                    // 並列実行開始and終了待ち。
                    await Task.WhenAll(taskList);

                    DB.Cmn.SP_ExecHistory.ExecHistory_Insert(cnCmn, ジョブType._1_CollectWebPage, _Now, DateTime.Now);
                }
            }
            catch (Exception ex)
            {
                try
                {
                    Mail.SendMailAndLogErr("Exception発生 1namespace CollectGoogleSearch", ex);
                }
                catch (Exception ex2)
                {
                    File.WriteAllText(@"d:\CollectGoogleSearch.log", ex.Message);
                    File.WriteAllText(@"d:\CollectGoogleSearch.log", ex.StackTrace);

                    File.WriteAllText(@"d:\CollectGoogleSearch.log", ex2.Message);
                    File.WriteAllText(@"d:\CollectGoogleSearch.log", ex2.StackTrace);
                }
            }
                finally
            {
                ログ.ログ書き出し($"[END]");
            }

        }

        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            try
            {
                using (var cnCmn = new SqlConnection())
                {
                    cnCmn.ConnectionString = AppSetting.ConnectionString_UnikktleCmn;
                    cnCmn.Open();

                    DB.Cmn.SP_ExecHistory.ExecHistory_Insert(cnCmn, ジョブType._1_CollectWebPage, _Now, DateTime.Now);
                }

                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                try
                {
                    Mail.SendMailAndLogErr("Exception発生 1namespace CollectGoogleSearch", ex);
                }
                catch (Exception ex2)
                {
                    File.WriteAllText(@"d:\CollectGoogleSearch.log", ex.Message);
                    File.WriteAllText(@"d:\CollectGoogleSearch.log", ex.StackTrace);

                    File.WriteAllText(@"d:\CollectGoogleSearch.log", ex2.Message);
                    File.WriteAllText(@"d:\CollectGoogleSearch.log", ex2.StackTrace);
                }
            }
            finally
            {
                ログ.ログ書き出し($"[END] Time ouver");
            }
        }
    }
}
