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
    public static class BulkCopy_SearchWord
    {
        public static DataTable Data;

        static BulkCopy_SearchWord()
        {
            Data = new DataTable("wlt.tSearchWord");
            Data.Columns.Add(new DataColumn("No", typeof(long)));
            Data.Columns.Add(new DataColumn("Word", typeof(string)));
        }

        // ※分かり易くする為に、１パラメータ１列を変えない。
        public static void Add(long no, string word)
        {
            Data.Rows.Add(no, word);
        }

        public static void Flush(string connectionString)
        {
            ログ.ログ書き出し("BulkCopy_SearchWord connectionString : " + connectionString);

            if (Data.Rows.Count < 1)
            {
                return;
            }

            using (SqlBulkCopy bulkcopy = new SqlBulkCopy(connectionString))
            {
                bulkcopy.DestinationTableName = "wlt.tSearchWord";
                bulkcopy.BulkCopyTimeout = DBConst.CommandTimeout_1h;
                bulkcopy.WriteToServer(Data);
            }

            Data.Clear();
        }
    }
}

