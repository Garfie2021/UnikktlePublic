using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using 定数;
using Logging;


namespace DB.Collect
{
    public static class SP_CollectYahoo
    {
        private static SqlCommand cmdSelect_State0;
        private static SqlCommand cmdSelect_CutoutStateUrl0;
        private static SqlCommand cmdUpdate_State;
        private static SqlCommand cmdUpdate_CutoutStateUrl;
        private static SqlCommand cmdTruncate;

        static SP_CollectYahoo()
        {
            cmdSelect_State0 = new SqlCommand("hst.sp1CollectYahoo_Select_State0");
            cmdSelect_State0.CommandType = CommandType.StoredProcedure;
            cmdSelect_State0.CommandTimeout = DBConst.CommandTimeout_1h;

            cmdSelect_CutoutStateUrl0 = new SqlCommand("hst.sp1CollectYahoo_Select_UrlState0");
            cmdSelect_CutoutStateUrl0.CommandType = CommandType.StoredProcedure;
            cmdSelect_CutoutStateUrl0.CommandTimeout = DBConst.CommandTimeout_1h;

            cmdUpdate_State = new SqlCommand("hst.sp1CollectYahoo_Update_State");
            cmdUpdate_State.CommandType = CommandType.StoredProcedure;
            cmdUpdate_State.CommandTimeout = DBConst.CommandTimeout_1h;
            cmdUpdate_State.Parameters.Add(new SqlParameter("SearchKeywordNo", SqlDbType.BigInt));
            cmdUpdate_State.Parameters.Add(new SqlParameter("SearchDate", SqlDbType.DateTime));
            cmdUpdate_State.Parameters.Add(new SqlParameter("State", SqlDbType.TinyInt));
            cmdUpdate_State.Parameters.Add(new SqlParameter("HtmlParseResult", SqlDbType.TinyInt));
            cmdUpdate_State.Parameters["SearchKeywordNo"].Direction = ParameterDirection.Input;
            cmdUpdate_State.Parameters["SearchDate"].Direction = ParameterDirection.Input;
            cmdUpdate_State.Parameters["State"].Direction = ParameterDirection.Input;
            cmdUpdate_State.Parameters["HtmlParseResult"].Direction = ParameterDirection.Input;

            cmdUpdate_CutoutStateUrl = new SqlCommand("hst.sp1CollectYahoo_Update_UrlState");
            cmdUpdate_CutoutStateUrl.CommandType = CommandType.StoredProcedure;
            cmdUpdate_CutoutStateUrl.CommandTimeout = DBConst.CommandTimeout_1h;
            cmdUpdate_CutoutStateUrl.Parameters.Add(new SqlParameter("SearchKeywordNo", SqlDbType.BigInt));
            cmdUpdate_CutoutStateUrl.Parameters.Add(new SqlParameter("SearchDate", SqlDbType.DateTime));
            cmdUpdate_CutoutStateUrl.Parameters.Add(new SqlParameter("UrlState", SqlDbType.TinyInt));
            cmdUpdate_CutoutStateUrl.Parameters["SearchKeywordNo"].Direction = ParameterDirection.Input;
            cmdUpdate_CutoutStateUrl.Parameters["SearchDate"].Direction = ParameterDirection.Input;
            cmdUpdate_CutoutStateUrl.Parameters["UrlState"].Direction = ParameterDirection.Input;

            cmdTruncate = new SqlCommand("hst.sp1CollectYahoo_Truncate");
            cmdTruncate.CommandType = CommandType.StoredProcedure;
            cmdTruncate.CommandTimeout = DBConst.CommandTimeout_1h;
        }


        public static DataTable Select_State0(SqlConnection cn)
        {
            cmdSelect_State0.Connection = cn;

            var table = new DataTable();
            using (var reader = cmdSelect_State0.ExecuteReader())
            {
                table.Load(reader);
                reader.Close();
            }

            return table;

            //// DataTableをListに入れ替え
            //var list = new List<CollectYahooRow>();
            //foreach (DataRow row in table.Rows)
            //{
            //    list.Add(new CollectYahooRow()
            //    {
            //        SearchKeywordNo = (long)row[0],
            //        SearchDate = (DateTime)row[1],
            //        検索結果Html = (string)row[2]
            //    });
            //}

            //return list;
        }

        public static DataTable Select_CutoutStateUrl0(SqlConnection cn)
        {
            cmdSelect_CutoutStateUrl0.Connection = cn;

            var table = new DataTable();
            using (var reader = cmdSelect_CutoutStateUrl0.ExecuteReader())
            {
                table.Load(reader);
                reader.Close();
            }

            return table;
        }

        public static void Update_State(SqlConnection cn,
            long SearchKeywordNo, DateTime SearchDate, State State, 解析結果 解析結果)
        {
            try
            {
                cmdUpdate_State.Connection = cn;
                cmdUpdate_State.Parameters["SearchKeywordNo"].Value = SearchKeywordNo;
                cmdUpdate_State.Parameters["SearchDate"].Value = SearchDate;
                cmdUpdate_State.Parameters["State"].Value = (byte)State;
                cmdUpdate_State.Parameters["HtmlParseResult"].Value = (byte)解析結果;
                
                cmdUpdate_State.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ログ.ログ書き出し(ex);
                ログ.ログ書き出し("SearchKeywordNo: " + SearchKeywordNo +
                    "\r\n　SearchDate: " + SearchDate.ToString());
                throw;
            }
        }

        public static void Update_CutoutStateUrl(SqlConnection cn,
            long SearchKeywordNo, DateTime SearchDate, State CutoutStateUrl)
        {
            try
            {
                cmdUpdate_CutoutStateUrl.Connection = cn;
                cmdUpdate_CutoutStateUrl.Parameters["SearchKeywordNo"].Value = SearchKeywordNo;
                cmdUpdate_CutoutStateUrl.Parameters["SearchDate"].Value = SearchDate;
                cmdUpdate_CutoutStateUrl.Parameters["UrlState"].Value = CutoutStateUrl;

                cmdUpdate_CutoutStateUrl.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ログ.ログ書き出し(ex);
                ログ.ログ書き出し("SearchKeywordNo: " + SearchKeywordNo +
                    "\r\n　SearchDate: " + SearchDate.ToString());
                throw;
            }
        }

        public static void Truncate(SqlConnection cn)
        {
            ログ.ログ書き出し("Truncate : " + cn.ConnectionString);

            cmdTruncate.Connection = cn;

            cmdTruncate.ExecuteNonQuery();
        }
    }
}
