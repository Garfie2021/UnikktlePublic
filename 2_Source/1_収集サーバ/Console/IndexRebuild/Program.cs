using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using 定数;
using DB;
using Logging;
using AppDirectory;
using Common;

namespace IndexRebuild
{
    public class Program
    {
        private const string ConnectionString_localhost_UnikktleCmn = "Data Source=localhost;Initial Catalog=UnikktleCmn;User ID=xxx;Password=xxx";
        private const string ConnectionString_localhost = "Data Source=localhost;Initial Catalog={0};User ID=xxx;Password=xxx";

        private const string ConnectionString_192_168_11_5_UnikktleCmn = "Data Source=xxx;Initial Catalog=UnikktleCmn;User ID=xxx;Password=xxx";
        private const string ConnectionString_192_168_11_5 = "Data Source=xxx;Initial Catalog={0};User ID=xxx;Password=xxx";

        private const string ConnectionString_160_16_75_102_UnikktleCmn = "Data Source=160.16.75.102,60002;Initial Catalog=UnikktleCmn;User ID=xxx;Password=xxx";
        private const string ConnectionString_160_16_75_102 = "Data Source=160.16.75.102,60002;Initial Catalog={0};User ID=xxx;Password=xxx";

        private static void Main(string[] args)
        {
            var now = DateTime.Now;

            try
            {
                App.Initialize(args[0], typeof(Program).Namespace);

                ログ.ログ書き出し($"IndexRebuild Start");

                //using (var cn = new SqlConnection(ConnectionString_localhost_UnikktleCmn))
                //{
                //    cn.Open();

                //    Worker.Exec_IndexRebuild(cn, ConnectionString_localhost);
                //}

                using (var cn = new SqlConnection(ConnectionString_160_16_75_102_UnikktleCmn))
                {
                    cn.Open();

                    Worker.Exec_IndexRebuild(cn, ConnectionString_160_16_75_102);
                }

                using (var cn = new SqlConnection(ConnectionString_192_168_11_5_UnikktleCmn))
                {
                    cn.Open();

                    Worker.Exec_IndexRebuild(cn, ConnectionString_192_168_11_5);
                }



            }
            catch (Exception ex)
            {
                try
                {
                    Mail.SendMailAndLogErr("Exception発生 IndexRebuild", ex);
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

                ログ.ログ書き出し($"IndexRebuild end");
            }
        }
    }
}
