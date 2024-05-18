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


namespace CollectYahooSearch
{
    public class Program
    {
        // 日次で実行。
        // [mst].[tKeyword]から、[Yahoo検索日時]が古い順にTop1000ワードを取得し、
        // ワードをYahoo検索する。
        // 
        // 後で、メールと同様に重要語と関係性掲載を行う。
        private static void Main(string[] args)
        {
            int Count = 0;
            string html;
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

                    // [mst].[tKeyword]から、[Yahoo検索日時]が古い順にTop10ワードを取得。
                    var list = DB.Collect.SP_Keyword.Select_Yahoo検索(cn);
                    Count = list.Count();

                    foreach (var keyword in list)
                    {
                        // ワードをYahoo検索する。
                        html = YahooSearch.Search(keyword);

                        DB.Collect.BulkCopy_CollectYahoo.Add(keyword.No, now, html);

                        DB.Collect.BulkCopy_CollectYahoo.Flush(AppSetting.ConnectionString_UnikktleCollect);

                        DB.Collect.SP_Keyword.Update_Yahoo検索日時(cn, keyword.No, now);

                        // Yahooに禁止されないように間を空ける。
                        Thread.Sleep(30000);
                    }

                    DB.Cmn.SP_ExecHistory.ExecHistory_Insert(cnCmn, ジョブType._1_CollectYahooSearch, now, DateTime.Now);
                }
            }
            catch (Exception ex)
            {
                try
                {
                    if (ex.Message.IndexOf("リモート サーバーがエラーを返しました") > -1 &&
                        ex.Message.IndexOf("999") > -1)
                    {
                        // Yahooは「リモート サーバーがエラーを返しました: (999) Unable to process request at this time -- error 999」エラーが頻発する。
                        // このエラーはログのみ記録する。
                        ログ.ログ書き出し(ex);
                    }
                    else
                    {
                        Mail.SendMailAndLogErr("Exception発生 1CollectYahooSearch", ex);
                    }
                }
                catch (Exception ex2)
                {
                    File.WriteAllText(@"d:\CollectYahooSearch.log", ex.Message);
                    File.WriteAllText(@"d:\CollectYahooSearch.log", ex.StackTrace);

                    File.WriteAllText(@"d:\CollectYahooSearch.log", ex2.Message);
                    File.WriteAllText(@"d:\CollectYahooSearch.log", ex2.StackTrace);
                }
            }
            finally
            {
                ログ.ログ書き出し($"End Count:{Count}");
            }
        }

    }
}
