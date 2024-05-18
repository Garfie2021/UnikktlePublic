using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.IO;
using System.Threading;
using System.Text;
using System.Threading.Tasks;

using 定数;
using Common;
using DB;
using Logging;
using AppDirectory;


namespace CollectBingSearch
{
    public class Program
    {
        // 日次で実行。
        // [mst].[tKeyword]から、[Bing検索日時]が古い順にTop1000ワードを取得し、
        // ワードをBing検索する。
        // 
        // 後で、メールと同様に重要語と関係性掲載を行う。
        private static void Main(string[] args)
        {
            int Count = 0;
            var html = "";
            var now = DateTime.Now;

            try
            {
                App.Initialize(args[0], typeof(Program).Namespace);

                using (var cnCmn = new SqlConnection())
                using (var cn = new SqlConnection())
                {
                    cnCmn.ConnectionString = AppSetting.ConnectionString_UnikktleCmn;
                    cnCmn.Open();

                    cn.ConnectionString = AppSetting.ConnectionString_UnikktleCollect;
                    cn.Open();

                    // [mst].[tKeyword]から、[Bing検索日時]が古い順にTop10ワードを取得。
                    var list = DB.Collect.SP_Keyword.Select_Bing検索(cn);
                    Count = list.Count();

                    foreach (var keyword in list)
                    {
                        byte retryCnt = 0;
                        do
                        {
                            try
                            {
                                // ワードをBing検索する。
                                html = BingSearch.Search(keyword);

                                break;
                            }
                            catch (Exception ex)
                            {
                                ログ.ログ書き出し(ex);

                                if (retryCnt == 5)
                                {
                                    throw;
                                }

                                // Bingの復帰を待つ。5分×５回。
                                Thread.Sleep(300000);
                            }
                        }
                        while (retryCnt++ > 5);

                        DB.Collect.BulkCopy_CollectBing.Add(keyword.No, now, html);

                        DB.Collect.BulkCopy_CollectBing.Flush(AppSetting.ConnectionString_UnikktleCollect);

                        DB.Collect.SP_Keyword.Update_Bing検索日時(cn, keyword.No, now);

                        // Bingに禁止されないように30秒空ける。
                        Thread.Sleep(30000);
                    }

                    DB.Cmn.SP_ExecHistory.ExecHistory_Insert(cnCmn, ジョブType._1_CollectBingSearch, now, DateTime.Now);
                }
            }
            catch (Exception ex)
            {
                try
                {
                    Mail.SendMailAndLogErr("Exception発生 1CollectBingSearch", ex);
                }
                catch (Exception ex2)
                {
                    File.WriteAllText(@"d:\CollectBingSearch.log", ex.Message);
                    File.WriteAllText(@"d:\CollectBingSearch.log", ex.StackTrace);

                    File.WriteAllText(@"d:\CollectBingSearch.log", ex2.Message);
                    File.WriteAllText(@"d:\CollectBingSearch.log", ex2.StackTrace);
                }
            }
            finally
            {
                ログ.ログ書き出し($"End Count:{Count}");
            }
        }
    }
}
