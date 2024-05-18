using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using 定数;
using Logging;


namespace DB.Collect
{
    public static class SP_TmpCollectTargetKeywordMail
    {
        private static SqlCommand cmdInsertIntoSelect_Mail_Tmp;

        static SP_TmpCollectTargetKeywordMail()
        {
            cmdInsertIntoSelect_Mail_Tmp = new SqlCommand("tmp.spTmpCollectTargetKeywordMail_InsertIntoSelect");
            cmdInsertIntoSelect_Mail_Tmp.CommandType = CommandType.StoredProcedure;
            cmdInsertIntoSelect_Mail_Tmp.CommandTimeout = DBConst.CommandTimeout_1h;
        }

        public static void InsertIntoSelect(SqlConnection cn)
        {
            cmdInsertIntoSelect_Mail_Tmp.Connection = cn;
            cmdInsertIntoSelect_Mail_Tmp.ExecuteNonQuery();
        }

    }
}


