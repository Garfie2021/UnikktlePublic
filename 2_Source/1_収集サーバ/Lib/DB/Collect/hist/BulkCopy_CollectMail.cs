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
    public static class BulkCopy_CollectMail
    {
        public static DataTable Data;

        static BulkCopy_CollectMail()
        {
            Data = new DataTable("hst.t1CollectMail");
            Data.Columns.Add(new DataColumn("CollectTargetNo", typeof(long)));     // 列1
            Data.Columns.Add(new DataColumn("SendDate", typeof(DateTime)));         // 列3
            Data.Columns.Add(new DataColumn("登録日時", typeof(DateTime)));         // 列4
            Data.Columns.Add(new DataColumn("更新日時", typeof(DateTime)));         // 列4
            Data.Columns.Add(new DataColumn("State", typeof(byte)));
            Data.Columns.Add(new DataColumn("FromDisplayName", typeof(string)));    // 列6
            Data.Columns.Add(new DataColumn("CurrentMessageID", typeof(string)));   // 列7
            Data.Columns.Add(new DataColumn("CurrentSubject", typeof(string)));     // 列8
            Data.Columns.Add(new DataColumn("CurrentBody", typeof(string)));        // 列9
        }

        // ※分かり易くする為に、１パラメータ１列を変えない。
        public static void Add(
            CollectMailRow mail, 
            DateTime now)
        {
            Data.Rows.Add(
                mail.CollectTargetNo,
                mail.SendDate,
                now,
                now,
                State.未解析,
                mail.FromDisplayName,
                mail.CurrentMessageID,
                mail.CurrentSubject,
                mail.CurrentBody);
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
                bulkcopy.DestinationTableName = "hst.t1CollectMail";
                bulkcopy.BulkCopyTimeout = DBConst.CommandTimeout_1h;
                bulkcopy.WriteToServer(Data);
            }

            Data.Clear();
        }

    }
}
