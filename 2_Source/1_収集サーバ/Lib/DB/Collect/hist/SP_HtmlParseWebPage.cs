using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using 定数;
using Logging;


namespace DB.Collect
{
    public static class SP_HtmlParseWebPage
    {
        private static SqlCommand cmdGetCount;
        private static SqlCommand cmdSelect_State0;
        private static SqlCommand cmdUpdate_State;
        private static SqlCommand cmdUpdate_Html;

        static SP_HtmlParseWebPage()
        {
            cmdGetCount = new SqlCommand("hst.sp2HtmlParseWebPage_GetCount");
            cmdGetCount.CommandType = CommandType.StoredProcedure;
            cmdGetCount.CommandTimeout = DBConst.CommandTimeout_1h;
            cmdGetCount.Parameters.Add(new SqlParameter("DomainNo", SqlDbType.BigInt));
            cmdGetCount.Parameters.Add(new SqlParameter("UrlNo", SqlDbType.BigInt));
            cmdGetCount.Parameters.Add(new SqlParameter("Cnt", SqlDbType.BigInt));
            cmdGetCount.Parameters["DomainNo"].Direction = ParameterDirection.Input;
            cmdGetCount.Parameters["UrlNo"].Direction = ParameterDirection.Input;
            cmdGetCount.Parameters["Cnt"].Direction = ParameterDirection.Output;

            cmdSelect_State0 = new SqlCommand("hst.sp2HtmlParseWebPage_Select_State0");
            cmdSelect_State0.CommandType = CommandType.StoredProcedure;
            cmdSelect_State0.CommandTimeout = DBConst.CommandTimeout_1h;

            cmdUpdate_State = new SqlCommand("hst.sp2HtmlParseWebPage_Update_State");
            cmdUpdate_State.CommandType = CommandType.StoredProcedure;
            cmdUpdate_State.CommandTimeout = DBConst.CommandTimeout_1h;
            cmdUpdate_State.Parameters.Add(new SqlParameter("DomainNo", SqlDbType.BigInt));
            cmdUpdate_State.Parameters.Add(new SqlParameter("UrlNo", SqlDbType.BigInt));
            cmdUpdate_State.Parameters.Add(new SqlParameter("State", SqlDbType.TinyInt));
            cmdUpdate_State.Parameters["DomainNo"].Direction = ParameterDirection.Input;
            cmdUpdate_State.Parameters["UrlNo"].Direction = ParameterDirection.Input;
            cmdUpdate_State.Parameters["State"].Direction = ParameterDirection.Input;

            cmdUpdate_Html = new SqlCommand("hst.sp2HtmlParseWebPage_Update_Html");
            cmdUpdate_Html.CommandType = CommandType.StoredProcedure;
            cmdUpdate_Html.CommandTimeout = DBConst.CommandTimeout_1h;
            cmdUpdate_Html.Parameters.Add(new SqlParameter("DomainNo", SqlDbType.BigInt));
            cmdUpdate_Html.Parameters.Add(new SqlParameter("UrlNo", SqlDbType.BigInt));
            cmdUpdate_Html.Parameters.Add(new SqlParameter("言語判定", SqlDbType.TinyInt));
            cmdUpdate_Html.Parameters.Add(new SqlParameter("HtmlTag除外後1段階目", SqlDbType.NText));
            cmdUpdate_Html.Parameters.Add(new SqlParameter("HtmlTag除外後2段階目", SqlDbType.NText));
            cmdUpdate_Html.Parameters["DomainNo"].Direction = ParameterDirection.Input;
            cmdUpdate_Html.Parameters["UrlNo"].Direction = ParameterDirection.Input;
            cmdUpdate_Html.Parameters["言語判定"].Direction = ParameterDirection.Input;
            cmdUpdate_Html.Parameters["HtmlTag除外後1段階目"].Direction = ParameterDirection.Input;
            cmdUpdate_Html.Parameters["HtmlTag除外後2段階目"].Direction = ParameterDirection.Input;
        }


        public static long GetCount(SqlConnection cn, long domainNo, long urlNo)
        {
            cmdGetCount.Connection = cn;
            cmdGetCount.Parameters["DomainNo"].Value = domainNo;
            cmdGetCount.Parameters["UrlNo"].Value = urlNo;

            cmdGetCount.ExecuteNonQuery();

            return (long)cmdGetCount.Parameters["Cnt"].Value;
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
            //var list = new List<HtmlParseWebPageRow>();
            //foreach (DataRow row in table.Rows)
            //{
            //    list.Add(new HtmlParseWebPageRow()
            //    {
            //        SearchKeywordNo = (long)row[0],
            //        SearchDate = (DateTime)row[1],
            //        HtmlTag除外後2段階目 = (string)row[2]
            //    });
            //}
            //
            //return list;
        }

        public static void Update_State(SqlConnection cn,
            DataRow HtmlParseWebPageRow, State State)
        {
            try
            {
                cmdUpdate_State.Connection = cn;
                cmdUpdate_State.Parameters["DomainNo"].Value = HtmlParseWebPageRow["DomainNo"];
                cmdUpdate_State.Parameters["UrlNo"].Value = HtmlParseWebPageRow["UrlNo"];
                cmdUpdate_State.Parameters["State"].Value = State;

                cmdUpdate_State.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ログ.ログ書き出し(ex);
                ログ.ログ書き出し("DomainNo: " + HtmlParseWebPageRow["DomainNo"] +
                    "\r\n　UrlNo: " + HtmlParseWebPageRow["UrlNo"]);
                throw;
            }
        }

        public static void Update_Html(SqlConnection cn,
            long domainNo, long urlNo, 言語判定結果? 言語判定, string HtmlTag除外後1段階目, string HtmlTag除外後2段階目)
        {
            try
            {
                cmdUpdate_Html.Connection = cn;
                cmdUpdate_Html.Parameters["DomainNo"].Value = domainNo;
                cmdUpdate_Html.Parameters["UrlNo"].Value = urlNo;
                cmdUpdate_Html.Parameters["言語判定"].Value = 言語判定;

                // デバッグで必要な時のみ有効化する。
                //cmdUpdate_Html.Parameters["HtmlTag除外後1段階目"].Value = HtmlTag除外後1段階目;
                cmdUpdate_Html.Parameters["HtmlTag除外後1段階目"].Value = DBNull.Value;

                cmdUpdate_Html.Parameters["HtmlTag除外後2段階目"].Value = HtmlTag除外後2段階目;

                cmdUpdate_Html.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ログ.ログ書き出し(ex);
                ログ.ログ書き出し("domainNo: " + domainNo + " urlNo: " + urlNo);
                throw;
            }
        }

    }
}
