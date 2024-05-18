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
    public static class SP_Databases
    {
        private static SqlCommand cmdSelect;

        static SP_Databases()
        {
            cmdSelect = new SqlCommand("mnt.spDatabases_Select");
            cmdSelect.CommandType = CommandType.StoredProcedure;
            cmdSelect.CommandTimeout = DBConst.CommandTimeout_1h;
        }

        public static List<string> Select(SqlConnection cn)
        {
            cmdSelect.Connection = cn;

            var table = new DataTable();
            using (var reader = cmdSelect.ExecuteReader())
            {
                table.Load(reader);
                reader.Close();
            }

            // DataTableをListに入れ替え
            var list = new List<string>();
            foreach (DataRow row in table.Rows)
            {
                list.Add((string)row[0]);
            }

            return list;
        }
    }
}
