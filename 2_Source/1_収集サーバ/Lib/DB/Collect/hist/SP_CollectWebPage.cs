using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using 定数;
using Logging;


namespace DB.Collect
{
    public static class SP_CollectWebPage
    {
        private static SqlCommand cmdSelect_CutoutStateUrlNull;
        private static SqlCommand cmdUpdate_CutoutStateUrl;

        private static SqlCommand cmdSelect_CutoutStateHtmlNull;
        private static SqlCommand cmdUpdate_CutoutStateHtml;
        private static SqlCommand cmdUpdate_Update_Html_Null;

        
        static SP_CollectWebPage()
        {
            cmdSelect_CutoutStateUrlNull = new SqlCommand("hst.sp1CollectWebPage_Select_CutoutStateUrlNull");
            cmdSelect_CutoutStateUrlNull.CommandType = CommandType.StoredProcedure;
            //cmdSelect_CutoutStateUrlNull.CommandTimeout = DBConst.CommandTimeout_1h;
            cmdSelect_CutoutStateUrlNull.CommandTimeout = DBConst.CommandTimeout_1h;

            cmdUpdate_CutoutStateUrl = new SqlCommand("hst.sp1CollectWebPage_Update_CutoutStateUrl");
            cmdUpdate_CutoutStateUrl.CommandType = CommandType.StoredProcedure;
            //cmdUpdate_CutoutStateUrl.CommandTimeout = DBConst.CommandTimeout_1h;
            cmdUpdate_CutoutStateUrl.CommandTimeout = DBConst.CommandTimeout_1h;
            cmdUpdate_CutoutStateUrl.Parameters.Add(new SqlParameter("DomainNo", SqlDbType.BigInt));
            cmdUpdate_CutoutStateUrl.Parameters.Add(new SqlParameter("UrlNo", SqlDbType.BigInt));
            cmdUpdate_CutoutStateUrl.Parameters.Add(new SqlParameter("CutoutStateUrl", SqlDbType.TinyInt));
            cmdUpdate_CutoutStateUrl.Parameters["DomainNo"].Direction = ParameterDirection.Input;
            cmdUpdate_CutoutStateUrl.Parameters["UrlNo"].Direction = ParameterDirection.Input;
            cmdUpdate_CutoutStateUrl.Parameters["CutoutStateUrl"].Direction = ParameterDirection.Input;

            cmdSelect_CutoutStateHtmlNull = new SqlCommand("hst.sp1CollectWebPage_Select_CutoutStateHtmlNull");
            cmdSelect_CutoutStateHtmlNull.CommandType = CommandType.StoredProcedure;
            //cmdSelect_CutoutStateHtmlNull.CommandTimeout = DBConst.CommandTimeout_1h;
            cmdSelect_CutoutStateHtmlNull.CommandTimeout = DBConst.CommandTimeout_1h;

            cmdUpdate_CutoutStateHtml = new SqlCommand("hst.sp1CollectWebPage_Update_CutoutStateHtml");
            cmdUpdate_CutoutStateHtml.CommandType = CommandType.StoredProcedure;
            //cmdUpdate_CutoutStateHtml.CommandTimeout = DBConst.CommandTimeout_1h;
            cmdUpdate_CutoutStateHtml.CommandTimeout = DBConst.CommandTimeout_1h;
            cmdUpdate_CutoutStateHtml.Parameters.Add(new SqlParameter("DomainNo", SqlDbType.BigInt));
            cmdUpdate_CutoutStateHtml.Parameters.Add(new SqlParameter("UrlNo", SqlDbType.BigInt));
            cmdUpdate_CutoutStateHtml.Parameters.Add(new SqlParameter("CutoutStateHtml", SqlDbType.TinyInt));
            cmdUpdate_CutoutStateHtml.Parameters["DomainNo"].Direction = ParameterDirection.Input;
            cmdUpdate_CutoutStateHtml.Parameters["UrlNo"].Direction = ParameterDirection.Input;
            cmdUpdate_CutoutStateHtml.Parameters["CutoutStateHtml"].Direction = ParameterDirection.Input;

            cmdUpdate_Update_Html_Null = new SqlCommand("hst.sp1CollectWebPage_Update_Html_Null");
            cmdUpdate_Update_Html_Null.CommandType = CommandType.StoredProcedure;
            cmdUpdate_Update_Html_Null.CommandTimeout = DBConst.CommandTimeout_1h;
        }

        public static void Delete(SqlConnection cn, long domainNo, long urlNo)
        {
            try
            {
                // 並列実行から呼ばれるので、インスタンスはメソッド内で作る。
                using (var cmd = new SqlCommand("hst.sp1CollectWebPage_Delete"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("DomainNo", SqlDbType.BigInt));
                    cmd.Parameters.Add(new SqlParameter("UrlNo", SqlDbType.BigInt));
                    cmd.Parameters["DomainNo"].Direction = ParameterDirection.Input;
                    cmd.Parameters["UrlNo"].Direction = ParameterDirection.Input;

                    cmd.Connection = cn;
                    cmd.Parameters["DomainNo"].Value = domainNo;
                    cmd.Parameters["UrlNo"].Value = urlNo;

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                ログ.ログ書き出し("DomainNo: " + domainNo + "UrlNo: " + urlNo);
                ログ.ログ書き出し(ex);
                throw;
            }
        }

        public static void Update_Html(SqlConnection cn,
            long domainNo, long urlNo, CollectState collectState, string html)
        {
            try
            {
                // 並列実行から呼ばれるので、インスタンスはメソッド内で作る。
                using (var cmd = new SqlCommand("hst.sp1CollectWebPage_Update_Html"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("DomainNo", SqlDbType.BigInt));
                    cmd.Parameters.Add(new SqlParameter("UrlNo", SqlDbType.BigInt));
                    cmd.Parameters.Add(new SqlParameter("CollectState", SqlDbType.TinyInt));
                    cmd.Parameters.Add(new SqlParameter("Html", SqlDbType.NText));
                    cmd.Parameters["DomainNo"].Direction = ParameterDirection.Input;
                    cmd.Parameters["UrlNo"].Direction = ParameterDirection.Input;
                    cmd.Parameters["CollectState"].Direction = ParameterDirection.Input;
                    cmd.Parameters["Html"].Direction = ParameterDirection.Input;

                    cmd.Connection = cn;
                    cmd.Parameters["DomainNo"].Value = domainNo;
                    cmd.Parameters["UrlNo"].Value = urlNo;
                    cmd.Parameters["CollectState"].Value = collectState;
                    cmd.Parameters["Html"].Value = html;

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                ログ.ログ書き出し("DomainNo: " + domainNo + "UrlNo: " + urlNo);
                ログ.ログ書き出し(ex);
                throw;
            }
        }

        public static void Update_CutoutStateUrl(SqlConnection cn,
            long domainNo, long urlNo, 解析結果 state)
        {
            try
            {
                cmdUpdate_CutoutStateUrl.Connection = cn;
                cmdUpdate_CutoutStateUrl.Parameters["DomainNo"].Value = domainNo;
                cmdUpdate_CutoutStateUrl.Parameters["UrlNo"].Value = urlNo;
                cmdUpdate_CutoutStateUrl.Parameters["CutoutStateUrl"].Value = state;

                cmdUpdate_CutoutStateUrl.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ログ.ログ書き出し(ex);
                ログ.ログ書き出し("UrlNo: " + urlNo);
                throw;
            }
        }


        //public static SqlDataReader Select_CutoutStateUrlNull(SqlConnection cn)
        //{
        //    cmdSelect_CutoutStateUrlNull.Connection = cn;

        //    return cmdSelect_CutoutStateUrlNull.ExecuteReader();
        //}
        public static DataTable Select_CutoutStateUrlNull(SqlConnection cn)
        {
            cmdSelect_CutoutStateUrlNull.Connection = cn;

            var table = new DataTable();
            using (var reader = cmdSelect_CutoutStateUrlNull.ExecuteReader())
            {
                table.Load(reader);
                reader.Close();
            }

            return table;
        }

        //public static SqlDataReader Select_CutoutStateHtmlNull(SqlConnection cn)
        //{
        //    cmdSelect_CutoutStateHtmlNull.Connection = cn;

        //    return cmdSelect_CutoutStateHtmlNull.ExecuteReader();
        //}
        public static DataTable Select_CutoutStateHtmlNull(SqlConnection cn)
        {
            cmdSelect_CutoutStateHtmlNull.Connection = cn;

            var table = new DataTable();
            using (var reader = cmdSelect_CutoutStateHtmlNull.ExecuteReader())
            {
                table.Load(reader);
                reader.Close();
            }

            return table;

            //// DataTableをListに入れ替え
            //var list = new List<CollectWebPageRow>();
            //foreach (DataRow row in table.Rows)
            //{
            //    list.Add(new CollectWebPageRow()
            //    {
            //        SearchKeywordNo = (long)row[0],
            //        SearchDate = (DateTime)row[1],
            //        検索結果Html = (string)row[2]
            //    });
            //}

            //return list;
        }


        public static void Update_CutoutStateHtml(SqlConnection cn,
            long domainNo, long urlNo, 解析結果 state)
        {
            try
            {
                cmdUpdate_CutoutStateHtml.Connection = cn;
                cmdUpdate_CutoutStateHtml.Parameters["DomainNo"].Value = domainNo;
                cmdUpdate_CutoutStateHtml.Parameters["UrlNo"].Value = urlNo;
                cmdUpdate_CutoutStateHtml.Parameters["CutoutStateHtml"].Value = state;

                cmdUpdate_CutoutStateHtml.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ログ.ログ書き出し(ex);
                ログ.ログ書き出し("UrlNo: " + urlNo);
                throw;
            }
        }

        public static void Update_Html_Null(SqlConnection cn)
        {
            try
            {
                cmdUpdate_Update_Html_Null.Connection = cn;

                cmdUpdate_Update_Html_Null.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ログ.ログ書き出し(ex);
                throw;
            }
        }

    }
}
