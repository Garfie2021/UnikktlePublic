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
    public static class BulkCopy_HtmlParseWebPage
    {
        public static DataTable Data;

        static BulkCopy_HtmlParseWebPage()
        {
            Data = new DataTable("hst.t2HtmlParseWebPage");
            Data.Columns.Add(new DataColumn("DomainNo", typeof(long)));
            Data.Columns.Add(new DataColumn("UrlNo", typeof(long)));
            Data.Columns.Add(new DataColumn("State", typeof(byte)));
            Data.Columns.Add(new DataColumn("言語判定", typeof(byte)));
            Data.Columns.Add(new DataColumn("HtmlTag除外後1段階目", typeof(string)));
            Data.Columns.Add(new DataColumn("HtmlTag除外後2段階目", typeof(string)));
        }

        // ※分かり易くする為に、１パラメータ１列を変えない。
        public static void Add(
            long DomainNo,
            long UrlNo,
            言語判定結果? 言語判定結果,
            string HtmlTag除外後1段階目,
            string HtmlTag除外後2段階目)
        {
            Data.Rows.Add(
                DomainNo,
                UrlNo,
                State.未解析,
                言語判定結果,
                null,   //HtmlTag除外後1段階目,   // デバッグで必要な時のみ有効化する。
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
                bulkcopy.DestinationTableName = "hst.t2HtmlParseWebPage";
                bulkcopy.BulkCopyTimeout = DBConst.CommandTimeout_1h;
                bulkcopy.WriteToServer(Data);
            }

            Data.Clear();
        }
    }
}
