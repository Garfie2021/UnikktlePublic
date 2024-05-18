using Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using 定数;


namespace DB.Collect
{
    public static class SP_Url
    {
        private static SqlCommand cmdInsert;
        private static SqlCommand cmdSelect;
        private static SqlCommand cmdSelect_DomainNoNull;
        private static SqlCommand cmdSelect_DomainNo;


        static SP_Url()
        {
            cmdInsert = new SqlCommand("mst.spUrl_Insert");
            cmdInsert.CommandType = CommandType.StoredProcedure;
            cmdInsert.CommandTimeout = DBConst.CommandTimeout_1h;
            cmdInsert.Parameters.Add(new SqlParameter("DomainNo", SqlDbType.BigInt));
            cmdInsert.Parameters["DomainNo"].Direction = ParameterDirection.Input;
            cmdInsert.Parameters.Add(new SqlParameter("Url", SqlDbType.NVarChar));
            cmdInsert.Parameters["Url"].Direction = ParameterDirection.Input;

            cmdSelect = new SqlCommand("mst.spUrl_Select");
            cmdSelect.CommandType = CommandType.StoredProcedure;
            cmdSelect.CommandTimeout = DBConst.CommandTimeout_1h;

            cmdSelect_DomainNoNull = new SqlCommand("mst.spUrl_Select_DomainNoNull");
            cmdSelect_DomainNoNull.CommandType = CommandType.StoredProcedure;
            cmdSelect_DomainNoNull.CommandTimeout = DBConst.CommandTimeout_1h;

            cmdSelect_DomainNo = new SqlCommand("mst.spUrl_Select_DomainNo");
            cmdSelect_DomainNo.CommandType = CommandType.StoredProcedure;
            cmdSelect_DomainNo.CommandTimeout = DBConst.CommandTimeout_1h;
        }


        public static DataTable Select_DomainNoNull(SqlConnection cn)
        {
            cmdSelect_DomainNoNull.Connection = cn;

            var table = new DataTable();
            using (var reader = cmdSelect_DomainNoNull.ExecuteReader())
            {
                table.Load(reader);
                reader.Close();
            }

            //// DataTableをListに入れ替え
            //var list = new List<UrlRow>();
            //foreach (DataRow row in table.Rows)
            //{
            //    list.Add(new UrlRow()
            //    {
            //        UrlNo = (long)row["UrlNo"],
            //        URL = (string)row["URL"]
            //    });
            //}

            return table;
        }

        public static DataTable Select_DomainNo(SqlConnection cn)
        {
            cmdSelect_DomainNo.Connection = cn;

            var table = new DataTable();
            using (var reader = cmdSelect_DomainNo.ExecuteReader())
            {
                table.Load(reader);
                reader.Close();
            }

            //// DataTableをListに入れ替え
            //var list = new List<long>();
            //foreach (DataRow row in table.Rows)
            //{
            //    list.Add((long)row["DomainNo"]);
            //}

            return table;
        }
        
        //public static List<UrlRow> Select_No_Url(SqlConnection cn, long domainNo)
        public static DataTable Select_No_Url(SqlConnection cn, long domainNo)
        {
            // 並列実行から呼ばれるので、インスタンスはメソッド内で作る。
            using (var cmd = new SqlCommand("mst.spUrl_Select_No_Url"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("DomainNo", SqlDbType.BigInt));
                cmd.Parameters["DomainNo"].Direction = ParameterDirection.Input;

                cmd.Connection = cn;
                cmd.Parameters["DomainNo"].Value = domainNo;

                var table = new DataTable();
                using (var reader = cmd.ExecuteReader())
                {
                    table.Load(reader);
                    reader.Close();
                }

                return table;

                //// DataTableをListに入れ替え
                //var list = new List<UrlRow>();
                //foreach (DataRow row in table.Rows)
                //{
                //    list.Add(new UrlRow()
                //    {
                //        UrlNo = (long)row["UrlNo"],
                //        URL = (string)row["URL"],
                //        CollectDate = (DateTime)row["CollectDate"]
                //    });
                //}

                //return list;
            }
        }


        public static void Insert(SqlConnection cn, long domainNo, string url)
        {
            try
            {
                cmdInsert.Connection = cn;
                cmdInsert.Parameters["DomainNo"].Value = domainNo;
                cmdInsert.Parameters["Url"].Value = url;

                cmdInsert.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ログ.ログ書き出し(ex);
                ログ.ログ書き出し("domainNo:" + domainNo + "　Url:" + url);
                throw;
            }
        }

        public static void UpdateCollectDate(SqlConnection cn, DateTime now, UrlRow row)
        {
            try
            {
                // 並列実行から呼ばれるので、インスタンスはメソッド内で作る。
                using (var cmd = new SqlCommand("mst.spUrl_UpdateCollectDate"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("DomainNo", SqlDbType.BigInt));
                    cmd.Parameters.Add(new SqlParameter("UrlNo", SqlDbType.BigInt));
                    cmd.Parameters.Add(new SqlParameter("CollectDate", SqlDbType.DateTime));
                    cmd.Parameters["DomainNo"].Direction = ParameterDirection.Input;
                    cmd.Parameters["UrlNo"].Direction = ParameterDirection.Input;
                    cmd.Parameters["CollectDate"].Direction = ParameterDirection.Input;

                    cmd.Connection = cn;
                    cmd.Parameters["DomainNo"].Value = row.DomainNo;
                    cmd.Parameters["UrlNo"].Value = row.UrlNo;
                    cmd.Parameters["CollectDate"].Value = now;

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                ログ.ログ書き出し(ex);
                ログ.ログ書き出し("UrlNo: " + row.UrlNo + "　CollectDate: " + now + "　CollectState: " + row.CollectState);
                throw;
            }
        }


    }

}
