using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using 定数;
using Logging;


namespace DB.Collect
{
    public static class SP_HtmlParseGoogle
    {
        private static SqlCommand cmdSelect_State0;
        private static SqlCommand cmdUpdate_State;

        static SP_HtmlParseGoogle()
        {
            cmdSelect_State0 = new SqlCommand("hst.sp2HtmlParseGoogle_Select_State0");
            cmdSelect_State0.CommandType = CommandType.StoredProcedure;
            cmdSelect_State0.CommandTimeout = DBConst.CommandTimeout_1h;

            cmdUpdate_State = new SqlCommand("hst.sp2HtmlParseGoogle_Update_State");
            cmdUpdate_State.CommandType = CommandType.StoredProcedure;
            cmdUpdate_State.CommandTimeout = DBConst.CommandTimeout_1h;
            cmdUpdate_State.Parameters.Add(new SqlParameter("SearchKeywordNo", SqlDbType.BigInt));
            cmdUpdate_State.Parameters.Add(new SqlParameter("SearchDate", SqlDbType.DateTime));
            cmdUpdate_State.Parameters.Add(new SqlParameter("State", SqlDbType.TinyInt));
            cmdUpdate_State.Parameters["SearchKeywordNo"].Direction = ParameterDirection.Input;
            cmdUpdate_State.Parameters["SearchDate"].Direction = ParameterDirection.Input;
            cmdUpdate_State.Parameters["State"].Direction = ParameterDirection.Input;
        }

        public static List<HtmlParseGoogleRow> Select_State0(SqlConnection cn)
        {
            cmdSelect_State0.Connection = cn;

            var table = new DataTable();
            using (var reader = cmdSelect_State0.ExecuteReader())
            {
                table.Load(reader);
                reader.Close();
            }

            // DataTableをListに入れ替え
            var list = new List<HtmlParseGoogleRow>();
            foreach (DataRow row in table.Rows)
            {
                list.Add(new HtmlParseGoogleRow()
                {
                    SearchKeywordNo = (long)row[0],
                    SearchDate = (DateTime)row[1],
                    HtmlTag除外後2段階目 = (string)row[2]
                });
            }

            return list;
        }

        public static void Update_State(SqlConnection cn,
            HtmlParseGoogleRow HtmlParseGoogleRow, State State)
        {
            try
            {
                cmdUpdate_State.Connection = cn;
                cmdUpdate_State.Parameters["SearchKeywordNo"].Value = HtmlParseGoogleRow.SearchKeywordNo;
                cmdUpdate_State.Parameters["SearchDate"].Value = HtmlParseGoogleRow.SearchDate;
                cmdUpdate_State.Parameters["State"].Value = State;

                cmdUpdate_State.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ログ.ログ書き出し(ex);
                ログ.ログ書き出し("SearchKeywordNo: " + HtmlParseGoogleRow.SearchKeywordNo +
                    "\r\n　SearchDate: " + HtmlParseGoogleRow.SearchDate.ToString());
                throw;
            }
        }

    }
}
