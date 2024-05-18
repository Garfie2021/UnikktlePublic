using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;


namespace Maintenance
{
    public static class ExecAllSpFuncGrant
    {
        private const string SqlFolder_UnikktleCmn_SP = @"3_SourceDB_SQLServer\1_データベース\UnikktleCmn\3_SP";
        private const string SqlFolder_UnikktleCmn_Func = @"3_SourceDB_SQLServer\1_データベース\UnikktleCmn\4_Func";
        private const string SqlFolder_UnikktleCollect_SP = @"3_SourceDB_SQLServer\1_データベース\UnikktleCollect\3_SP";
        private const string SqlFolder_UnikktleCollect_Func = @"3_SourceDB_SQLServer\1_データベース\UnikktleCollect\4_Func";
        private const string SqlFolder_UnikktlePayPalListen_SP = @"3_SourceDB_SQLServer\1_データベース\UnikktlePayPalListen\3_SP";
        private const string SqlFolder_UnikktlePayPalListen_Func = @"3_SourceDB_SQLServer\1_データベース\UnikktlePayPalListen\4_Func";
        private const string SqlFolder_UnikktleWeb_SP = @"3_SourceDB_SQLServer\1_データベース\UnikktleWeb\3_SP";
        private const string SqlFolder_UnikktleWeb_Func = @"3_SourceDB_SQLServer\1_データベース\UnikktleWeb\4_Func";
        private const string SqlFolder_UnikktleWebCollectWork_SP = @"3_SourceDB_SQLServer\1_データベース\UnikktleWebCollectWork\3_SP";
        private const string SqlFolder_UnikktleWebCollectWork_Func = @"3_SourceDB_SQLServer\1_データベース\UnikktleWebCollectWork\4_Func";

        private const string File_UnikktleCmn_Grant = @"3_SourceDB_SQLServer\2_セキュリティ\2_ストアド実行権\UnikktleCmn.sql";
        private const string File_UnikktlePayPalListen_Grant = @"3_SourceDB_SQLServer\2_セキュリティ\2_ストアド実行権\UnikktlePayPalListen.sql";
        private const string File_UnikktleWeb_Grant = @"3_SourceDB_SQLServer\2_セキュリティ\2_ストアド実行権\UnikktleWeb.sql";


        private const string ConnectionString_localhost_UnikktleCmn = "Data Source=localhost;Initial Catalog=UnikktleCmn;User ID=xxx;Password=xxx";
        private const string ConnectionString_localhost_UnikktleCollect = "Data Source=localhost;Initial Catalog=UnikktleCollect;User ID=xxx;Password=xxx";
        private const string ConnectionString_localhost_UnikktlePayPalListen = "Data Source=localhost;Initial Catalog=UnikktlePayPalListen;User ID=xxx;Password=xxx";
        private const string ConnectionString_localhost_UnikktleWeb = "Data Source=localhost;Initial Catalog=UnikktleWeb;User ID=xxx;Password=xxx";
        private const string ConnectionString_localhost_UnikktleWebCollectWork = "Data Source=localhost;Initial Catalog=UnikktleWebCollectWork;User ID=xxx;Password=xxx";

        private const string ConnectionString_192_168_11_5_UnikktleCmn = "Data Source=xxx;Initial Catalog=UnikktleCmn;User ID=xxx;Password=xxx";
        private const string ConnectionString_192_168_11_5_UnikktleCollect = "Data Source=xxx;Initial Catalog=UnikktleCollect;User ID=xxx;Password=xxx";
        private const string ConnectionString_192_168_11_5_UnikktlePayPalListen = "Data Source=xxx;Initial Catalog=UnikktlePayPalListen;User ID=xxx;Password=xxx";
        private const string ConnectionString_192_168_11_5_UnikktleWeb = "Data Source=xxx;Initial Catalog=UnikktleWeb;User ID=xxx;Password=xxx";
        private const string ConnectionString_192_168_11_5_UnikktleWebCollectWork = "Data Source=xxx;Initial Catalog=UnikktleWebCollectWork;User ID=xxx;Password=xxx";

        private const string ConnectionString_192_168_11_31_UnikktleCmn = "Data Source=xxx;Initial Catalog=UnikktleCmn;User ID=xxx;Password=xxx";
        private const string ConnectionString_192_168_11_31_UnikktleCollect = "Data Source=xxx;Initial Catalog=UnikktleCollect;User ID=xxx;Password=xxx";
        private const string ConnectionString_192_168_11_31_UnikktlePayPalListen = "Data Source=xxx;Initial Catalog=UnikktlePayPalListen;User ID=xxx;Password=xxx";
        private const string ConnectionString_192_168_11_31_UnikktleWeb = "Data Source=xxx;Initial Catalog=UnikktleWeb;User ID=xxx;Password=xxx";
        private const string ConnectionString_192_168_11_31_UnikktleWebCollectWork = "Data Source=xxx;Initial Catalog=UnikktleWebCollectWork;User ID=xxx;Password=xxx";

        private const string ConnectionString_160_16_75_102_UnikktleCmn = "Data Source=160.16.75.102,60002;Initial Catalog=UnikktleCmn;User ID=xxx;Password=xxx";
        private const string ConnectionString_160_16_75_102_UnikktleCollect = "Data Source=160.16.75.102,60002;Initial Catalog=UnikktleCollect;User ID=xxx;Password=xxx";
        private const string ConnectionString_160_16_75_102_UnikktlePayPalListen = "Data Source=160.16.75.102,60002;Initial Catalog=UnikktlePayPalListen;User ID=xxx;Password=xxx";
        private const string ConnectionString_160_16_75_102_UnikktleWeb = "Data Source=160.16.75.102,60002;Initial Catalog=UnikktleWeb;User ID=xxx;Password=xxx";
        private const string ConnectionString_160_16_75_102_UnikktleWebCollectWork = "Data Source=160.16.75.102,60002;Initial Catalog=UnikktleWebCollectWork;User ID=xxx;Password=xxx";


        public static string _ConnectionString;
        public static string _SqlFilePath;
        public static string _Sql;

        public static void Exec(bool chkLocalhost, bool chk192_168_11_5, bool chk192_168_11_31, bool chk160_16_75_102,
            string Trunkフォルダパス)
        {
            if (chkLocalhost)
            {
                Exec_localhost(Trunkフォルダパス);
            }

            if (chk192_168_11_5)
            {
                Exec_192_168_11_5(Trunkフォルダパス);
            }

            if (chk192_168_11_31)
            {
                Exec_192_168_11_31(Trunkフォルダパス);
            }

            if (chk160_16_75_102)
            {
                Exec_160_16_75_102(Trunkフォルダパス);
            }
        }

        private static void Exec_localhost(string Trunkフォルダパス)
        {
            // UnikktleCmn
            ExecSQL_ParentFolder(ConnectionString_localhost_UnikktleCmn, Trunkフォルダパス + "\\" + SqlFolder_UnikktleCmn_SP);
            ExecSQL_ParentFolder(ConnectionString_localhost_UnikktleCmn, Trunkフォルダパス + "\\" + SqlFolder_UnikktleCmn_Func);

            // UnikktleCollect
            ExecSQL_ParentFolder(ConnectionString_localhost_UnikktleCollect, Trunkフォルダパス + "\\" + SqlFolder_UnikktleCollect_SP);

            // UnikktlePayPalListen
            ExecSQL_ParentFolder(ConnectionString_localhost_UnikktlePayPalListen, Trunkフォルダパス + "\\" + SqlFolder_UnikktlePayPalListen_SP);

            // UnikktleWeb
            ExecSQL_ParentFolder(ConnectionString_localhost_UnikktleWeb, Trunkフォルダパス + "\\" + SqlFolder_UnikktleWeb_SP);
            ExecSQL_ParentFolder(ConnectionString_localhost_UnikktleWeb, Trunkフォルダパス + "\\" + SqlFolder_UnikktleWeb_Func);

            // UnikktleWebCollectWork
            ExecSQL_ParentFolder(ConnectionString_localhost_UnikktleWebCollectWork, Trunkフォルダパス + "\\" + SqlFolder_UnikktleWebCollectWork_SP);

            // Grant更新　UnikktleCmn
            ExecSQL_File(ConnectionString_localhost_UnikktleCmn, Trunkフォルダパス + "\\" + File_UnikktleCmn_Grant);

            // Grant更新　UnikktleWeb
            ExecSQL_File(ConnectionString_localhost_UnikktlePayPalListen, Trunkフォルダパス + "\\" + File_UnikktleWeb_Grant);

            // Grant更新　UnikktlePayPalListen
            ExecSQL_File(ConnectionString_localhost_UnikktlePayPalListen, Trunkフォルダパス + "\\" + File_UnikktlePayPalListen_Grant);
        }

        public static void Exec_192_168_11_31(string Trunkフォルダパス)
        {
            // UnikktleCmn
            ExecSQL_ParentFolder(ConnectionString_192_168_11_31_UnikktleCmn, Trunkフォルダパス + "\\" + SqlFolder_UnikktleCmn_SP);
            ExecSQL_ParentFolder(ConnectionString_192_168_11_31_UnikktleCmn, Trunkフォルダパス + "\\" + SqlFolder_UnikktleCmn_Func);

            // UnikktleCollect

            // UnikktlePayPalListen

            // UnikktleWeb
            ExecSQL_ParentFolder(ConnectionString_192_168_11_31_UnikktleWeb, Trunkフォルダパス + "\\" + SqlFolder_UnikktleWeb_SP);
            ExecSQL_ParentFolder(ConnectionString_192_168_11_31_UnikktleWeb, Trunkフォルダパス + "\\" + SqlFolder_UnikktleWeb_Func);

            // UnikktleWebCollectWork
            ExecSQL_ParentFolder(ConnectionString_192_168_11_31_UnikktleWebCollectWork, Trunkフォルダパス + "\\" + SqlFolder_UnikktleWebCollectWork_SP);

            // Grant更新　UnikktleCmn

            // Grant更新　UnikktleWeb

            // Grant更新　UnikktlePayPalListen
        }

        public static void Exec_192_168_11_5(string Trunkフォルダパス)
        {
            // UnikktleCmn
            ExecSQL_ParentFolder(ConnectionString_192_168_11_5_UnikktleCmn, Trunkフォルダパス + "\\" + SqlFolder_UnikktleCmn_SP);
            ExecSQL_ParentFolder(ConnectionString_192_168_11_5_UnikktleCmn, Trunkフォルダパス + "\\" + SqlFolder_UnikktleCmn_Func);

            // UnikktleCollect
            ExecSQL_ParentFolder(ConnectionString_192_168_11_5_UnikktleCollect, Trunkフォルダパス + "\\" + SqlFolder_UnikktleCollect_SP);

            // UnikktlePayPalListen

            // UnikktleWeb

            // UnikktleWebCollectWork
            ExecSQL_ParentFolder(ConnectionString_192_168_11_5_UnikktleWebCollectWork, Trunkフォルダパス + "\\" + SqlFolder_UnikktleWebCollectWork_SP);

            // Grant更新　UnikktleCmn

            // Grant更新　UnikktleWeb

            // Grant更新　UnikktlePayPalListen
        }

        public static void Exec_160_16_75_102(string Trunkフォルダパス)
        {
            // UnikktleCmn
            ExecSQL_ParentFolder(ConnectionString_160_16_75_102_UnikktleCmn, Trunkフォルダパス + "\\" + SqlFolder_UnikktleCmn_SP);
            ExecSQL_ParentFolder(ConnectionString_160_16_75_102_UnikktleCmn, Trunkフォルダパス + "\\" + SqlFolder_UnikktleCmn_Func);

            // UnikktleCollect

            // UnikktlePayPalListen
            ExecSQL_ParentFolder(ConnectionString_160_16_75_102_UnikktlePayPalListen, Trunkフォルダパス + "\\" + SqlFolder_UnikktlePayPalListen_SP);

            // UnikktleWeb
            ExecSQL_ParentFolder(ConnectionString_160_16_75_102_UnikktleWeb, Trunkフォルダパス + "\\" + SqlFolder_UnikktleWeb_SP);
            ExecSQL_ParentFolder(ConnectionString_160_16_75_102_UnikktleWeb, Trunkフォルダパス + "\\" + SqlFolder_UnikktleWeb_Func);

            // UnikktleWebCollectWork
            ExecSQL_ParentFolder(ConnectionString_160_16_75_102_UnikktleWebCollectWork, Trunkフォルダパス + "\\" + SqlFolder_UnikktleWebCollectWork_SP);

            // Grant更新　UnikktleCmn
            ExecSQL_File(ConnectionString_160_16_75_102_UnikktleCmn, Trunkフォルダパス + "\\" + File_UnikktleCmn_Grant);

            // Grant更新　UnikktleWeb
            ExecSQL_File(ConnectionString_160_16_75_102_UnikktlePayPalListen, Trunkフォルダパス + "\\" + File_UnikktleWeb_Grant);

            // Grant更新　UnikktlePayPalListen
            ExecSQL_File(ConnectionString_160_16_75_102_UnikktlePayPalListen, Trunkフォルダパス + "\\" + File_UnikktlePayPalListen_Grant);
        }

        private static void ExecSQL_ParentFolder(string connectionString, string parentFolder)
        {
            _ConnectionString = connectionString;

            using (var cn = new SqlConnection(connectionString))
            {
                cn.Open();

                var folders = Directory.GetDirectories(parentFolder, "*");

                foreach (var folder in folders)
                {
                    var files = Directory.GetFiles(folder, "*");
                    foreach (string file in files)
                    {
                        ExecSQL(cn, file);
                    }
                }
            }
        }

        private static void ExecSQL_Folder(string connectionString, string folder)
        {
            _ConnectionString = connectionString;

            using (var cn = new SqlConnection(connectionString))
            {
                cn.Open();

                var files = Directory.GetFiles(folder, "*");
                foreach (string file in files)
                {
                    ExecSQL(cn, file);
                }
            }
        }

        private static void ExecSQL_File(string connectionString, string file)
        {
            _ConnectionString = connectionString;

            using (var cn = new SqlConnection(connectionString))
            {
                cn.Open();

                ExecSQL(cn, file);
            }
        }

        private static void ExecSQL(SqlConnection cn, string file)
        {
            _SqlFilePath = file;

            var sqlLines = File.ReadAllLines(file, Encoding.GetEncoding("shift_jis"));

            _Sql = "";
            foreach (var line in sqlLines)
            {
                if (line.Trim().ToUpper() == "GO")
                {
                    (new SqlCommand(_Sql, cn)).ExecuteNonQuery();
                    _Sql = "";
                }
                else
                {
                    _Sql += line + "\r\n";
                }
            }

            if (!string.IsNullOrEmpty(_Sql))
            {
                (new SqlCommand(_Sql, cn)).ExecuteNonQuery();
            }
        }

    }
}
