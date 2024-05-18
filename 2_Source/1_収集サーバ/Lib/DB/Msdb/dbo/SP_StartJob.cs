using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using 定数;
using Logging;


namespace DB.Msdb
{
    public static class SP_StartJob
    {
        private static SqlCommand cmdExec_ExportToUnikktleWeb_Keyword;
        private static SqlCommand cmdExec_ExportToUnikktleWeb_CollaborateKeyword;

        static SP_StartJob()
        {
            cmdExec_ExportToUnikktleWeb_Keyword = new SqlCommand("msdb.dbo.sp_start_job");
            cmdExec_ExportToUnikktleWeb_Keyword.CommandType = CommandType.StoredProcedure;
            cmdExec_ExportToUnikktleWeb_Keyword.CommandTimeout = DBConst.CommandTimeout_1h;
            cmdExec_ExportToUnikktleWeb_Keyword.Parameters.AddWithValue("@job_name", "Exec_ExportToUnikktleWeb_Keyword");

            cmdExec_ExportToUnikktleWeb_CollaborateKeyword = new SqlCommand("msdb.dbo.sp_start_job");
            cmdExec_ExportToUnikktleWeb_CollaborateKeyword.CommandType = CommandType.StoredProcedure;
            cmdExec_ExportToUnikktleWeb_CollaborateKeyword.CommandTimeout = DBConst.CommandTimeout_1h;
            cmdExec_ExportToUnikktleWeb_CollaborateKeyword.Parameters.AddWithValue("@job_name", "Exec_ExportToUnikktleWeb_CollaborateKeyword");
        }

        public static void Exec_ExportToUnikktleWeb_Keyword(SqlConnection cn)
        {
            cmdExec_ExportToUnikktleWeb_Keyword.Connection = cn;

            cmdExec_ExportToUnikktleWeb_Keyword.ExecuteNonQuery();
        }

        public static void Exec_ExportToUnikktleWeb_CollaborateKeyword(SqlConnection cn)
        {
            cmdExec_ExportToUnikktleWeb_CollaborateKeyword.Connection = cn;

            cmdExec_ExportToUnikktleWeb_CollaborateKeyword.ExecuteNonQuery();
        }
    }
}
