using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using 定数;
using Logging;


namespace DB.Collect
{
    public static class SP_ExtractMail
    {
        private static SqlCommand cmdSelect_MeCabState0;
        private static SqlCommand cmdUpdate_MeCabState;

        static SP_ExtractMail()
        {
            cmdSelect_MeCabState0 = new SqlCommand("hst.sp3ExtractMail_Select_MeCabState0");
            cmdSelect_MeCabState0.CommandType = CommandType.StoredProcedure;
            //cmd.CommandTimeout = DB定数.CommandTimeout;
            cmdSelect_MeCabState0.CommandTimeout = DBConst.CommandTimeout_1h;

            cmdUpdate_MeCabState = new SqlCommand("hst.sp3ExtractMail_Update_MeCabState");
            cmdUpdate_MeCabState.CommandType = CommandType.StoredProcedure;
            cmdUpdate_MeCabState.CommandTimeout = DBConst.CommandTimeout_1h;
            cmdUpdate_MeCabState.Parameters.Add(new SqlParameter("CollectTargetNo", SqlDbType.BigInt));
            cmdUpdate_MeCabState.Parameters.Add(new SqlParameter("SendDate", SqlDbType.DateTime));
            cmdUpdate_MeCabState.Parameters.Add(new SqlParameter("登録日時", SqlDbType.DateTime));
            cmdUpdate_MeCabState.Parameters.Add(new SqlParameter("MeCabState", SqlDbType.TinyInt));
            cmdUpdate_MeCabState.Parameters.Add(new SqlParameter("MeCab名詞", SqlDbType.NVarChar));
            cmdUpdate_MeCabState.Parameters["CollectTargetNo"].Direction = ParameterDirection.Input;
            cmdUpdate_MeCabState.Parameters["SendDate"].Direction = ParameterDirection.Input;
            cmdUpdate_MeCabState.Parameters["登録日時"].Direction = ParameterDirection.Input;
            cmdUpdate_MeCabState.Parameters["MeCabState"].Direction = ParameterDirection.Input;
            cmdUpdate_MeCabState.Parameters["MeCab名詞"].Direction = ParameterDirection.Input;
        }

        public static List<ExtractMailRow> Select_MeCabState0(SqlConnection cn)
        {
            cmdSelect_MeCabState0.Connection = cn;

            var table = new DataTable();
            using (var reader = cmdSelect_MeCabState0.ExecuteReader())
            {
                table.Load(reader);
                reader.Close();
            }

            // DataTableをListに入れ替え
            var list = new List<ExtractMailRow>();
            foreach (DataRow row in table.Rows)
            {
                list.Add(new ExtractMailRow()
                {
                    CollectTargetNo = (long)row[0],
                    SendDate = (DateTime)row[1],
                    登録日時 = (DateTime)row[2],
                    英語連結名詞除外後 = (string)row[3],
                });
            }

            return list;
        }

        public static void Update_MeCabState(SqlConnection cn,
            ExtractMailRow ExtractMailRow, State MeCabState, string MeCab名詞)
        {
            cmdUpdate_MeCabState.Connection = cn;
            cmdUpdate_MeCabState.Parameters["CollectTargetNo"].Value = ExtractMailRow.CollectTargetNo;
            cmdUpdate_MeCabState.Parameters["SendDate"].Value = ExtractMailRow.SendDate;
            cmdUpdate_MeCabState.Parameters["登録日時"].Value = ExtractMailRow.登録日時;
            cmdUpdate_MeCabState.Parameters["MeCabState"].Value = MeCabState;
            cmdUpdate_MeCabState.Parameters["MeCab名詞"].Value = MeCab名詞;

            cmdUpdate_MeCabState.ExecuteNonQuery();
        }
    }
}
