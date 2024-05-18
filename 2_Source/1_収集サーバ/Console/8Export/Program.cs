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
using _8Export.SiteMap;
using _8Export.MstKeyword;
using _8Export.HstCollaborateKeyword;
using _8Export.WltSearchWord;


namespace _8Export
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var now = DateTime.Now;

            try
            {
                App.Initialize(args[0], typeof(Program).Namespace);

                ログ.ログ書き出し($"8Export Start");
                //ログ.ログ書き出し($"AppSetting.BcpExportFilePath_hst_t6CollaborateKeyword： {AppSetting.BcpExportFilePath_hst_t6CollaborateKeyword}");
                //ログ.ログ書き出し($"AppSetting.BcpExportFilePath_mst_tKeyword： {AppSetting.BcpExportFilePath_mst_tKeyword}");

                ログ.ログ書き出し($"Initialize 開始");
                Ssh.Initialize(AppSetting.SSH_HostName, int.Parse(AppSetting.SSH_Port),
                    AppSetting.SSH_UserName, AppSetting.SSH_KeyFile, AppSetting.SSH_PassPhrase);

                ログ.ログ書き出し($"Worker_WltSearchWord.Exec() 開始");
                Worker_WltSearchWord.Exec();


                ログ.ログ書き出し($"CollectサーバのデータをWebサーバへ MstKeyword 開始");
                Worker_MstKeyword.Exec_FromCollectServer_ToWebServer_MstKeyword(now);


                ログ.ログ書き出し($"WebサーバのデータをCollectサーバへ 開始");
                if (Worker_HstCollaborateKeyword.Exec_FromWebServer_ToCollectServer() == false)
                {
                    ログ.ログ書き出し($"Worker.Exec_FromWebServer_ToCollectServer() 異常発生。中断。");
                    return;
                }

                ログ.ログ書き出し($"CollectサーバのデータをWebサーバへ 開始する。");
                Worker_HstCollaborateKeyword.Exec_FromCollectServer_ToWebServer(now);


                ログ.ログ書き出し($"CollectサーバのデータをWebサーバへ 開始する。");
                Worker_SiteMap.Exec_SiteMap();

            }
            catch (Exception ex)
            {
                try
                {
                    Mail.SendMailAndLogErr("Exception発生 8Export", ex);
                }
                catch (Exception ex2)
                {
                    File.WriteAllText(@"d:\Export.log", ex.Message);
                    File.WriteAllText(@"d:\Export.log", ex.StackTrace);
                    File.WriteAllText(@"d:\Export.log", ex2.Message);
                    File.WriteAllText(@"d:\Export.log", ex2.StackTrace);
                }
            }
            finally
            {
                using (var cnCmn = new SqlConnection())
                {
                    cnCmn.ConnectionString = AppSetting.ConnectionString_UnikktleCmn;
                    cnCmn.Open();

                    DB.Cmn.SP_ExecHistory.ExecHistory_Insert(cnCmn, ジョブType._3_ExtractEnglishConcatNoun, now, DateTime.Now);
                }

                ログ.ログ書き出し($"8Export end");
            }
        }

    }
}
