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

namespace _2HtmlParseWebPage
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

                    long cntUrl = 0;
                    long cntHtml = 0;
                    long limit = 1000;
                    var urlIsMore = false;
                    var htmlIsMore = false;
                    while (true)
                    {
                        // 処理に時間がかかるので、URL切出しとHTML切出しを交互に行う。

                        // 収集に使うUrlを切り出し。
                        urlIsMore = UrlParseWebPage.Exec(cn, now, ref cntUrl);

                        // キーワード解析に使うデータを切り出し。
                        htmlIsMore = HtmlParseWebPage.Exec(cn, now, ref cntHtml);

                        // URLとHTMLのどちらも解析済みの行はHTMLをnullにする。
                        DB.Collect.SP_CollectWebPage.Update_Html_Null(cn);

                        if (cntUrl >= limit || cntHtml >= limit)
                        {
                            ログ.ログ書き出し($"HtmlParseWebPage exec. Cnt URL:{cntUrl} Cnt HTML:{cntHtml}");
                            limit = limit * 10;
                        }

                        if (urlIsMore == false && htmlIsMore == false)
                        {
                            ログ.ログ書き出し($"HtmlParseWebPage exec. Cnt URL:{cntUrl} Cnt HTML:{cntHtml}");
                            break;
                        }
                    }

                    DB.Cmn.SP_ExecHistory.ExecHistory_Insert(cnCmn, ジョブType._2_HtmlParseWebPage, now, DateTime.Now);

                    ログ.ログ書き出し($"HtmlParseWebPage exec end.");
                }
            }
            catch (Exception ex)
            {
                try
                {
                    Mail.SendMailAndLogErr("Exception発生 2HtmlParseWebPage", ex);
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
