using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 定数;

namespace DB.Cmn
{
    public static class SP_ExecHistory
    {
        private static SqlCommand cmdInsert;

        static SP_ExecHistory()
        {
            cmdInsert = new SqlCommand("job.spExecHistory_Insert");
            cmdInsert.CommandType = CommandType.StoredProcedure;
            cmdInsert.CommandTimeout = DBConst.CommandTimeout_1h;
            cmdInsert.Parameters.Add(new SqlParameter("Type", SqlDbType.TinyInt));
            cmdInsert.Parameters.Add(new SqlParameter("StartDate", SqlDbType.DateTime));
            cmdInsert.Parameters.Add(new SqlParameter("EndDate", SqlDbType.DateTime));
            cmdInsert.Parameters["Type"].Direction = ParameterDirection.Input;
            cmdInsert.Parameters["StartDate"].Direction = ParameterDirection.Input;
            cmdInsert.Parameters["EndDate"].Direction = ParameterDirection.Input;
        }


        public static void ExecHistory_Insert(SqlConnection cn, ジョブType ジョブType, DateTime StartDate, DateTime EndDate)
        {
            cmdInsert.Connection = cn;
            cmdInsert.Parameters["Type"].Value = ジョブType;
            cmdInsert.Parameters["StartDate"].Value = StartDate;
            cmdInsert.Parameters["EndDate"].Value = EndDate;

            cmdInsert.ExecuteNonQuery();
        }
    }
}
