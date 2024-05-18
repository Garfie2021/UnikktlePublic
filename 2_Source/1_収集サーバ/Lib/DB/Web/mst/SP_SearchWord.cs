using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using 定数;
using Logging;


namespace DB.Web
{
    public static class SP_SearchWord
    {
        private static SqlCommand cmdSelect;

        static SP_SearchWord()
        {
            cmdSelect = new SqlCommand("mst.spSearchWord_Select");
            cmdSelect.CommandType = CommandType.StoredProcedure;
            cmdSelect.CommandTimeout = DBConst.CommandTimeout_1h;
            cmdSelect.Parameters.Add(new SqlParameter("No", SqlDbType.BigInt));
            cmdSelect.Parameters["No"].Direction = ParameterDirection.Input;
        }

        public static SqlDataReader Select(SqlConnection cn, long no)
        {
            cmdSelect.Connection = cn;
            cmdSelect.Parameters["No"].Value = no;

            return cmdSelect.ExecuteReader();
        }
    }
}
