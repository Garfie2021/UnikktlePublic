using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TestData
{
    public static class SQL_Keyword
    {
        public static void Truncate(SqlConnection cn)
        {
            const string sql = "TRUNCATE TABLE [clt].[tKeyword];";

            ログ.Add($"Exec SQL :{sql}");

            var cmd = new SqlCommand(sql, cn);
            //cmd.CommandType = CommandType.StoredProcedure;

            cmd.ExecuteNonQuery();
        }

        public static Clt_Keyword_Row GetKeywordRow(SqlConnection cn, long no)
        {
            const string sql = "SELECT [No],[r_w],[Word],[FullTextSupple] FROM [clt].[tKeyword] WHERE [No]={0} ; ";

            var sql2 = string.Format(sql, no);
            ログ.Add($"Exec SQL :{sql2}");

            var cmd = new SqlCommand(sql2, cn);
            //cmd.CommandType = CommandType.StoredProcedure;

            using (var reader = SQL_CollaborateKeyword.Select(cn))
            {
                while (reader.Read() == true)
                {
                    return new Clt_Keyword_Row()
                    {
                        No = (long)reader[0],
                        r_w = (short)reader[1],
                        Word = (string)reader[2],
                        FullTextSupple = (string)reader[3],
                    };
                }
            }

            return null;
        }

        public static void FulltextCatalog_Rebuild(SqlConnection cn)
        {
            const string sql = "ALTER FULLTEXT CATALOG [Word] REBUILD;";

            ログ.Add($"Exec SQL :{sql}");

            var cmd = new SqlCommand(sql, cn);
            //cmd.CommandType = CommandType.StoredProcedure;

            cmd.ExecuteNonQuery();
        }

    }
}
