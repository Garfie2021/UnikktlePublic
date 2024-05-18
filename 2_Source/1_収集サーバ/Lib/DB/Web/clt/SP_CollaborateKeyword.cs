using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using 定数;
using Logging;


namespace DB.Web
{
    public static class SP_CollaborateKeyword
    {
        private static SqlCommand cmdGetCount;

        static SP_CollaborateKeyword()
        {
            cmdGetCount = new SqlCommand("clt.spCollaborateKeyword_GetCount");
            cmdGetCount.CommandType = CommandType.StoredProcedure;
            cmdGetCount.CommandTimeout = DBConst.CommandTimeout_1h;
            cmdGetCount.Parameters.Add(new SqlParameter("Cnt", SqlDbType.BigInt));
            cmdGetCount.Parameters["Cnt"].Direction = ParameterDirection.Output;
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
