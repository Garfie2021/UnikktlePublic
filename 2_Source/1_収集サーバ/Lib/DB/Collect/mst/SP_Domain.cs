using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using 定数;
using Logging;


namespace DB.Collect
{
    public static class SP_Domain
    {
        private static SqlCommand cmdGetWithInsert;

        static SP_Domain()
        {
            cmdGetWithInsert = new SqlCommand("mst.spDomain_GetWithInsert");
            cmdGetWithInsert.CommandType = CommandType.StoredProcedure;
            cmdGetWithInsert.CommandTimeout = DBConst.CommandTimeout_1h;
            cmdGetWithInsert.Parameters.Add(new SqlParameter("Domain", SqlDbType.NVarChar));
            cmdGetWithInsert.Parameters.Add(new SqlParameter("No", SqlDbType.BigInt));
            cmdGetWithInsert.Parameters["Domain"].Direction = ParameterDirection.Input;
            cmdGetWithInsert.Parameters["No"].Direction = ParameterDirection.Output;
        }

        public static long GetWithInsert(SqlConnection cn, string url)
        {
            try
            {
                cmdGetWithInsert.Connection = cn;
                cmdGetWithInsert.Parameters["Domain"].Value = url;

                cmdGetWithInsert.ExecuteNonQuery();

                return (long)cmdGetWithInsert.Parameters["No"].Value;
            }
            catch (Exception ex)
            {
                ログ.ログ書き出し(ex);
                ログ.ログ書き出し("Url: " + url);
                throw;
            }
        }

    }
}
