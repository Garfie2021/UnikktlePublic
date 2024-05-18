using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using 定数;
using Logging;


namespace DB.Collect
{
    public static class SP_CollectMail
    {
        private static SqlCommand cmdSelect_State0;
        private static SqlCommand cmdUpdate_State;
        
        static SP_CollectMail()
        {
            cmdSelect_State0 = new SqlCommand("hst.sp1CollectMail_Select_State0");
            cmdSelect_State0.CommandType = CommandType.StoredProcedure;
            cmdSelect_State0.CommandTimeout = DBConst.CommandTimeout_1h;

            cmdUpdate_State = new SqlCommand("hst.sp1CollectMail_Update_State");
            cmdUpdate_State.CommandType = CommandType.StoredProcedure;
            cmdUpdate_State.CommandTimeout = DBConst.CommandTimeout_1h;
            cmdUpdate_State.Parameters.Add(new SqlParameter("CollectTargetNo", SqlDbType.BigInt));
            cmdUpdate_State.Parameters.Add(new SqlParameter("SendDate", SqlDbType.DateTime));
            cmdUpdate_State.Parameters.Add(new SqlParameter("登録日時", SqlDbType.DateTime));
            cmdUpdate_State.Parameters.Add(new SqlParameter("State", SqlDbType.TinyInt));
            //cmdUpdate_State.Parameters.Add(new SqlParameter("言語判定", SqlDbType.TinyInt));
            //cmdUpdate_State.Parameters.Add(new SqlParameter("不要文字列除外後", SqlDbType.NVarChar));
            //cmdUpdate_State.Parameters.Add(new SqlParameter("英語連結名詞", SqlDbType.NVarChar));
            //cmdUpdate_State.Parameters.Add(new SqlParameter("英語連結名詞除外後", SqlDbType.NVarChar));
            cmdUpdate_State.Parameters["CollectTargetNo"].Direction = ParameterDirection.Input;
            cmdUpdate_State.Parameters["SendDate"].Direction = ParameterDirection.Input;
            cmdUpdate_State.Parameters["登録日時"].Direction = ParameterDirection.Input;
            cmdUpdate_State.Parameters["State"].Direction = ParameterDirection.Input;
            //cmdUpdate_State.Parameters["言語判定"].Direction = ParameterDirection.Input;
            //cmdUpdate_State.Parameters["不要文字列除外後"].Direction = ParameterDirection.Input;
            //cmdUpdate_State.Parameters["英語連結名詞"].Direction = ParameterDirection.Input;
            //cmdUpdate_State.Parameters["英語連結名詞除外後"].Direction = ParameterDirection.Input;

        }

        public static List<CollectMailRow> Select_State0(SqlConnection cn)
        {
            cmdSelect_State0.Connection = cn;

            var table = new DataTable();
            using (var reader = cmdSelect_State0.ExecuteReader())
            {
                table.Load(reader);
                reader.Close();
            }

            // DataTableをListに入れ替え
            var list = new List<CollectMailRow>();
            foreach (DataRow row in table.Rows)
            {
                list.Add(new CollectMailRow()
                {
                    CollectTargetNo = (long)row[0],
                    SendDate = (DateTime)row[1],
                    登録日時 = (DateTime)row[2],
                    CurrentSubject = (string)row[3],
                    CurrentBody = (string)row[4]
                });
            }

            return list;
        }

        public static void Update_State(SqlConnection cn,
            long CollectTargetNo, DateTime SendDate, DateTime 登録日時, State State)
        {
            try
            {
                cmdUpdate_State.Connection = cn;
                cmdUpdate_State.Parameters["CollectTargetNo"].Value = CollectTargetNo;
                cmdUpdate_State.Parameters["SendDate"].Value = SendDate;
                cmdUpdate_State.Parameters["登録日時"].Value = 登録日時;
                cmdUpdate_State.Parameters["State"].Value = State;
                //cmdUpdateCollectMail_State.Parameters["不要文字列除外後"].Value = collectMailRow.不要文字列除外後;
                //cmdUpdateCollectMail_State.Parameters["英語連結名詞"].Value = collectMailRow.英語連結名詞;
                //cmdUpdateCollectMail_State.Parameters["英語連結名詞除外後"].Value = collectMailRow.英語連結名詞除外後;
                //cmdUpdateCollectMail_State.Parameters["言語判定"].Value = (byte)collectMailRow.言語判定;

                cmdUpdate_State.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ログ.ログ書き出し(ex);
                ログ.ログ書き出し("CollectTargetNo: " + CollectTargetNo +
                    "\r\n　SendDate: " + SendDate.ToString() +
                    "\r\n　登録日時: " + 登録日時.ToString() );
                    //"\r\n　英語連結名詞: " + collectMailRow.英語連結名詞 +
                    //"\r\n　英語連結名詞除外後: " + collectMailRow.英語連結名詞除外後);
                throw;
            }
        }

    }
}
