using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TestData
{
    public static class SP_Keyword
    {
        public static Clt_Keyword_Row SelectNo_FullColumn(SqlConnection cn, long no)
        {
            var cmdGetCount = new SqlCommand("clt.spKeyword_SelectNo_FullColumn", cn);
            cmdGetCount.CommandType = CommandType.StoredProcedure;
            cmdGetCount.Parameters.Add(new SqlParameter("No", SqlDbType.BigInt));
            cmdGetCount.Parameters["No"].Direction = ParameterDirection.Input;

            cmdGetCount.Parameters["No"].Value = no;

            using (var reader = cmdGetCount.ExecuteReader())
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

    }
}
