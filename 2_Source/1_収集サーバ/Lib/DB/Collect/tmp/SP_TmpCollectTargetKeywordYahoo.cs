using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using 定数;
using Logging;


namespace DB.Collect
{
    public static class SP_TmpCollectTargetKeywordYahoo
    {
        private static SqlCommand cmdInsertIntoSelect_Yahoo_Tmp;

        static SP_TmpCollectTargetKeywordYahoo()
        {
            cmdInsertIntoSelect_Yahoo_Tmp = new SqlCommand("tmp.spTmpCollectTargetKeywordYahoo_InsertIntoSelect");
            cmdInsertIntoSelect_Yahoo_Tmp.CommandType = CommandType.StoredProcedure;
            cmdInsertIntoSelect_Yahoo_Tmp.CommandTimeout = DBConst.CommandTimeout_1h;
        }

        public static void InsertIntoSelect(SqlConnection cn)
        {
            cmdInsertIntoSelect_Yahoo_Tmp.Connection = cn;
            cmdInsertIntoSelect_Yahoo_Tmp.ExecuteNonQuery();
        }

    }
}


