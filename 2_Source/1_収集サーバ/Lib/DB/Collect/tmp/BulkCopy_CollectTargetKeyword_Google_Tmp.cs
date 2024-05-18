using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using 定数;
using AppDirectory;
using Logging;

namespace DB.Collect
{
    public static class BulkCopy_CollectTargetKeyword_Google_Tmp
    {
        public static DataTable Data;

        static BulkCopy_CollectTargetKeyword_Google_Tmp()
        {
            Data = new DataTable("tmp.tTmpCollectTargetKeywordGoogle");
            Data.Columns.Add(new DataColumn("SearchKeywordNo", typeof(long)));     // 列1
            Data.Columns.Add(new DataColumn("SearchDate", typeof(DateTime)));         // 列2
            Data.Columns.Add(new DataColumn("SearchResultNo", typeof(byte)));         // 列2
            Data.Columns.Add(new DataColumn("KeywordNo", typeof(long)));            // 列3
            Data.Columns.Add(new DataColumn("登録日時", typeof(DateTime)));            // 列
            Data.Columns.Add(new DataColumn("更新日時", typeof(DateTime)));            // 列
        }

        // ※分かり易くする為に、１パラメータ１列を変えない。
        public static void Add(
            long SearchKeywordNo,
            DateTime SearchDate,
            byte SearchResultNo,
            long KeywordNo,
            DateTime 登録日時更新日時)
        {
            //if (Data.Select($"SearchKeywordNo = {collectGoogleRow.SearchKeywordNo} AND SearchDate = #{collectGoogleRow.SearchDate}# AND SearchResultNo = {collectGoogleRow.SearchResultNo} AND KeywordNo = {keywordRow.No}").Length < 1)
            //{
            //    Data.Rows.Add(collectGoogleRow.SearchKeywordNo, collectGoogleRow.SearchDate, collectGoogleRow.SearchResultNo, keywordRow.No,
            //        DateTime.Now, DateTime.Now);
            //}
            Data.Rows.Add(SearchKeywordNo, SearchDate, SearchResultNo, KeywordNo, 登録日時更新日時, 登録日時更新日時);
        }

        public static void Flush(string connectionString)
        {
            //var connectionString = AppSetting.ConnectionString_UnikktleCollect;
            //ログ.ログ書き出し("Flush() connectionString : " + connectionString);

            if (Data.Rows.Count < 1)
            {
                return;
            }

            using (SqlBulkCopy bulkcopy = new SqlBulkCopy(connectionString))
            {
                bulkcopy.DestinationTableName = "tmp.tTmpCollectTargetKeywordGoogle";
                bulkcopy.BulkCopyTimeout = DBConst.CommandTimeout_1h;
                bulkcopy.WriteToServer(Data);
            }

            Data.Clear();
        }
    }
}
