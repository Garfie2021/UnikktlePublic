using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using 定数;
using Logging;


namespace DB.Web
{
    public static class SP_Mind
    {
        private static SqlCommand cmdSelect_AllNo;

        static SP_Mind()
        {
            cmdSelect_AllNo = new SqlCommand("mst.spMind_Select_AllNo");
            cmdSelect_AllNo.CommandType = CommandType.StoredProcedure;
            cmdSelect_AllNo.CommandTimeout = DBConst.CommandTimeout_1h;
        }

        public static SqlDataReader Select_AllNo(SqlConnection cn)
        {
            cmdSelect_AllNo.Connection = cn;

            return cmdSelect_AllNo.ExecuteReader();
        }
    }
}
