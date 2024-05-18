using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using 定数;
using Logging;


namespace DB.Collect
{
    public static class SP_ExtractWebPage
    {
        private static SqlCommand cmdGetCount;
        private static SqlCommand cmdSelect_MeCabState0;
        private static SqlCommand cmdUpdate_MeCabState;
        private static SqlCommand cmdUpdate_英語連結名詞;

        static SP_ExtractWebPage()
        {
            cmdGetCount = new SqlCommand("hst.sp3ExtractWebPage_GetCount");
            cmdGetCount.CommandType = CommandType.StoredProcedure;
            //cmdGetCount.CommandTimeout = DBConst.CommandTimeout_1h;
            cmdGetCount.CommandTimeout = DBConst.CommandTimeout_1h;
            cmdGetCount.Parameters.Add(new SqlParameter("DomainNo", SqlDbType.BigInt));
            cmdGetCount.Parameters.Add(new SqlParameter("UrlNo", SqlDbType.BigInt));
            cmdGetCount.Parameters.Add(new SqlParameter("Cnt", SqlDbType.BigInt));
            cmdGetCount.Parameters["DomainNo"].Direction = ParameterDirection.Input;
            cmdGetCount.Parameters["UrlNo"].Direction = ParameterDirection.Input;
            cmdGetCount.Parameters["Cnt"].Direction = ParameterDirection.Output;

            cmdSelect_MeCabState0 = new SqlCommand("hst.sp3ExtractWebPage_Select_MeCabState0");
            cmdSelect_MeCabState0.CommandType = CommandType.StoredProcedure;
            //cmdSelect_MeCabState0.CommandTimeout = DBConst.CommandTimeout_6h;
            cmdSelect_MeCabState0.CommandTimeout = DBConst.CommandTimeout_1h;
            cmdSelect_MeCabState0.Parameters.Add(new SqlParameter("CntMeCabState0", SqlDbType.BigInt));
            cmdSelect_MeCabState0.Parameters.Add(new SqlParameter("Cnt関連キーワード以外", SqlDbType.BigInt));
            cmdSelect_MeCabState0.Parameters.Add(new SqlParameter("Cnt日本語", SqlDbType.BigInt));
            cmdSelect_MeCabState0.Parameters.Add(new SqlParameter("Cnt英語", SqlDbType.BigInt));
            cmdSelect_MeCabState0.Parameters["CntMeCabState0"].Direction = ParameterDirection.Output;
            cmdSelect_MeCabState0.Parameters["Cnt関連キーワード以外"].Direction = ParameterDirection.Output;
            cmdSelect_MeCabState0.Parameters["Cnt日本語"].Direction = ParameterDirection.Output;
            cmdSelect_MeCabState0.Parameters["Cnt英語"].Direction = ParameterDirection.Output;

            cmdUpdate_MeCabState = new SqlCommand("hst.sp3ExtractWebPage_Update_MeCabState");
            cmdUpdate_MeCabState.CommandType = CommandType.StoredProcedure;
            cmdUpdate_MeCabState.CommandTimeout = DBConst.CommandTimeout_1h;
            cmdUpdate_MeCabState.Parameters.Add(new SqlParameter("SearchKeywordNo", SqlDbType.BigInt));
            cmdUpdate_MeCabState.Parameters.Add(new SqlParameter("SearchDate", SqlDbType.DateTime));
            cmdUpdate_MeCabState.Parameters.Add(new SqlParameter("SearchResultNo", SqlDbType.TinyInt));
            cmdUpdate_MeCabState.Parameters.Add(new SqlParameter("MeCabState", SqlDbType.TinyInt));
            cmdUpdate_MeCabState.Parameters.Add(new SqlParameter("MeCab名詞", SqlDbType.NVarChar));
            cmdUpdate_MeCabState.Parameters["SearchKeywordNo"].Direction = ParameterDirection.Input;
            cmdUpdate_MeCabState.Parameters["SearchDate"].Direction = ParameterDirection.Input;
            cmdUpdate_MeCabState.Parameters["SearchResultNo"].Direction = ParameterDirection.Input;
            cmdUpdate_MeCabState.Parameters["MeCabState"].Direction = ParameterDirection.Input;
            cmdUpdate_MeCabState.Parameters["MeCab名詞"].Direction = ParameterDirection.Input;

            cmdUpdate_英語連結名詞 = new SqlCommand("hst.sp3ExtractWebPage_Update_英語連結名詞");
            cmdUpdate_英語連結名詞.CommandType = CommandType.StoredProcedure;
            //cmdGetCount.CommandTimeout = DBConst.CommandTimeout_1h;
            cmdUpdate_英語連結名詞.CommandTimeout = DBConst.CommandTimeout_1h;
            cmdUpdate_英語連結名詞.Parameters.Add(new SqlParameter("DomainNo", SqlDbType.BigInt));
            cmdUpdate_英語連結名詞.Parameters.Add(new SqlParameter("UrlNo", SqlDbType.BigInt));
            cmdUpdate_英語連結名詞.Parameters.Add(new SqlParameter("更新日時", SqlDbType.DateTime));
            cmdUpdate_英語連結名詞.Parameters.Add(new SqlParameter("言語判定", SqlDbType.TinyInt));
            cmdUpdate_英語連結名詞.Parameters.Add(new SqlParameter("HtmlTag除外後", SqlDbType.NText));
            cmdUpdate_英語連結名詞.Parameters.Add(new SqlParameter("不要文字列除外後", SqlDbType.NText));
            cmdUpdate_英語連結名詞.Parameters.Add(new SqlParameter("英語連結名詞", SqlDbType.NText));
            cmdUpdate_英語連結名詞.Parameters.Add(new SqlParameter("英語連結名詞除外後", SqlDbType.NText));
            cmdUpdate_英語連結名詞.Parameters["DomainNo"].Direction = ParameterDirection.Input;
            cmdUpdate_英語連結名詞.Parameters["UrlNo"].Direction = ParameterDirection.Input;
            cmdUpdate_英語連結名詞.Parameters["更新日時"].Direction = ParameterDirection.Input;
            cmdUpdate_英語連結名詞.Parameters["言語判定"].Direction = ParameterDirection.Input;
            cmdUpdate_英語連結名詞.Parameters["HtmlTag除外後"].Direction = ParameterDirection.Input;
            cmdUpdate_英語連結名詞.Parameters["不要文字列除外後"].Direction = ParameterDirection.Input;
            cmdUpdate_英語連結名詞.Parameters["英語連結名詞"].Direction = ParameterDirection.Input;
            cmdUpdate_英語連結名詞.Parameters["英語連結名詞除外後"].Direction = ParameterDirection.Input;
        }

        public static long GetCount(SqlConnection cn, long domainNo, long urlNo)
        {
            cmdGetCount.Connection = cn;
            cmdGetCount.Parameters["DomainNo"].Value = domainNo;
            cmdGetCount.Parameters["UrlNo"].Value = urlNo;

            cmdGetCount.ExecuteNonQuery();

            return (long)cmdGetCount.Parameters["Cnt"].Value;
        }

        public static List<ExtractWebPageRow> Select_MeCabState0(SqlConnection cn,
            out long CntMeCabState0,
            out long Cnt関連キーワード以外,
            out long Cnt日本語,
            out long Cnt英語)
        {
            cmdSelect_MeCabState0.Connection = cn;

            var table = new DataTable();
            using (var reader = cmdSelect_MeCabState0.ExecuteReader())
            {
                table.Load(reader);
                reader.Close();
            }

            CntMeCabState0 = (long)cmdSelect_MeCabState0.Parameters["CntMeCabState0"].Value;
            Cnt関連キーワード以外 = (long)cmdSelect_MeCabState0.Parameters["Cnt関連キーワード以外"].Value;
            Cnt日本語 = (long)cmdSelect_MeCabState0.Parameters["Cnt日本語"].Value;
            Cnt英語 = (long)cmdSelect_MeCabState0.Parameters["Cnt英語"].Value;

            // DataTableをListに入れ替え
            var list = new List<ExtractWebPageRow>();
            foreach (DataRow row in table.Rows)
            {
                list.Add(new ExtractWebPageRow()
                {
                    SearchKeywordNo = (long)row[0],
                    SearchDate = (DateTime)row[1],
                    SearchResultNo = (byte)row[2],
                    英語連結名詞除外後 = (string)row[3]
                });
            }

            return list;
        }

        public static void Update_MeCabState(SqlConnection cn,
            ExtractWebPageRow extractWebPageRow, State MeCabState, string MeCab名詞)
        {
            cmdUpdate_MeCabState.Connection = cn;
            cmdUpdate_MeCabState.Parameters["SearchKeywordNo"].Value = extractWebPageRow.SearchKeywordNo;
            cmdUpdate_MeCabState.Parameters["SearchDate"].Value = extractWebPageRow.SearchDate;
            cmdUpdate_MeCabState.Parameters["SearchResultNo"].Value = extractWebPageRow.SearchResultNo;
            cmdUpdate_MeCabState.Parameters["MeCabState"].Value = MeCabState;
            cmdUpdate_MeCabState.Parameters["MeCab名詞"].Value = MeCab名詞;

            cmdUpdate_MeCabState.ExecuteNonQuery();
        }

        public static void Update_英語連結名詞(SqlConnection cn,
            long domainNo, long urlNo, DateTime now, 言語判定結果 言語判定, 
            string 抽出文, string 不要文字列除外後, string 英語連結名詞, string 英語連結名詞除外後)
        {
            try
            {
                cmdUpdate_英語連結名詞.Connection = cn;
                cmdUpdate_英語連結名詞.Parameters["DomainNo"].Value = domainNo;
                cmdUpdate_英語連結名詞.Parameters["UrlNo"].Value = urlNo;
                cmdUpdate_英語連結名詞.Parameters["更新日時"].Value = now;
                cmdUpdate_英語連結名詞.Parameters["言語判定"].Value = 言語判定;
                cmdUpdate_英語連結名詞.Parameters["HtmlTag除外後"].Value = 抽出文;
                cmdUpdate_英語連結名詞.Parameters["不要文字列除外後"].Value = 不要文字列除外後;
                cmdUpdate_英語連結名詞.Parameters["英語連結名詞"].Value = 英語連結名詞;
                cmdUpdate_英語連結名詞.Parameters["英語連結名詞除外後"].Value = 英語連結名詞除外後;

                cmdUpdate_英語連結名詞.ExecuteNonQuery();
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
