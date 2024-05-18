using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using 定数;
using Logging;


namespace DB.Collect
{
    public static class SP_TmpCollectTargetKeywordGoogle
    {
        private static SqlCommand cmdInsertIntoSelect_Google_Tmp;

        static SP_TmpCollectTargetKeywordGoogle()
        {
            cmdInsertIntoSelect_Google_Tmp = new SqlCommand("tmp.spTmpCollectTargetKeywordGoogle_InsertIntoSelect");
            cmdInsertIntoSelect_Google_Tmp.CommandType = CommandType.StoredProcedure;
            cmdInsertIntoSelect_Google_Tmp.CommandTimeout = DBConst.CommandTimeout_1h;
        }

        public static void InsertIntoSelect(SqlConnection cn)
        {
            cmdInsertIntoSelect_Google_Tmp.Connection = cn;
            cmdInsertIntoSelect_Google_Tmp.ExecuteNonQuery();
        }

    }
}


