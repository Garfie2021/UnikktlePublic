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

namespace _2HtmlParse
{
    public class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                var now = DateTime.Now;

                App.Initialize(args[0], typeof(Program).Namespace);

                using (var cnCmn = new SqlConnection())
                using (var cn = new SqlConnection())
                {
                    cnCmn.ConnectionString = AppSetting.ConnectionString_UnikktleCmn;
                    cnCmn.Open();

                    cn.ConnectionString = AppSetting.ConnectionString_UnikktleCollect;
                    cn.Open();

                    // 収集に使うUrlを切り出し。
                    UrlParseBing.Exec(cn, now);
                    UrlParseYahoo.Exec(cn, now);
                    UrlParseGoogle.Exec(cn, now);

                    // キーワード解析に使うデータを切り出し。
                    HtmlParseBing.Exec(cn, now);
                    HtmlParseYahoo.Exec(cn, now);
                    HtmlParseGoogle.Exec(cn, now);

                    // t1Collect系のテーブルデータは半年程度は残す。 
                    DB.Collect.SP_CollectBing.Truncate(cn);
                    DB.Collect.SP_CollectGoogle.Truncate(cn);
                    DB.Collect.SP_CollectYahoo.Truncate(cn);

                    DB.Cmn.SP_ExecHistory.ExecHistory_Insert(cnCmn, ジョブType._2_HtmlParse, now, DateTime.Now);
                }   
            }
            catch (Exception ex)
            {
                try
                {
                    Mail.SendMailAndLogErr("Exception発生 2HtmlParse", ex);
                }
                catch (Exception ex2)
                {
                    File.WriteAllText(@"d:\2HtmlParse.log", ex.Message);
                    File.WriteAllText(@"d:\2HtmlParse.log", ex.StackTrace);

                    File.WriteAllText(@"d:\2HtmlParse.log", ex2.Message);
                    File.WriteAllText(@"d:\2HtmlParse.log", ex2.StackTrace);
                }
            }
            finally
            {
                ログ.ログ書き出し("End");
            }
        }

    }
}
