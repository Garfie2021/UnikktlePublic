using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace TestData
{
    public static class BulkCopy_Keyword
    {
        public static DataTable Data;

        static BulkCopy_Keyword()
        {
            Data = new DataTable("clt.tKeyword");
            Data.Columns.Add(new DataColumn("No", typeof(long)));
            Data.Columns.Add(new DataColumn("r_w", typeof(short)));
            Data.Columns.Add(new DataColumn("Word", typeof(string)));
            Data.Columns.Add(new DataColumn("FullTextSupple", typeof(string)));
        }

        // ※分かり易くする為に、１パラメータ１列を変えない。
        public static void Add(long no, short r_w, string word, string fullTextSupple)
        {
            Data.Rows.Add(no, r_w, word, fullTextSupple);
        }

        public static void Flush(string connectionString)
        {
            ログ.Add($"connectionString :{connectionString}");

            if (Data.Rows.Count < 1)
            {
                return;
            }

            using (SqlBulkCopy bulkcopy = new SqlBulkCopy(connectionString))
            {
                bulkcopy.DestinationTableName = "clt.tKeyword";
                bulkcopy.WriteToServer(Data);
            }

            Data.Clear();
        }
    }
}
