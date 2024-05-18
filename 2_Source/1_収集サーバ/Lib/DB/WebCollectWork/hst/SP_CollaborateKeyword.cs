using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using 定数;
using Logging;


namespace DB.WebCollectWork
{
    public static class SP_CollaborateKeyword
    {
        //private static SqlCommand cmdExportToUnikktleWeb;
        private static SqlCommand cmdTruncate;
        private static SqlCommand cmdGetCount;

        static SP_CollaborateKeyword()
        {
            //cmdExportToUnikktleWeb = new SqlCommand("hst.spCollaborateKeyword_ExportToUnikktleWeb", cn);
            //cmdExportToUnikktleWeb.CommandType = CommandType.StoredProcedure;
            //cmdExportToUnikktleWeb.CommandTimeout = DBConst.CommandTimeout_6h;

            cmdTruncate = new SqlCommand("hst.spCollaborateKeyword_Truncate");
            cmdTruncate.CommandType = CommandType.StoredProcedure;
            cmdTruncate.CommandTimeout = DBConst.CommandTimeout_1h;

            cmdGetCount = new SqlCommand("hst.spCollaborateKeyword_GetCount");
            cmdGetCount.CommandType = CommandType.StoredProcedure;
            cmdGetCount.CommandTimeout = DBConst.CommandTimeout_1h;
            cmdGetCount.Parameters.Add(new SqlParameter("Cnt", SqlDbType.BigInt));
            cmdGetCount.Parameters["Cnt"].Direction = ParameterDirection.Output;
        }

        //public static void ExportToUnikktleWeb()
        //{
        //    cmdExportToUnikktleWeb.ExecuteNonQuery();
        //}

        public static void Truncate(SqlConnection cn)
        {
            ログ.ログ書き出し("Truncate : " + cn.ConnectionString);

            cmdTruncate.Connection = cn;

            cmdTruncate.ExecuteNonQuery();
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
