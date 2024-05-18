using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.IO;
using System.Threading;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using 定数;
using DB;
using Logging;
using AppDirectory;
using Common;


namespace CollectEmailMEagazine
{
    public class Program
    {
        private static int Main(string[] args)
        {
            int Count = 0;
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

                    var collectTargetList = DB.Collect.SP_CollectTarget.Select(cn);

                    // メールマガジン収集
                    var 受信MailList = CollectEmail.Exec();
                    if (受信MailList.Count < 1)
                    {
                        DB.Cmn.SP_ExecHistory.ExecHistory_Insert(cnCmn, ジョブType._1_CollectEmailMEagazine, now, DateTime.Now);
                        return 0;
                    }

                    Count = 受信MailList.Count();

                    foreach (var mail in 受信MailList)
                    {
                        try
                        {
                            mail.CollectTargetNo = CollectEmail.CollectTargetNo(cn, collectTargetList, mail);

#if DEBUG
                            // ファイルに保存
                            CollectEmail.Write(mail);
#endif
                            mail.CurrentBody = HttpUtility.UrlDecode(HttpUtility.HtmlDecode(mail.CurrentBody));

                            // DB保存用にキャッシュ
                            DB.Collect.BulkCopy_CollectMail.Add(mail, now);
                        }
                        catch (Exception ex)
                        {
                            ログ.ログ書き出し(ex);
                        }
                    }

                    // DB保存用のキャッシュを一括保存
                    DB.Collect.BulkCopy_CollectMail.Flush(AppSetting.ConnectionString_UnikktleCollect);

                    DB.Cmn.SP_ExecHistory.ExecHistory_Insert(cnCmn, ジョブType._1_CollectEmailMEagazine, now, DateTime.Now);
                }

                return 0;
            }
            catch (Exception ex)
            {
                try
                {
                    Mail.SendMailAndLogErr("Exception発生 1CollectEmailMEagazine", ex);
                }
                catch (Exception ex2)
                {
                    File.WriteAllText(@"d:\CollectEmailMEagazine.log", ex.Message);
                    File.WriteAllText(@"d:\CollectEmailMEagazine.log", ex.StackTrace);

                    File.WriteAllText(@"d:\CollectEmailMEagazine.log", ex2.Message);
                    File.WriteAllText(@"d:\CollectEmailMEagazine.log", ex2.StackTrace);
                }

                return 1;
            }
            finally
            {
                ログ.ログ書き出し($"End Count:{Count}");
            }
        }

    }
}
