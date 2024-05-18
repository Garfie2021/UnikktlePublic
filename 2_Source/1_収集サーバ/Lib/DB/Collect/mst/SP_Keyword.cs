using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using 定数;
using Logging;


namespace DB.Collect
{
    public static class SP_Keyword
    {
        private static SqlCommand cmdSelect_更新日時;
        private static SqlCommand cmdGetCount;
        private static SqlCommand cmdGetKeywordWithInsert;
        private static SqlCommand cmdInsert;
        private static SqlCommand cmdInsert_FromWltSearchWord;
        private static SqlCommand cmdUpdate_Google検索日時;
        private static SqlCommand cmdUpdate_Yahoo検索日時;
        private static SqlCommand cmdUpdate_Bing検索日時;
        private static SqlCommand cmdUpdate_採用;
        private static SqlCommand cmdSelect;
        private static SqlCommand cmdSelect_Google検索;
        private static SqlCommand cmdSelect_Yahoo検索;
        private static SqlCommand cmdSelect_Bing検索;

        static SP_Keyword()
        {
            cmdSelect_更新日時 = new SqlCommand("mst.spKeyword_Select_更新日時");
            cmdSelect_更新日時.CommandType = CommandType.StoredProcedure;
            cmdSelect_更新日時.CommandTimeout = DBConst.CommandTimeout_6h;
            cmdSelect_更新日時.Parameters.Add(new SqlParameter("更新日時Start", SqlDbType.DateTime));
            cmdSelect_更新日時.Parameters.Add(new SqlParameter("更新日時End", SqlDbType.DateTime));
            cmdSelect_更新日時.Parameters["更新日時Start"].Direction = ParameterDirection.Input;
            cmdSelect_更新日時.Parameters["更新日時End"].Direction = ParameterDirection.Input;

            cmdGetCount = new SqlCommand("mst.spKeyword_GetCount");
            cmdGetCount.CommandType = CommandType.StoredProcedure;
            cmdGetCount.CommandTimeout = DBConst.CommandTimeout_1h;
            cmdGetCount.Parameters.Add(new SqlParameter("Cnt", SqlDbType.BigInt));
            cmdGetCount.Parameters["Cnt"].Direction = ParameterDirection.Output;

            cmdGetKeywordWithInsert = new SqlCommand("mst.spKeyword_GetWithInsert");
            cmdGetKeywordWithInsert.CommandType = CommandType.StoredProcedure;
            cmdGetKeywordWithInsert.CommandTimeout = DBConst.CommandTimeout_1h;
            cmdGetKeywordWithInsert.Parameters.Add(new SqlParameter("CollectTargetCategory", SqlDbType.TinyInt));
            cmdGetKeywordWithInsert.Parameters.Add(new SqlParameter("CollectNo", SqlDbType.BigInt));
            cmdGetKeywordWithInsert.Parameters.Add(new SqlParameter("SearchResultNo", SqlDbType.TinyInt));
            cmdGetKeywordWithInsert.Parameters.Add(new SqlParameter("SendDate", SqlDbType.DateTime));
            cmdGetKeywordWithInsert.Parameters.Add(new SqlParameter("DomainNo", SqlDbType.BigInt));
            cmdGetKeywordWithInsert.Parameters.Add(new SqlParameter("UrlNo", SqlDbType.BigInt));
            cmdGetKeywordWithInsert.Parameters.Add(new SqlParameter("名詞区分", SqlDbType.TinyInt));
            cmdGetKeywordWithInsert.Parameters.Add(new SqlParameter("Word", SqlDbType.NVarChar));
            cmdGetKeywordWithInsert.Parameters.Add(new SqlParameter("解析元データ", SqlDbType.NVarChar));
            cmdGetKeywordWithInsert.Parameters.Add(new SqlParameter("No", SqlDbType.BigInt));
            cmdGetKeywordWithInsert.Parameters.Add(new SqlParameter("採用", SqlDbType.TinyInt));
            cmdGetKeywordWithInsert.Parameters.Add(new SqlParameter("採用判定済み", SqlDbType.TinyInt));
            cmdGetKeywordWithInsert.Parameters["CollectTargetCategory"].Direction = ParameterDirection.Input;
            cmdGetKeywordWithInsert.Parameters["CollectNo"].Direction = ParameterDirection.Input;
            cmdGetKeywordWithInsert.Parameters["SearchResultNo"].Direction = ParameterDirection.Input;
            cmdGetKeywordWithInsert.Parameters["SendDate"].Direction = ParameterDirection.Input;
            cmdGetKeywordWithInsert.Parameters["DomainNo"].Direction = ParameterDirection.Input;
            cmdGetKeywordWithInsert.Parameters["UrlNo"].Direction = ParameterDirection.Input;
            cmdGetKeywordWithInsert.Parameters["名詞区分"].Direction = ParameterDirection.Input;
            cmdGetKeywordWithInsert.Parameters["Word"].Direction = ParameterDirection.Input;
            cmdGetKeywordWithInsert.Parameters["解析元データ"].Direction = ParameterDirection.Input;
            cmdGetKeywordWithInsert.Parameters["No"].Direction = ParameterDirection.Output;
            cmdGetKeywordWithInsert.Parameters["採用"].Direction = ParameterDirection.Output;
            cmdGetKeywordWithInsert.Parameters["採用判定済み"].Direction = ParameterDirection.Output;

            cmdInsert_FromWltSearchWord = new SqlCommand("mst.spKeyword_Insert_FromWltSearchWord");
            cmdInsert_FromWltSearchWord.CommandType = CommandType.StoredProcedure;
            cmdInsert_FromWltSearchWord.CommandTimeout = DBConst.CommandTimeout_1h;

            cmdInsert = new SqlCommand("mst.spKeyword_Insert");
            cmdInsert.CommandType = CommandType.StoredProcedure;
            cmdInsert.CommandTimeout = DBConst.CommandTimeout_1h;
            cmdInsert.Parameters.Add(new SqlParameter("TargetCategory", SqlDbType.TinyInt));
            cmdInsert.Parameters.Add(new SqlParameter("Word", SqlDbType.NVarChar));            
            cmdInsert.Parameters.Add(new SqlParameter("解析元データ", SqlDbType.NVarChar));
            cmdInsert.Parameters.Add(new SqlParameter("No", SqlDbType.BigInt));
            cmdInsert.Parameters["TargetCategory"].Direction = ParameterDirection.Input;
            cmdInsert.Parameters["Word"].Direction = ParameterDirection.Input;
            cmdInsert.Parameters["解析元データ"].Direction = ParameterDirection.Input;
            cmdInsert.Parameters["No"].Direction = ParameterDirection.Output;

            cmdUpdate_Google検索日時 = new SqlCommand("mst.spKeyword_UpdateGoogle検索日時");
            cmdUpdate_Google検索日時.CommandType = CommandType.StoredProcedure;
            cmdUpdate_Google検索日時.CommandTimeout = DBConst.CommandTimeout_1h;
            cmdUpdate_Google検索日時.Parameters.Add(new SqlParameter("No", SqlDbType.BigInt));
            cmdUpdate_Google検索日時.Parameters.Add(new SqlParameter("Google検索日時", SqlDbType.DateTime));
            cmdUpdate_Google検索日時.Parameters["No"].Direction = ParameterDirection.Input;
            cmdUpdate_Google検索日時.Parameters["Google検索日時"].Direction = ParameterDirection.Input;

            cmdUpdate_Yahoo検索日時 = new SqlCommand("mst.spKeyword_UpdateYahoo検索日時");
            cmdUpdate_Yahoo検索日時.CommandType = CommandType.StoredProcedure;
            cmdUpdate_Yahoo検索日時.CommandTimeout = DBConst.CommandTimeout_1h;
            cmdUpdate_Yahoo検索日時.Parameters.Add(new SqlParameter("No", SqlDbType.BigInt));
            cmdUpdate_Yahoo検索日時.Parameters.Add(new SqlParameter("Yahoo検索日時", SqlDbType.DateTime));
            cmdUpdate_Yahoo検索日時.Parameters["No"].Direction = ParameterDirection.Input;
            cmdUpdate_Yahoo検索日時.Parameters["Yahoo検索日時"].Direction = ParameterDirection.Input;

            cmdUpdate_Bing検索日時 = new SqlCommand("mst.spKeyword_UpdateBing検索日時");
            cmdUpdate_Bing検索日時.CommandType = CommandType.StoredProcedure;
            cmdUpdate_Bing検索日時.CommandTimeout = DBConst.CommandTimeout_1h;
            cmdUpdate_Bing検索日時.Parameters.Add(new SqlParameter("No", SqlDbType.BigInt));
            cmdUpdate_Bing検索日時.Parameters.Add(new SqlParameter("Bing検索日時", SqlDbType.DateTime));
            cmdUpdate_Bing検索日時.Parameters["No"].Direction = ParameterDirection.Input;
            cmdUpdate_Bing検索日時.Parameters["Bing検索日時"].Direction = ParameterDirection.Input;

            cmdUpdate_採用 = new SqlCommand("mst.spKeyword_Update採用");
            cmdUpdate_採用.CommandType = CommandType.StoredProcedure;
            cmdUpdate_採用.CommandTimeout = DBConst.CommandTimeout_1h;
            cmdUpdate_採用.Parameters.Add(new SqlParameter("Word", SqlDbType.VarChar));
            cmdUpdate_採用.Parameters.Add(new SqlParameter("採用", SqlDbType.TinyInt));
            cmdUpdate_採用.Parameters["Word"].Direction = ParameterDirection.Input;
            cmdUpdate_採用.Parameters["採用"].Direction = ParameterDirection.Input;

            cmdSelect_Google検索 = new SqlCommand("mst.spKeyword_SelectGoogle検索");
            cmdSelect_Google検索.CommandType = CommandType.StoredProcedure;
            cmdSelect_Google検索.CommandTimeout = DBConst.CommandTimeout_1h;

            cmdSelect_Yahoo検索 = new SqlCommand("mst.spKeyword_SelectYahoo検索");
            cmdSelect_Yahoo検索.CommandType = CommandType.StoredProcedure;
            cmdSelect_Yahoo検索.CommandTimeout = DBConst.CommandTimeout_1h;

            cmdSelect_Bing検索 = new SqlCommand("mst.spKeyword_SelectBing検索");
            cmdSelect_Bing検索.CommandType = CommandType.StoredProcedure;
            cmdSelect_Bing検索.CommandTimeout = DBConst.CommandTimeout_1h;

            cmdSelect = new SqlCommand("mst.spKeyword_Select");
            cmdSelect.CommandType = CommandType.StoredProcedure;
            //cmd.CommandTimeout = DB定数.CommandTimeout;
            cmdSelect.CommandTimeout = DBConst.CommandTimeout_1h;

        }

        public static SqlDataReader Select_更新日時(SqlConnection cn,
            DateTime 更新日時Start, DateTime 更新日時End)
        {
            cmdSelect_更新日時.Connection = cn;
            cmdSelect_更新日時.Parameters["更新日時Start"].Value = 更新日時Start;
            cmdSelect_更新日時.Parameters["更新日時End"].Value = 更新日時End;

            return cmdSelect_更新日時.ExecuteReader();
        }

        public static long GetCount(SqlConnection cn)
        {
            cmdGetCount.Connection = cn;

            cmdGetCount.ExecuteNonQuery();

            return (long)cmdGetCount.Parameters["Cnt"].Value;
        }

        public static void Update_Google検索日時(SqlConnection cn,
            long No, DateTime Google検索日時)
        {
            cmdUpdate_Google検索日時.Connection = cn;
            cmdUpdate_Google検索日時.Parameters["No"].Value = No;
            cmdUpdate_Google検索日時.Parameters["Google検索日時"].Value = Google検索日時;

            cmdUpdate_Google検索日時.ExecuteNonQuery();
        }

        public static void Update_Yahoo検索日時(SqlConnection cn,
            long No, DateTime Yahoo検索日時)
        {
            cmdUpdate_Yahoo検索日時.Connection = cn;
            cmdUpdate_Yahoo検索日時.Parameters["No"].Value = No;
            cmdUpdate_Yahoo検索日時.Parameters["Yahoo検索日時"].Value = Yahoo検索日時;

            cmdUpdate_Yahoo検索日時.ExecuteNonQuery();
        }

        public static void Update_Bing検索日時(SqlConnection cn,
            long No, DateTime Yahoo検索日時)
        {
            cmdUpdate_Bing検索日時.Connection = cn;
            cmdUpdate_Bing検索日時.Parameters["No"].Value = No;
            cmdUpdate_Bing検索日時.Parameters["Bing検索日時"].Value = Yahoo検索日時;

            cmdUpdate_Bing検索日時.ExecuteNonQuery();
        }

        public static void Update_採用(SqlConnection cn,
            string Word, 採用不採用 採用)
        {
            cmdUpdate_採用.Connection = cn;
            cmdUpdate_採用.Parameters["Word"].Value = Word;
            cmdUpdate_採用.Parameters["採用"].Value = (byte)採用;

            cmdUpdate_採用.ExecuteNonQuery();
        }

        public static KeywordRow GetKeywordWithInsert(SqlConnection cn,
            CollectTargetCategory collectTargetCategory,
            long? SearchKeywordNo,
            long? SearchResultNo,
            DateTime? SendDate,
            long? DomainNo,
            long? UrlNo,
            名詞区分 名詞区分,
            string Word,
            string 解析元データ)
        {
            cmdGetKeywordWithInsert.Connection = cn;
            cmdGetKeywordWithInsert.Parameters["CollectTargetCategory"].Value = collectTargetCategory;

            if (SearchKeywordNo == null)
            {
                cmdGetKeywordWithInsert.Parameters["CollectNo"].Value = System.Data.SqlTypes.SqlByte.Null;
            }
            else
            {
                cmdGetKeywordWithInsert.Parameters["CollectNo"].Value = SearchKeywordNo;
            }

            if (SearchResultNo == null)
            {
                cmdGetKeywordWithInsert.Parameters["SearchResultNo"].Value = System.Data.SqlTypes.SqlByte.Null;
            }
            else
            { 
                cmdGetKeywordWithInsert.Parameters["SearchResultNo"].Value = SearchResultNo;
            }

            if (SendDate == null)
            {
                cmdGetKeywordWithInsert.Parameters["SendDate"].Value = System.Data.SqlTypes.SqlDateTime.Null;
            }
            else
            {
                cmdGetKeywordWithInsert.Parameters["SendDate"].Value = SendDate;
            }

            if (DomainNo == null)
            {
                cmdGetKeywordWithInsert.Parameters["DomainNo"].Value = System.Data.SqlTypes.SqlDateTime.Null;
            }
            else
            {
                cmdGetKeywordWithInsert.Parameters["DomainNo"].Value = DomainNo;
            }

            if (UrlNo == null)
            {
                cmdGetKeywordWithInsert.Parameters["UrlNo"].Value = System.Data.SqlTypes.SqlDateTime.Null;
            }
            else
            {
                cmdGetKeywordWithInsert.Parameters["UrlNo"].Value = UrlNo;
            }

            cmdGetKeywordWithInsert.Parameters["名詞区分"].Value = 名詞区分;
            cmdGetKeywordWithInsert.Parameters["Word"].Value = Word;
            cmdGetKeywordWithInsert.Parameters["解析元データ"].Value = 解析元データ;

            cmdGetKeywordWithInsert.ExecuteNonQuery();

            return new KeywordRow()
            {
                No = (long)cmdGetKeywordWithInsert.Parameters["No"].Value,
                採用 = (byte)cmdGetKeywordWithInsert.Parameters["採用"].Value,
                採用判定済み = (byte)cmdGetKeywordWithInsert.Parameters["採用判定済み"].Value
            };
        }

        public static void Insert_FromWltSearchWord(SqlConnection cn)
        {
            try
            {
                cmdInsert_FromWltSearchWord.Connection = cn;

                cmdInsert_FromWltSearchWord.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ログ.ログ書き出し(ex);
                throw;
            }
        }

        public static long Insert(SqlConnection cn,
            string Keyword, string 解析元データ)
        {
            try
            {
                cmdInsert.Connection = cn;
                cmdInsert.Parameters["TargetCategory"].Value = 0;
                cmdInsert.Parameters["Word"].Value = Keyword;
                cmdInsert.Parameters["解析元データ"].Value = 解析元データ;

                cmdInsert.ExecuteNonQuery();

                return (long)cmdInsert.Parameters["No"].Value;
            }
            catch (Exception ex)
            {
                ログ.ログ書き出し(ex);
                ログ.ログ書き出し("Keyword: " + Keyword);
                throw;
            }
        }

        public static List<KeywordRow> Select_Google検索(SqlConnection cn)
        {
            cmdSelect_Google検索.Connection = cn;

            var table = new DataTable();
            using (var reader = cmdSelect_Google検索.ExecuteReader())
            {
                table.Load(reader);
                reader.Close();
            }

            // DataTableをListに入れ替え
            var list = new List<KeywordRow>();
            foreach (DataRow row in table.Rows)
            {
                list.Add(new KeywordRow()
                {
                    No = (long)row[0],
                    Word = (string)row[1],
                });
            }

            return list;
        }

        public static List<KeywordRow> Select_Yahoo検索(SqlConnection cn)
        {
            cmdSelect_Yahoo検索.Connection = cn;

            var table = new DataTable();
            using (var reader = cmdSelect_Yahoo検索.ExecuteReader())
            {
                table.Load(reader);
                reader.Close();
            }

            // DataTableをListに入れ替え
            var list = new List<KeywordRow>();
            foreach (DataRow row in table.Rows)
            {
                list.Add(new KeywordRow()
                {
                    No = (long)row[0],
                    Word = (string)row[1],
                });
            }

            return list;
        }

        public static List<KeywordRow> Select_Bing検索(SqlConnection cn)
        {
            cmdSelect_Bing検索.Connection = cn;

            var table = new DataTable();
            using (var reader = cmdSelect_Bing検索.ExecuteReader())
            {
                table.Load(reader);
                reader.Close();
            }

            // DataTableをListに入れ替え
            var list = new List<KeywordRow>();
            foreach (DataRow row in table.Rows)
            {
                list.Add(new KeywordRow()
                {
                    No = (long)row[0],
                    Word = (string)row[1],
                });
            }

            return list;
        }



        // 今は使ってない。
        public static List<KeywordRow> Select(SqlConnection cn)
        {
            cmdSelect.Connection = cn;

            var table = new DataTable();
            using (var reader = cmdSelect.ExecuteReader())
            {
                table.Load(reader);
                reader.Close();
            }

            // DataTableをListに入れ替え
            var list = new List<KeywordRow>();
            foreach (DataRow row in table.Rows)
            {
                list.Add(new KeywordRow()
                {
                    No = (long)row[0],
                    Word = (string)row[1],
                });
            }

            return list;
        }

    }
}
