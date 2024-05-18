using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using 定数;
using Logging;


namespace DB.Collect
{
    public static class SP_SearchWord
    {
        private static SqlCommand cmdGetMaxNo;

        static SP_SearchWord()
        {
            cmdGetMaxNo = new SqlCommand("wlt.spSearchWord_GetMaxNo");
            cmdGetMaxNo.CommandType = CommandType.StoredProcedure;
            cmdGetMaxNo.CommandTimeout = DBConst.CommandTimeout_1h;
            cmdGetMaxNo.Parameters.Add(new SqlParameter("No", SqlDbType.BigInt));
            cmdGetMaxNo.Parameters["No"].Direction = ParameterDirection.Output;
        }

        public static long GetMaxNo(SqlConnection cn)
        {
            cmdGetMaxNo.Connection = cn;
            cmdGetMaxNo.ExecuteNonQuery();

            if (cmdGetMaxNo.Parameters["No"].Value == DBNull.Value)
            {
                return -1;
            }

            return (long)cmdGetMaxNo.Parameters["No"].Value;
        }

    }
}
