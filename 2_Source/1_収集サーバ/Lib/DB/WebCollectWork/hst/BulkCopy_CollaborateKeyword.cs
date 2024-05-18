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
    public static class BulkCopy_CollaborateKeyword
    {
        public static DataTable Data;

        static BulkCopy_CollaborateKeyword()
        {
            Data = new DataTable("hst.tCollaborateKeyword");
            Data.Columns.Add(new DataColumn("KeywordNo_元", typeof(long)));
            Data.Columns.Add(new DataColumn("KeywordNo_先", typeof(long)));
            Data.Columns.Add(new DataColumn("同時出現ドキュメント数", typeof(long)));
        }

        // ※分かり易くする為に、１パラメータ１列を変えない。
        public static void Add(long KeywordNo_元, long KeywordNo_先, long 同時出現ドキュメント数)
        {
            Data.Rows.Add(KeywordNo_元, KeywordNo_先, 同時出現ドキュメント数);
        }

        public static void Flush(string connectionString)
        {
            //ログ.ログ書き出し("Flush() connectionString : " + connectionString);

            if (Data.Rows.Count < 1)
            {
                return;
            }

            using (SqlBulkCopy bulkcopy = new SqlBulkCopy(connectionString))
            {
                bulkcopy.DestinationTableName = "hst.tCollaborateKeyword";
                bulkcopy.BulkCopyTimeout = DBConst.CommandTimeout_1h;
                bulkcopy.WriteToServer(Data);
            }

            Data.Clear();
        }
    }
}
