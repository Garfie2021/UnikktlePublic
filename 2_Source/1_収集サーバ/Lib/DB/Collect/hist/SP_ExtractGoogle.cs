﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using 定数;
using Logging;


namespace DB.Collect
{
    public static class SP_ExtractGoogle
    {
        private static SqlCommand cmdSelect_MeCabState0;
        private static SqlCommand cmdUpdate_MeCabState;

        static SP_ExtractGoogle()
        {
            cmdSelect_MeCabState0 = new SqlCommand("hst.sp3ExtractGoogle_Select_MeCabState0");
            cmdSelect_MeCabState0.CommandType = CommandType.StoredProcedure;
            cmdSelect_MeCabState0.CommandTimeout = DBConst.CommandTimeout_1h;
            cmdSelect_MeCabState0.Parameters.Add(new SqlParameter("CntMeCabState0", SqlDbType.BigInt));
            cmdSelect_MeCabState0.Parameters.Add(new SqlParameter("Cnt関連キーワード以外", SqlDbType.BigInt));
            cmdSelect_MeCabState0.Parameters.Add(new SqlParameter("Cnt日本語", SqlDbType.BigInt));
            cmdSelect_MeCabState0.Parameters.Add(new SqlParameter("Cnt英語", SqlDbType.BigInt));
            cmdSelect_MeCabState0.Parameters["CntMeCabState0"].Direction = ParameterDirection.Output;
            cmdSelect_MeCabState0.Parameters["Cnt関連キーワード以外"].Direction = ParameterDirection.Output;
            cmdSelect_MeCabState0.Parameters["Cnt日本語"].Direction = ParameterDirection.Output;
            cmdSelect_MeCabState0.Parameters["Cnt英語"].Direction = ParameterDirection.Output;
            //cmd.CommandTimeout = DB定数.CommandTimeout;

            cmdUpdate_MeCabState = new SqlCommand("hst.sp3ExtractGoogle_Update_MeCabState");
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
        }

        public static List<ExtractGoogleRow> Select_MeCabState0(SqlConnection cn,
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
            var list = new List<ExtractGoogleRow>();
            foreach (DataRow row in table.Rows)
            {
                list.Add(new ExtractGoogleRow()
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
            ExtractGoogleRow extractGoogleRow, State MeCabState, string MeCab名詞)
        {
            cmdUpdate_MeCabState.Connection = cn;
            cmdUpdate_MeCabState.Parameters["SearchKeywordNo"].Value = extractGoogleRow.SearchKeywordNo;
            cmdUpdate_MeCabState.Parameters["SearchDate"].Value = extractGoogleRow.SearchDate;
            cmdUpdate_MeCabState.Parameters["SearchResultNo"].Value = extractGoogleRow.SearchResultNo;
            cmdUpdate_MeCabState.Parameters["MeCabState"].Value = MeCabState;
            cmdUpdate_MeCabState.Parameters["MeCab名詞"].Value = MeCab名詞;

            cmdUpdate_MeCabState.ExecuteNonQuery();
        }

    }
}
