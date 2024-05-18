using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using 定数;
using Logging;


namespace DB.Collect
{
    public static class SP_TmpCollectTargetKeywordBing
    {
        private static SqlCommand cmdInsertIntoSelect_Bing_Tmp;

        static SP_TmpCollectTargetKeywordBing()
        {
            cmdInsertIntoSelect_Bing_Tmp = new SqlCommand("tmp.spTmpCollectTargetKeywordBing_InsertIntoSelect");
            cmdInsertIntoSelect_Bing_Tmp.CommandType = CommandType.StoredProcedure;
            cmdInsertIntoSelect_Bing_Tmp.CommandTimeout = DBConst.CommandTimeout_1h;
        }

        public static void InsertIntoSelect(SqlConnection cn)
        {
            cmdInsertIntoSelect_Bing_Tmp.Connection = cn;
            cmdInsertIntoSelect_Bing_Tmp.ExecuteNonQuery();
        }

    }
}


