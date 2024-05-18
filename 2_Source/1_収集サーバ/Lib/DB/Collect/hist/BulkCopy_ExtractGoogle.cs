using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using 定数;
using AppDirectory;
using Logging;

namespace DB.Collect
{
    public static class BulkCopy_ExtractGoogle
    {
        public static DataTable Data;

        static BulkCopy_ExtractGoogle()
        {
            Data = new DataTable("hst.t3ExtractGoogle");
            Data.Columns.Add(new DataColumn("SearchKeywordNo", typeof(long)));
            Data.Columns.Add(new DataColumn("SearchDate", typeof(DateTime)));
            Data.Columns.Add(new DataColumn("SearchResultNo", typeof(byte)));
            Data.Columns.Add(new DataColumn("関連キーワード", typeof(byte)));
            Data.Columns.Add(new DataColumn("登録日時", typeof(DateTime)));
            Data.Columns.Add(new DataColumn("更新日時", typeof(DateTime)));
            Data.Columns.Add(new DataColumn("MeCabState", typeof(byte)));
            Data.Columns.Add(new DataColumn("言語判定", typeof(byte)));
            Data.Columns.Add(new DataColumn("HtmlTag除外後", typeof(string)));
            Data.Columns.Add(new DataColumn("不要文字列除外後", typeof(string)));
            Data.Columns.Add(new DataColumn("英語連結名詞", typeof(string)));
            Data.Columns.Add(new DataColumn("英語連結名詞除外後", typeof(string)));
        }

        // ※分かり易くする為に、１パラメータ１列を変えない。
        public static void Add(
            long SearchKeywordNo,
            DateTime SearchDate,
            byte SearchResultNo,
            関連キーワードFlag 関連キーワード,
            DateTime 登録更新日時,
            言語判定結果 言語判定,
            string HtmlTag除外後,
            string 不要文字列除外後,
            string 英語連結名詞,
            string 英語連結名詞除外後)
        {
            Data.Rows.Add(
                SearchKeywordNo,
                SearchDate,
                SearchResultNo,
                関連キーワード,
                登録更新日時,
                登録更新日時,
                State.未解析,   // MeCabState
                言語判定,
                HtmlTag除外後,
                不要文字列除外後,
                string.IsNullOrEmpty(英語連結名詞) ? null : 英語連結名詞,
                英語連結名詞除外後);
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
                bulkcopy.DestinationTableName = "hst.t3ExtractGoogle";
                bulkcopy.BulkCopyTimeout = DBConst.CommandTimeout_1h;
                bulkcopy.WriteToServer(Data);
            }

            Data.Clear();
        }
    }
}
