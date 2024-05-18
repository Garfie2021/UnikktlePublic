using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using 定数;
using Logging;


namespace DB.Collect
{
    public static class SP_TmpCollectTargetKeywordWebPage
    {
        private static SqlCommand cmdInsertIntoSelect_WebPage_Tmp;

        static SP_TmpCollectTargetKeywordWebPage()
        {
            cmdInsertIntoSelect_WebPage_Tmp = new SqlCommand("tmp.spTmpCollectTargetKeywordWebPage_InsertIntoSelect");
            cmdInsertIntoSelect_WebPage_Tmp.CommandType = CommandType.StoredProcedure;
            cmdInsertIntoSelect_WebPage_Tmp.CommandTimeout = DBConst.CommandTimeout_1h;
        }

        public static void InsertIntoSelect(SqlConnection cn)
        {
            cmdInsertIntoSelect_WebPage_Tmp.Connection = cn;
            cmdInsertIntoSelect_WebPage_Tmp.ExecuteNonQuery();
        }

    }
}
