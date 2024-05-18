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

namespace DB.WebCollectWork
{
    public static class BulkCopy_Keyword
    {
        public static DataTable Data;

        static BulkCopy_Keyword()
        {
            Data = new DataTable("mst.tKeyword");
            Data.Columns.Add(new DataColumn("No", typeof(long)));
            Data.Columns.Add(new DataColumn("r_w", typeof(short)));
            Data.Columns.Add(new DataColumn("Word", typeof(string)));
            Data.Columns.Add(new DataColumn("解析元データ", typeof(string)));
        }

        // ※分かり易くする為に、１パラメータ１列を変えない。
        public static void Add(long no, short r_w, string word, string 解析元データ)
        {
            Data.Rows.Add(no, r_w, word, 解析元データ);
        }

        public static void Flush(string connectionString)
        {
            ログ.ログ書き出し("BulkCopy_Keyword connectionString : " + connectionString);

            if (Data.Rows.Count < 1)
            {
                return;
            }

            using (SqlBulkCopy bulkcopy = new SqlBulkCopy(connectionString))
            {
                bulkcopy.DestinationTableName = "mst.tKeyword";
                bulkcopy.BulkCopyTimeout = DBConst.CommandTimeout_1h;
                bulkcopy.WriteToServer(Data);
            }

            Data.Clear();
        }
    }
}
