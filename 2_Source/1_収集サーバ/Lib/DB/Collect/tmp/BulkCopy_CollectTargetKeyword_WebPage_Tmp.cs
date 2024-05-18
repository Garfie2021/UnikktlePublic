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
    public static class BulkCopy_CollectTargetKeyword_WebPage_Tmp
    {
        public static DataTable Data;

        static BulkCopy_CollectTargetKeyword_WebPage_Tmp()
        {
            Data = new DataTable("tmp.tTmpCollectTargetKeywordWebPage");
            Data.Columns.Add(new DataColumn("DomainNo", typeof(long)));     // 列1
            Data.Columns.Add(new DataColumn("UrlNo", typeof(long)));         // 列2
            Data.Columns.Add(new DataColumn("KeywordNo", typeof(long)));            // 列3
            Data.Columns.Add(new DataColumn("登録日時", typeof(DateTime)));            // 列
            Data.Columns.Add(new DataColumn("更新日時", typeof(DateTime)));            // 列
        }

        // ※分かり易くする為に、１パラメータ１列を変えない。
        public static void Add(
            long DomainNo,
            long UrlNo,
            long KeywordNo,
            DateTime 登録日時更新日時)
        {
            //if (Data.Select($"SearchKeywordNo = {collectWebPageRow.SearchKeywordNo} AND SearchDate = #{collectWebPageRow.SearchDate}# AND SearchResultNo = {collectWebPageRow.SearchResultNo} AND KeywordNo = {keywordRow.No}").Length < 1)
            //{
            //    Data.Rows.Add(collectWebPageRow.SearchKeywordNo, collectWebPageRow.SearchDate, collectWebPageRow.SearchResultNo, keywordRow.No,
            //        DateTime.Now, DateTime.Now);
            //}
            Data.Rows.Add(DomainNo, UrlNo, KeywordNo, 登録日時更新日時, 登録日時更新日時);
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
                bulkcopy.DestinationTableName = "tmp.tTmpCollectTargetKeywordWebPage";
                bulkcopy.BulkCopyTimeout = DBConst.CommandTimeout_1h;
                bulkcopy.WriteToServer(Data);
            }

            Data.Clear();
        }
    }
}
