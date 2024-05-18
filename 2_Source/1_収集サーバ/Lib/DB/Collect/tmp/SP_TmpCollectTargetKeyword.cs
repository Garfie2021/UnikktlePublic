using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using 定数;
using Logging;


namespace DB.Collect
{
    public static class SP_TmpCollectTargetKeyword
    {
        private static SqlCommand cmdReCreateTemptable;

        static SP_TmpCollectTargetKeyword()
        {
            cmdReCreateTemptable = new SqlCommand("tmp.spTmpCollectTargetKeyword_ReCreateTemptable");
            cmdReCreateTemptable.CommandType = CommandType.StoredProcedure;
            cmdReCreateTemptable.CommandTimeout = DBConst.CommandTimeout_1h;
        }

        public static void ReCreateTemptable(SqlConnection cn)
        {
            cmdReCreateTemptable.Connection = cn;
            cmdReCreateTemptable.ExecuteNonQuery();
        }

    }
}


