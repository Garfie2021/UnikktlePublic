using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using 定数;
using DB.Cmn;
using Logging;
using AppDirectory;
using Common;

namespace IndexRebuild
{
    public static class Worker
    {
        // ALTER INDEX [IX_mst_Career_CareerName] ON [mst].[tCareer] REBUILD PARTITION = ALL WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = ON, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, DATA_COMPRESSION = PAGE)
        private const string AlterIndexRebuild = "ALTER INDEX {0} ON {1}.{2} REBUILD PARTITION = ALL WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, ONLINE = ON, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, DATA_COMPRESSION = PAGE)";

        // ALTER INDEX [PK_mst_tKeyword] ON [mst].[tKeyword] REORGANIZE  WITH ( LOB_COMPACTION = ON )
        private const string AlterIndexReorganize = "ALTER INDEX {0} ON {1}.{2} REORGANIZE  WITH ( LOB_COMPACTION = ON )";


        public static void Exec_IndexRebuild(SqlConnection cnCmn, string connectionString)
        {
            ログ.ログ書き出し("cnCmn connectionString : " + cnCmn.ConnectionString);

            foreach (var databaseName in DB.Cmn.SP_Databases.Select(cnCmn))
            {
                using (var cn = new SqlConnection(string.Format(connectionString, databaseName)))
                {
                    ログ.ログ書き出し("cn connectionString : " + cn.ConnectionString);

                    cn.Open();

                    foreach (var index in DB.Cmn.SP_Indexes.Select(cn))
                    {
                        ログ.ログ書き出し("index : " + index.DatabaseName + "." + index.SchemaName + "." + index.TableName + " " + index.IndedxName + "  Fragmentation=" + index.Fragmentation);

                        var sql = string.Format(AlterIndexRebuild, index.IndedxName, index.SchemaName, index.TableName);

                        ログ.ログ書き出し("sql : " + sql);

                        var cmd = new SqlCommand(sql, cn)
                        {
                            CommandTimeout = DBConst.CommandTimeout_6h
                        };

                        try
                        {
                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            ログ.ログ書き出し(ex.Message);
                        }

                        

                        sql = string.Format(AlterIndexReorganize, index.IndedxName, index.SchemaName, index.TableName);

                        ログ.ログ書き出し("sql : " + sql);

                        cmd = new SqlCommand(sql, cn)
                        {
                            CommandTimeout = DBConst.CommandTimeout_6h
                        };

                        try
                        {
                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            ログ.ログ書き出し(ex.Message);
                        }

                    }
                }
            }
        }

    }
}
