using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using 定数;
using DB;
using Logging;
using AppDirectory;

namespace CollectWebPage
{
    public class WebPage
    {
        private static object _dbWrite = new object();


        // 並列処理でHTMLを取得。
        public async Task CollectAsync(long domainNo, DateTime now)
        {
            try
            {
                //ログ.ログ書き出し("Thread開始　ID:" + Thread.CurrentThread.ManagedThreadId + " DomainNo:" + domainNo);

                using (var webClient = new WebClient())
                using (var cn = new SqlConnection())
                using (var data = new DataTable("hst.t1CollectWebPage"))
                {
                    cn.ConnectionString = AppSetting.ConnectionString_UnikktleCollect;
                    cn.Open();

                    // カラムの並び順を変えたら、BulkCopyの並びも合わせないと、書き込みに失敗するので要注意
                    data.Columns.Add(new DataColumn("DomainNo", typeof(long)));
                    data.Columns.Add(new DataColumn("UrlNo", typeof(long)));
                    data.Columns.Add(new DataColumn("CollectState", typeof(byte)));
                    //data.Columns.Add(new DataColumn("Language判定1段階目", typeof(byte)));
                    data.Columns.Add(new DataColumn("State", typeof(byte)));
                    data.Columns.Add(new DataColumn("CutoutStateUrl", typeof(byte)));
                    //data.Columns.Add(new DataColumn("HtmlParseResult", typeof(byte)));
                    data.Columns.Add(new DataColumn("Html", typeof(string)));


                    var urlData = DB.Collect.SP_Url.Select_No_Url(cn, domainNo);

                    ログ.ログ書き出し(@"Collect start. domainNo:" + domainNo + "  Url Count:" + urlData.Rows.Count);

                    var resultList = new List<UrlRow>();

                    foreach (DataRow row in urlData.Rows)
                    {
                        var urlNo = (long)row["UrlNo"];
                        var url = (string)row["URL"];
                        var collectState = CollectState.収集失敗;
                        var html = "";

                        // Htmlの取得に失敗しても構わず処理を続行。
                        try
                        {
                            using (var st = webClient.OpenRead(url))
                            using (var sr = new StreamReader(st, Encoding.UTF8))
                            {
                                html = sr.ReadToEnd();
                            }

                            collectState = CollectState.収集成功;
                        }
                        catch (Exception ex)
                        {
                            ログ.ログ書き出し(ex);
                            ログ.ログ書き出し("Collect fail. DomainNo:" + domainNo + " UrlNo:" + urlNo + " URL:" + url);

                            collectState = CollectState.収集失敗;
                        }

                        if (row["CollectDate"] == DBNull.Value)
                        {
                            // 初回
                            data.Rows.Add(domainNo, urlNo, collectState, null, null, html);
                        }
                        else
                        {
                            // 2回目以降
                            DB.Collect.SP_CollectWebPage.Update_Html(cn, domainNo, urlNo, collectState, html);
                        }

                        resultList.Add(new UrlRow()
                        {
                            DomainNo = domainNo,
                            UrlNo = urlNo,
                            CollectState = collectState
                        });

                        ログ.ログ書き出し("Collect success. DomainNo:" + domainNo + " UrlNo:" + urlNo + " URL:" + url);

                        // 1つのドメインに負荷をかけないように間を空ける。
                        await Task.Delay(10000);
                    }

                    if (data.Rows.Count > 0)
                    {
                        // 初回収集分のWebPageをDBに登録。
                        using (SqlBulkCopy bulkcopy = new SqlBulkCopy(cn))
                        {
                            bulkcopy.DestinationTableName = "hst.t1CollectWebPage";
                            bulkcopy.BulkCopyTimeout = DBConst.CommandTimeout_1h;
                            bulkcopy.WriteToServer(data);
                        }
                    }

                    // 収集結果をDBに反映。収集日時は成功失敗に関係なく更新する。
                    foreach (var row in resultList)
                    {
                        DB.Collect.SP_Url.UpdateCollectDate(cn, now, row);
                    }

                    ログ.ログ書き出し(@"Collect end. domainNo:" + domainNo);
                }
            }
            catch (Exception ex)
            {
                ログ.ログ書き出し(@"Collect timeout end. domainNo:" + domainNo);
                ログ.ログ書き出し(ex);
                throw;
            }
        }

    }
}
