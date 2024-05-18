using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using 定数;
using Logging;


namespace DB.Collect
{
    public static class SP_HtmlParseBing
    {
        private static SqlCommand cmdSelect_State0;
        private static SqlCommand cmdUpdate_State;

        static SP_HtmlParseBing()
        {
            cmdSelect_State0 = new SqlCommand("hst.sp2HtmlParseBing_Select_State0");
            cmdSelect_State0.CommandType = CommandType.StoredProcedure;
            cmdSelect_State0.CommandTimeout = DBConst.CommandTimeout_1h;

            cmdUpdate_State = new SqlCommand("hst.sp2HtmlParseBing_Update_State");
            cmdUpdate_State.CommandType = CommandType.StoredProcedure;
            cmdUpdate_State.CommandTimeout = DBConst.CommandTimeout_1h;
            cmdUpdate_State.Parameters.Add(new SqlParameter("SearchKeywordNo", SqlDbType.BigInt));
            cmdUpdate_State.Parameters.Add(new SqlParameter("SearchDate", SqlDbType.DateTime));
            cmdUpdate_State.Parameters.Add(new SqlParameter("State", SqlDbType.TinyInt));
            cmdUpdate_State.Parameters["SearchKeywordNo"].Direction = ParameterDirection.Input;
            cmdUpdate_State.Parameters["SearchDate"].Direction = ParameterDirection.Input;
            cmdUpdate_State.Parameters["State"].Direction = ParameterDirection.Input;
        }


        public static List<HtmlParseBingRow> Select_State0(SqlConnection cn)
        {
            cmdSelect_State0.Connection = cn;

            var table = new DataTable();
            using (var reader = cmdSelect_State0.ExecuteReader())
            {
                table.Load(reader);
                reader.Close();
            }

            // DataTableをListに入れ替え
            var list = new List<HtmlParseBingRow>();
            foreach (DataRow row in table.Rows)
            {
                list.Add(new HtmlParseBingRow()
                {
                    SearchKeywordNo = (long)row[0],
                    SearchDate = (DateTime)row[1],
                    HtmlTag除外後2段階目 = (string)row[2]
                });
            }

            return list;
        }

        public static void Update_State(SqlConnection cn,
            HtmlParseBingRow HtmlParseBingRow, State State)
        {
            try
            {
                cmdUpdate_State.Connection = cn;
                cmdUpdate_State.Parameters["SearchKeywordNo"].Value = HtmlParseBingRow.SearchKeywordNo;
                cmdUpdate_State.Parameters["SearchDate"].Value = HtmlParseBingRow.SearchDate;
                cmdUpdate_State.Parameters["State"].Value = State;

                cmdUpdate_State.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ログ.ログ書き出し(ex);
                ログ.ログ書き出し("SearchKeywordNo: " + HtmlParseBingRow.SearchKeywordNo +
                    "\r\n　SearchDate: " + HtmlParseBingRow.SearchDate.ToString());
                throw;
            }
        }

    }
}
