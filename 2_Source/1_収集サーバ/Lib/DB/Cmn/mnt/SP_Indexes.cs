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

    public static class SP_Indexes
    {
        private static SqlCommand cmdSelect;

        static SP_Indexes()
        {
            cmdSelect = new SqlCommand("mnt.spIndexes_Select");
            cmdSelect.CommandType = CommandType.StoredProcedure;
            cmdSelect.CommandTimeout = DBConst.CommandTimeout_1h;
        }

        public static List<Indexes> Select(SqlConnection cn)
        {
            cmdSelect.Connection = cn;

            var table = new DataTable();
            using (var reader = cmdSelect.ExecuteReader())
            {
                table.Load(reader);
                reader.Close();
            }

            // DataTableをListに入れ替え
            var list = new List<Indexes>();
            foreach (DataRow row in table.Rows)
            {
                list.Add(new Indexes(){
                    DatabaseName = (string)row[0],
                    SchemaName = (string)row[1],
                    TableName = (string)row[2],
                    IndedxName = (string)row[3],
                    Fragmentation = (double)row[4]
                });
            }

            return list;
        }
    }

    public class Indexes
    {
        public string DatabaseName;
        public string SchemaName;
        public string TableName;
        public string IndedxName;
        public double Fragmentation;
    }
}
