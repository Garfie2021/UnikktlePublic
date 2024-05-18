using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using 定数;


namespace DB.Collect
{
    public static class SP_CollaborateKeyword
    {
        private static SqlCommand cmdSelect_WebServerCollectServerDiff;
        private static SqlCommand cmdGetCount;
        private static SqlCommand cmdInsert;

        static SP_CollaborateKeyword()
        {
            cmdSelect_WebServerCollectServerDiff = new SqlCommand("hst.sp6CollaborateKeyword_Select_WebServerCollectServerDiff");
            cmdSelect_WebServerCollectServerDiff.CommandType = CommandType.StoredProcedure;
            cmdSelect_WebServerCollectServerDiff.CommandTimeout = DBConst.CommandTimeout_6h;

            cmdGetCount = new SqlCommand("hst.sp6CollaborateKeyword_GetCount");
            cmdGetCount.CommandType = CommandType.StoredProcedure;
            cmdGetCount.CommandTimeout = DBConst.CommandTimeout_1h;
            cmdGetCount.Parameters.Add(new SqlParameter("Cnt", SqlDbType.BigInt));
            cmdGetCount.Parameters["Cnt"].Direction = ParameterDirection.Output;

            cmdInsert = new SqlCommand("hst.sp6CollaborateKeyword_Insert");
            cmdInsert.CommandType = CommandType.StoredProcedure;
            cmdInsert.CommandTimeout = DBConst.CommandTimeout_1h;
        }

        public static SqlDataReader Select_WebServerCollectServerDiff(SqlConnection cn)
        {
            cmdSelect_WebServerCollectServerDiff.Connection = cn;

            return cmdSelect_WebServerCollectServerDiff.ExecuteReader();
        }

        public static long GetCount(SqlConnection cn)
        {
            cmdGetCount.Connection = cn;

            cmdGetCount.ExecuteNonQuery();

            return (long)cmdGetCount.Parameters["Cnt"].Value;
        }

        public static void Insert(SqlConnection cn)
        {
            cmdInsert.Connection = cn;

            cmdInsert.ExecuteNonQuery();
        }

    }
}
