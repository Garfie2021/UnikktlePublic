using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using 定数;


namespace DB.Collect
{
    public static class SP_CollectTarget
    {
        private static SqlCommand cmdSelect;
        private static SqlCommand cmdInsert;

        static SP_CollectTarget()
        {
            cmdSelect = new SqlCommand("mst.spCollectTargetMail_Select");
            cmdSelect.CommandType = CommandType.StoredProcedure;
            //cmd.CommandTimeout = DB定数.CommandTimeout;
            cmdSelect.CommandTimeout = DBConst.CommandTimeout_1h;

            cmdInsert = new SqlCommand("mst.spCollectTargetMail_Insert");
            cmdInsert.CommandType = CommandType.StoredProcedure;
            cmdInsert.CommandTimeout = DBConst.CommandTimeout_1h;
            cmdInsert.Parameters.Add(new SqlParameter("名称", SqlDbType.NVarChar));
            cmdInsert.Parameters.Add(new SqlParameter("From_MailAddress", SqlDbType.NVarChar));
            cmdInsert.Parameters.Add(new SqlParameter("No", SqlDbType.BigInt));
            cmdInsert.Parameters["名称"].Direction = ParameterDirection.Input;
            cmdInsert.Parameters["From_MailAddress"].Direction = ParameterDirection.Input;
            cmdInsert.Parameters["No"].Direction = ParameterDirection.Output;
        }

        public static long Insert(SqlConnection cn,
            CollectTargetRow collectTarget)
        {
            cmdInsert.Connection = cn;
            cmdInsert.Parameters["名称"].Value = collectTarget.名称;
            cmdInsert.Parameters["From_MailAddress"].Value = collectTarget.From_MailAddress;

            cmdInsert.ExecuteNonQuery();

            return (long)cmdInsert.Parameters["No"].Value;
        }

        public static List<CollectTargetRow> Select(SqlConnection cn)
        {
            cmdSelect.Connection = cn;

            var table = new DataTable();
            using (var reader = cmdSelect.ExecuteReader())
            {
                table.Load(reader);
                reader.Close();
            }

            // DataTableをListに入れ替え
            var list = new List<CollectTargetRow>();
            foreach (DataRow row in table.Rows)
            {
                list.Add(new CollectTargetRow(){
                    No = (long)row[0],
                    名称 = (string)row[1],
                    From_MailAddress = (string)row[2],
                });
            }

            return list;
        }
    }
}

