using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;


namespace TestData
{
    public static class SP_CollaborateKeyword
    {
        public static SqlDataReader SelectDistinct_KeywordNo(SqlConnection cn)
        {
            var cmd = new SqlCommand("clt.spCollaborateKeyword_SelectDistinct_KeywordNo", cn);
            cmd.CommandType = CommandType.StoredProcedure;

            return cmd.ExecuteReader();
        }

    }
}
