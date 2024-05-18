using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Renci.SshNet;
using 定数;
using DB;
using Logging;
using AppDirectory;
using Common;

namespace _8Export.WltSearchWord
{
    public static class Worker_WltSearchWord
    {
        public static void Exec()
        {
            using (var cnCollect = new SqlConnection())
            using (var cnWeb = new SqlConnection())
            {
                cnCollect.ConnectionString = AppSetting.ConnectionString_UnikktleCollect;
                cnCollect.Open();

                cnWeb.ConnectionString = AppSetting.ConnectionString_UnikktleWeb;
                cnWeb.Open();

                ログ.ログ書き出し("Collectサーバ側の最大No取得 開始");
                var maxNo = DB.Collect.SP_SearchWord.GetMaxNo(cnCollect);

                ログ.ログ書き出し("Collectサーバ側の最大値以降のSearchWordをWebサーバ側から取得 開始");
                using (var reader = DB.Web.SP_SearchWord.Select(cnWeb, maxNo))
                {
                    while (reader.Read() == true)
                    {
                        DB.Collect.BulkCopy_SearchWord.Add((long)reader[0], (string)reader[1]);
                    }
                }
            }

            ログ.ログ書き出し("Webサーバ側から取得したSearchWordをCollectサーバへInsert 開始");
            DB.Collect.BulkCopy_SearchWord.Flush(AppSetting.ConnectionString_UnikktleCollect);

            using (var cnCollect = new SqlConnection())
            {
                cnCollect.ConnectionString = AppSetting.ConnectionString_UnikktleCollect;
                cnCollect.Open();

                ログ.ログ書き出し("wlt.SearchWord を mst.tKeyword へ反映 開始");
                DB.Collect.SP_Keyword.Insert_FromWltSearchWord(cnCollect);
            }

        }

    }
}
