using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using 定数;
using Logging;


namespace DB.WebCollectWork
{
    public static class SP_CollaborateKeywordCount_CollectServer
    {
        //private static SqlCommand cmdExportToUnikktleWeb;
        private static SqlCommand cmdSelect30RowOver;
        private static SqlCommand cmdSelect30RowOver_Word;
        private static SqlCommand cmdGetCount;
        private static SqlCommand cmdTruncate;
        private static SqlCommand cmdInsert;

        static SP_CollaborateKeywordCount_CollectServer()
        {
            cmdSelect30RowOver = new SqlCommand("hst.spCollaborateKeywordCount_CollectServer_Select30RowOver");
            cmdSelect30RowOver.CommandType = CommandType.StoredProcedure;
            cmdSelect30RowOver.CommandTimeout = DBConst.CommandTimeout_6h;

            cmdSelect30RowOver_Word = new SqlCommand("hst.spCollaborateKeywordCount_CollectServer_Select30RowOver_Word");
            cmdSelect30RowOver_Word.CommandType = CommandType.StoredProcedure;
            cmdSelect30RowOver_Word.CommandTimeout = DBConst.CommandTimeout_6h;

            cmdTruncate = new SqlCommand("hst.spCollaborateKeywordCount_CollectServer_Truncate");
            cmdTruncate.CommandType = CommandType.StoredProcedure;
            cmdTruncate.CommandTimeout = DBConst.CommandTimeout_1h;

            cmdInsert = new SqlCommand("hst.spCollaborateKeywordCount_CollectServer_Insert");
            cmdInsert.CommandType = CommandType.StoredProcedure;
            cmdInsert.CommandTimeout = DBConst.CommandTimeout_6h;
            cmdInsert.CommandTimeout = DBConst.CommandTimeout_1h;

            cmdGetCount = new SqlCommand("hst.spCollaborateKeywordCount_CollectServer_GetCount");
            cmdGetCount.CommandType = CommandType.StoredProcedure;
            cmdGetCount.CommandTimeout = DBConst.CommandTimeout_1h;
            cmdGetCount.Parameters.Add(new SqlParameter("Cnt", SqlDbType.BigInt));
            cmdGetCount.Parameters["Cnt"].Direction = ParameterDirection.Output;
        }

        public static SqlDataReader Select30RowOver(SqlConnection cn)
        {
            cmdSelect30RowOver.Connection = cn;

            return cmdSelect30RowOver.ExecuteReader();
        }

        public static SqlDataReader Select30RowOver_Word(SqlConnection cn)
        {
            cmdSelect30RowOver_Word.Connection = cn;

            return cmdSelect30RowOver_Word.ExecuteReader();
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
