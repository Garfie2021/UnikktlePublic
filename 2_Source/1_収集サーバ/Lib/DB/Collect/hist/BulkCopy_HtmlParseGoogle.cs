using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 定数;
using AppDirectory;
using Logging;

namespace DB.Collect
{
    public static class BulkCopy_HtmlParseGoogle
    {
        public static DataTable Data;

        static BulkCopy_HtmlParseGoogle()
        {
            Data = new DataTable("hst.t2HtmlParseGoogle");
            Data.Columns.Add(new DataColumn("SearchKeywordNo", typeof(long)));
            Data.Columns.Add(new DataColumn("SearchDate", typeof(DateTime)));
            Data.Columns.Add(new DataColumn("State", typeof(byte)));
            Data.Columns.Add(new DataColumn("HtmlTag除外後1段階目", typeof(string)));
            Data.Columns.Add(new DataColumn("HtmlTag除外後2段階目", typeof(string)));
        }

        // ※分かり易くする為に、１パラメータ１列を変えない。
        public static void Add(
            long SearchKeywordNo,
            DateTime SearchDate,
            string HtmlTag除外後1段階目,
            string HtmlTag除外後2段階目)
        {
            Data.Rows.Add(
                SearchKeywordNo,
                SearchDate,
                State.未解析,
                HtmlTag除外後1段階目,
                HtmlTag除外後2段階目);
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
                bulkcopy.DestinationTableName = "hst.t2HtmlParseGoogle";
                bulkcopy.BulkCopyTimeout = DBConst.CommandTimeout_1h;
                bulkcopy.WriteToServer(Data);
            }

            Data.Clear();
        }
    }
}
