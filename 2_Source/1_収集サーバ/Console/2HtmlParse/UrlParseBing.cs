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
    public static class UrlParseBing
    {
        //private static int count = 0;

        public static void Exec(SqlConnection cn, DateTime now)
        {
            var dataTable = DB.Collect.SP_CollectBing.Select_CutoutStateUrl0(cn);

            foreach (DataRow row in dataTable.Rows)
            {
                UrlParse.Url切り出し((string)row["検索結果Html"], out List<string> urlList);

                foreach (var url in urlList)
                {
                    DB.Collect.SP_Url.Insert(cn, UrlParse.GetDomainNo(cn, url), url);
                }

                DB.Collect.SP_CollectBing.Update_CutoutStateUrl(cn,
                    (long)row["SearchKeywordNo"],
                    (DateTime)row["SearchDate"],
                    State.解析済み);
            }

            ログ.ログ書き出し($"Bing Url Cnt:{dataTable.Rows.Count}");
        }

    }
}
