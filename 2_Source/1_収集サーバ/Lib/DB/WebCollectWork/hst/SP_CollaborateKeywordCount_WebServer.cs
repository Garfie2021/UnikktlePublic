using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using 定数;
using Logging;


namespace DB.WebCollectWork
{
    public static class SP_CollaborateKeywordCount_WebServer
    {
        //private static SqlCommand cmdExportToUnikktleWeb;
        private static SqlCommand cmdGetCount;
        private static SqlCommand cmdTruncate;
        private static SqlCommand cmdInsert;

        static SP_CollaborateKeywordCount_WebServer()
        {
            cmdTruncate = new SqlCommand("hst.spCollaborateKeywordCount_WebServer_Truncate");
            cmdTruncate.CommandType = CommandType.StoredProcedure;
            cmdTruncate.CommandTimeout = DBConst.CommandTimeout_1h;

            cmdInsert = new SqlCommand("hst.spCollaborateKeywordCount_WebServer_Insert");
            cmdInsert.CommandType = CommandType.StoredProcedure;
            cmdInsert.CommandTimeout = DBConst.CommandTimeout_6h;

            cmdGetCount = new SqlCommand("hst.spCollaborateKeywordCount_WebServer_GetCount");
            cmdGetCount.CommandType = CommandType.StoredProcedure;
            cmdGetCount.CommandTimeout = DBConst.CommandTimeout_1h;
            cmdGetCount.Parameters.Add(new SqlParameter("Cnt", SqlDbType.BigInt));
            cmdGetCount.Parameters["Cnt"].Direction = ParameterDirection.Output;
        }

        public static void Truncate(SqlConnection cn)
        {
            ログ.ログ書き出し("Truncate : " + cn.ConnectionString);

            cmdTruncate.Connection = cn;

            cmdTruncate.ExecuteNonQuery();
        }

        public static void Insert(SqlConnection cn)
        {
            ログ.ログ書き出し("Insert : " + cn.ConnectionString);

            cmdInsert.Connection = cn;

            cmdInsert.ExecuteNonQuery();
        }

        public static long GetCount(SqlConnection cn)
        {
            ログ.ログ書き出し("GetCount : " + cn.ConnectionString);

            cmdGetCount.Connection = cn;

            cmdGetCount.ExecuteNonQuery();

            return (long)cmdGetCount.Parameters["Cnt"].Value;
        }

    }
}

