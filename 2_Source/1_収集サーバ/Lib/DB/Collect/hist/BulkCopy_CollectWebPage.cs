using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using 定数;
using AppDirectory;
using Logging;

namespace DB.Collect
{
    public static class BulkCopy_CollectWebPage
    {
        public static DataTable Data;

        static BulkCopy_CollectWebPage()
        {
            Data = new DataTable("hst.t1CollectWebPage");
            Data.Columns.Add(new DataColumn("UrlNo", typeof(long)));
            Data.Columns.Add(new DataColumn("Html", typeof(string)));
        }

        // ※分かり易くする為に、１パラメータ１列を変えない。
        public static void Add(
            long SearchKeywordNo, 
            DateTime SearchDate, 
            string 検索結果Html)
        {
            Data.Rows.Add(
                SearchKeywordNo, 
                SearchDate, 
                State.未解析, 
                検索結果Html);
        }

        public static void Flush(string connectionString)
        {
            //var connectionString = AppSetting.ConnectionString_UnikktleCollect;
            //ログ.ログ書き出し("Flush() connectionString : " + connectionString);

            if (Data.Rows.Count < 1)
            {
                return;
            }

            using (SqlBulkCopy bulkcopy = new SqlBulkCopy(connectionString))
            {
                bulkcopy.DestinationTableName = "hst.t1CollectBing";
                bulkcopy.BulkCopyTimeout = DBConst.CommandTimeout_1h;
                bulkcopy.WriteToServer(Data);
            }

            Data.Clear();
        }
    }
}
