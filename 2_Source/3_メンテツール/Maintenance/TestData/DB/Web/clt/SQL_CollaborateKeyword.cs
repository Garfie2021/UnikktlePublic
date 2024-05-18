using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TestData
{
    public static class SQL_CollaborateKeyword
    {
        public static void Truncate(SqlConnection cn)
        {
            const string sql = "TRUNCATE TABLE [clt].[tCollaborateKeyword];";

            ログ.Add($"Exec SQL :{sql}");

            var cmd = new SqlCommand(sql, cn);
            //cmd.CommandType = CommandType.StoredProcedure;

            cmd.ExecuteNonQuery();
        }

        public static SqlDataReader Select(SqlConnection cn)
        {
            const string sql = "SELECT DISTINCT [KeywordNo_先] FROM [clt].[tCollaborateKeyword]; ";

            ログ.Add($"Exec SQL :{sql}");

            var cmd = new SqlCommand(sql, cn);
            //cmd.CommandType = CommandType.StoredProcedure;

            return cmd.ExecuteReader();
        }
    }
}
