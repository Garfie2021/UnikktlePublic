using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using 定数;


namespace DB
{
    public static class MorphologicalAnalysisDB
    {
        private static Hashtable Cache_CollectTarget = new Hashtable();
        private static Hashtable Cache_GetKeywordNo = new Hashtable();

        private static SqlCommand cmd_GetCollectTargetNo;
        private static SqlCommand cmd_GetKeywordNo;
        private static SqlCommand cmd_CulcKeywordAssociation;

        static MorphologicalAnalysisDB()
        {
            cmd_GetCollectTargetNo = new SqlCommand("mst.spGetCollectTargetMail_No");
            cmd_GetCollectTargetNo.CommandType = CommandType.StoredProcedure;
            cmd_GetCollectTargetNo.Parameters.Add(new SqlParameter("名称", SqlDbType.NVarChar));
            cmd_GetCollectTargetNo.Parameters.Add(new SqlParameter("From_MailAddress", SqlDbType.NVarChar));
            cmd_GetCollectTargetNo.Parameters.Add(new SqlParameter("No", SqlDbType.Int));
            cmd_GetCollectTargetNo.Parameters["名称"].Direction = ParameterDirection.Input;
            cmd_GetCollectTargetNo.Parameters["From_MailAddress"].Direction = ParameterDirection.Input;
            cmd_GetCollectTargetNo.Parameters["No"].Direction = ParameterDirection.Output;

            cmd_GetKeywordNo = new SqlCommand("mst.spGetKeywordNo");
            cmd_GetKeywordNo.CommandType = CommandType.StoredProcedure;
            cmd_GetKeywordNo.Parameters.Add(new SqlParameter("Keyword", SqlDbType.NVarChar));
            cmd_GetKeywordNo.Parameters.Add(new SqlParameter("No", SqlDbType.BigInt));
            cmd_GetKeywordNo.Parameters["Keyword"].Direction = ParameterDirection.Input;
            cmd_GetKeywordNo.Parameters["No"].Direction = ParameterDirection.Output;

            cmd_CulcKeywordAssociation = new SqlCommand("mst.spCulcKeywordAssociation");
            cmd_CulcKeywordAssociation.CommandType = CommandType.StoredProcedure;
        }

        public static int GetCollectTargetMail_No(SqlConnection cn, CollectMailRow mail)
        {
            try
            {
                if (Cache_CollectTarget.ContainsKey(mail.FromMailAddress))
                {
                    // キャッシュにあればDB問い合わせしない

                    return (int)Cache_CollectTarget[mail.FromMailAddress];
                }

                // キャッシュに無い場合はDBに新規登録してNo取得

                cmd_GetCollectTargetNo.Connection = cn;
                cmd_GetCollectTargetNo.Parameters["名称"].Value = mail.FromDisplayName;
                cmd_GetCollectTargetNo.Parameters["From_MailAddress"].Value = mail.FromMailAddress;

                cmd_GetCollectTargetNo.ExecuteNonQuery();

                var No = (int)cmd_GetCollectTargetNo.Parameters["No"].Value;

                Cache_CollectTarget.Add(mail.FromMailAddress, No);

                return No;
            }
            catch (Exception ex)
            {
                //Mail.SendMail(ex, "cDB", "new");
                throw;
            }
        }

        public static long GetKeywordNo(SqlConnection cn, string keyword)
        {
            try
            {
                if (Cache_GetKeywordNo.ContainsKey(keyword))
                {
                    // キャッシュにあればDB問い合わせしない

                    return (int)Cache_GetKeywordNo[keyword];
                }

                // キャッシュに無い場合はDBに新規登録してNo取得

                cmd_GetKeywordNo.Connection = cn;
                cmd_GetKeywordNo.Parameters["Keyword"].Value = keyword;

                cmd_GetKeywordNo.ExecuteNonQuery();

                var No = (int)cmd_GetKeywordNo.Parameters["No"].Value;

                Cache_GetKeywordNo.Add(keyword, No);

                return No;
            }
            catch (Exception ex)
            {
                //Mail.SendMail(ex, "cDB", "new");
                throw;
            }
        }

        public static void CulcKeywordAssociation(SqlConnection cn)
        {
            try
            {
                cmd_CulcKeywordAssociation.Connection = cn;

                cmd_CulcKeywordAssociation.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                //Mail.SendMail(ex, "cDB", "new");
                throw;
            }
        }


        //SQLエラーが発生する文字列に対応する。
        public static void ReplaceCommandText(
            string strHyousoukei, string cmdTxt)
        {
            try
            {
                //半角シングルコーテーション対応
                if (strHyousoukei == "'")
                {
                    cmdTxt += strHyousoukei + "''";
                }
                else
                {
                    cmdTxt += strHyousoukei + "'";
                }
            }
            catch (Exception ex)
            {
                //Mail.SendMail(ex, "cDB", "ReplaceCommandText");
            }
        }

        public static SqlConnection GetConnction()
        {
            try
            {
                //コネクションストリングを用意
                var UserID = "Admin";
                var Password = "";
                string cnctStr;
                var MDBFile = @"C:\Program Files\NXTM\KeywordAnalyzer\base.mdb";
                cnctStr = "Provider='Microsoft.Jet.OLEDB.4.0';";
                cnctStr += "Data Source='" + MDBFile + "';";
                cnctStr += "User ID=" + UserID + ";";
                cnctStr += "Jet OLEDB:Database Password=" + Password;

                //データベースとの接続関係を設定
                var db = new SqlConnection();
                db.ConnectionString = cnctStr;
                return db;
            }
            catch (Exception ex)
            {
                //Mail.SendMail(ex, "cDB", "new");
            }

            return null;
        }


        //環境設定　取得
        public static void Get_Tbl101_Envelonment(SqlConnection cn,
            ref bool bErrMail,
            ref bool bLimitDirSizeNotice,
            ref int intLimitDirSize,
            ref string strBeforeFolder,
            ref int intBeforeLearningArea)
        {
            try
            {
                //SELECTコマンドを作成
                var CommandText = "SELECT ";
                CommandText += " ID, Value ";
                CommandText += "FROM Tbl101_Envelonment ";

                var cmd = new SqlCommand(CommandText, cn);
                cmd.CommandType = CommandType.Text;

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        //エラーメール設定
                        if (reader["ID"] == "2")
                        {
                            if (reader["Value"] == "0")
                                bErrMail = false;
                            else
                                bErrMail = true;
                        }

                        //リミットサイズ通知
                        if (reader["ID"] == "4")
                        {
                            if (reader["Value"] == "0")
                                bLimitDirSizeNotice = false;
                            else
                                bLimitDirSizeNotice = true;
                        }

                        //バックアップディレクトリのリミットサイズ（メガ）設定
                        if (reader["ID"] == "3")
                            intLimitDirSize = (int)reader["Value"] / (1024 * 1024);

                        //前回の設定値。
                        //取り込みフォルダ。
                        if (reader["ID"] == "5")
                            strBeforeFolder = reader["Value"].ToString();

                        //前回の設定値
                        //学習領域を指定コンボボックスのインデックス。
                        if (reader["ID"] == "6")
                            intBeforeLearningArea = (int)reader["Value"];

                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                //Mail.SendMail(ex, "cDB", "Set_Tbl201_File_Add");
            }
        }



        //****************************************************************************************************
        //***    INSERT系     ********************************************************************************
        //****************************************************************************************************


        //TF 追加
        public static void Set_Tbl201_File_Add(SqlConnection cn,
            string strDFID,
            string strFileName,
            string strFilePath,
            ref string strTFID)
        {
            try
            {

                //****************************************************************************************************
                //登録しIDを取得

                var CommandText = "INSERT INTO Tbl201_File (DF_ID, FileName, FilePath) VALUES (";
                CommandText += strDFID + ", ";
                CommandText += " '" + strFileName + "' ,";
                CommandText += " '" + strFilePath + "' )";

                var cmd = new SqlCommand(CommandText, cn);
                cmd.CommandType = CommandType.Text;

                cmd.ExecuteNonQuery();

                CommandText = "SELECT Top 1 TF_ID FROM Tbl201_File";
                CommandText += " WHERE DF_ID = " + strDFID;
                CommandText += " AND   FileName = '" + strFileName + "'";
                CommandText += " AND   FilePath = '" + strFilePath + "'";
                CommandText += " Order by TF_ID DESC ";

                cmd = new SqlCommand(CommandText, cn);
                cmd.CommandType = CommandType.Text;

                using (var reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    strTFID = reader[0].ToString();

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                //Mail.SendMail(ex, "cDB", "Set_Tbl201_File_Add");
            }
        }


        //頻度(DF*語彙) 追加＆インクリメント
        public static void Set_Tbl203_KeywordFrequencyDF_Add(SqlConnection cn,
            string strDF_ID,
            string strGoiID)
        {
            try
            {

                //****************************************************************************************************
                //既に登録されているか調べる

                var CommandText = "SELECT Frequency FROM Tbl203_KeywordFrequencyDF ";
                CommandText += " WHERE DF_ID = " + strDF_ID;
                CommandText += " AND KeywordID = " + strGoiID;

                var cmd = new SqlCommand(CommandText, cn);
                cmd.CommandType = CommandType.Text;

                using (var reader = cmd.ExecuteReader())
                {
                    string strFrequency; // 語彙出現数
                    if (reader.HasRows == true)        //レコードがあるかチェック
                    {
                        //******　　既に登録されている語彙であれば、出現数を取得し、インクリメントする    ************
                        reader.Read();
                        strFrequency = reader[0].ToString();
                        reader.Close();

                        CommandText = "UPDATE Tbl203_KeywordFrequencyDF SET Frequency = Frequency + 1";
                        CommandText += " WHERE DF_ID = " + strDF_ID;
                        CommandText += " AND KeywordID = " + strGoiID;

                        var cmd2 = new SqlCommand(CommandText, cn);
                        cmd2.CommandType = CommandType.Text;
                        cmd2.ExecuteNonQuery();

                    }
                    else
                    {
                        //******　　登録されていない語彙であれば、出現数＝１で登録    ************

                        CommandText = "INSERT INTO Tbl203_KeywordFrequencyDF (DF_ID, KeywordID, Frequency) VALUES (";
                        CommandText += strDF_ID + " , " + strGoiID + ", 1 ) ";

                        var cmd2 = new SqlCommand(CommandText, cn);
                        cmd2.CommandType = CommandType.Text;
                        cmd2.ExecuteNonQuery();
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                //Mail.SendMail(ex, "cDB", "Set_Tbl203_KeywordFrequencyDF_Add");
            }

        }

        //頻度(TF*語彙) 追加＆インクリメント
        public static void Set_Tbl203_KeywordFrequencyDF_Add(SqlConnection cn,
            string strDF_ID,
            string strTF_ID,
            string strGoiID)
        {
            try
            {

                //****************************************************************************************************
                //既に登録されているか調べる

                var CommandText = "SELECT Frequency FROM Tbl204_KeywordFrequencyTF ";
                CommandText += " WHERE DF_ID = " + strDF_ID;
                CommandText += " AND TF_ID = " + strTF_ID;
                CommandText += " AND KeywordID = " + strGoiID;

                var cmd = new SqlCommand(CommandText, cn);
                cmd.CommandType = CommandType.Text;


                using (var reader = cmd.ExecuteReader())
                {
                    string strFrequency; // 語彙出現数
                    if (reader.HasRows == true)        //レコードがあるかチェック
                    {
                        //******　　既に登録されている語彙であれば、出現数を取得し、インクリメントする    ************
                        reader.Read();
                        strFrequency = reader[0].ToString();
                        reader.Close();

                        CommandText = "UPDATE Tbl204_KeywordFrequencyTF SET Frequency = Frequency + 1";
                        CommandText += " WHERE DF_ID = " + strDF_ID;
                        CommandText += " AND TF_ID = " + strTF_ID;
                        CommandText += " AND KeywordID = " + strGoiID;

                        var cmd2 = new SqlCommand(CommandText, cn);
                        cmd2.CommandType = CommandType.Text;
                        cmd2.ExecuteNonQuery();
                    }
                    else
                    {
                        //******　　登録されていない語彙であれば、出現数＝１で登録    ************
                        reader.Close();

                        CommandText = "INSERT INTO Tbl204_KeywordFrequencyTF (DF_ID, TF_ID, KeywordID, Frequency) VALUES (";
                        CommandText += strDF_ID + " , " + strTF_ID + " , " + strGoiID + ", 1 ) ";

                        var cmd2 = new SqlCommand(CommandText, cn);
                        cmd2.CommandType = CommandType.Text;
                        cmd2.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                //Mail.SendMail(ex, "cDB", "Set_Tbl203_KeywordFrequencyDF_Add");
            }
        }

        //****************************************************************************************************
        //***    UPDATE系     ********************************************************************************
        //****************************************************************************************************

        //環境設定　更新
        public static void Renew_Tbl201_File(SqlConnection cn,
            bool bErrMail,
            bool bLimitDirSizeNotice,
            int intLimitDirSize)
        {
            try
            {
                //****************************************************************************************************
                //エラーメール設定

                var CommandText = "UPDATE  Tbl101_Envelonment SET [value] = ";

                if (bErrMail == false)
                    CommandText += "'0' ";
                else
                    CommandText += "'1' ";

                CommandText += "WHERE ID = 2 ";

                var cmd = new SqlCommand(CommandText, cn);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();

                //****************************************************************************************************
                //リミットサイズ通知設定

                CommandText = "UPDATE  Tbl101_Envelonment SET [value] = ";

                if (bErrMail == false)
                    CommandText += "'0' ";
                else
                    CommandText += "'1' ";

                CommandText += "WHERE ID = 4 ";

                cmd = new SqlCommand(CommandText, cn);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();

                //****************************************************************************************************
                //バックアップディレクトリのリミットサイズ（メガ）設定

                CommandText = "UPDATE  Tbl101_Envelonment SET [value] = ";
                CommandText += "'" + (intLimitDirSize * 1024 * 1024).ToString() + "' ";
                CommandText += "WHERE ID = 3 ";

                cmd = new SqlCommand(CommandText, cn);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                //Mail.SendMail(ex, "cDB", "Set_Tbl201_File_Add");
            }
        }


        //環境設定　更新
        public static void Renew_Tbl201_File(SqlConnection cn,
            string strBeforeFolder,
            int intBeforeLearningArea)
        {
            try
            {
                //****************************************************************************************************
                //前回の設定値。
                //取り込みフォルダ。

                var CommandText = "UPDATE  Tbl101_Envelonment SET [value] = ";
                CommandText += "'" + strBeforeFolder + "' ";
                CommandText += "WHERE ID = 5 ";

                var cmd = new SqlCommand(CommandText, cn);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();

                //****************************************************************************************************
                //前回の設定値
                //学習領域を指定コンボボックスのインデックス。

                CommandText = "UPDATE  Tbl101_Envelonment SET [value] = ";
                CommandText += "'" + intBeforeLearningArea.ToString() + "' ";
                CommandText += "WHERE ID = 6 ";

                cmd = new SqlCommand(CommandText, cn);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                //Mail.SendMail(ex, "cDB", "Renew_Tbl201_File");
            }
        }

        //TF・IDFを計算
        public static void Renew_TFIDF(SqlConnection cn, string strDF_ID)
        {
            try
            {

                //****************************************************************************************************
                //総ドキュメント数を取得する。

                int intAllDocumentNum = 0;

                //SELECTコマンドを作成
                var CommandText = "SELECT ";
                CommandText += " Count(TF_ID) as AllDocumentNum ";
                CommandText += "FROM Tbl201_File ";
                CommandText += "WHERE";
                CommandText += " (DF_ID = " + strDF_ID + " ) ";

                //データリーダを用意
                var cmd = new SqlCommand(CommandText, cn);
                cmd.CommandType = CommandType.Text;

                using (var reader = cmd.ExecuteReader())
                {
                    //レコードがあるかチェック
                    if (reader.HasRows == true)
                    {
                        //データリーダを次の行に位置付ける。
                        //この場合、列タイトル行からレコード行へ
                        reader.Read();

                        intAllDocumentNum = (int)reader["AllDocumentNum"];
                    }

                    reader.Close();
                }


                //****************************************************************************************************
                //TF値、語彙が出現するドキュメント数を取得し、Tbl204_KeywordFrequencyTFテーブルを更新して行く。

                int intTF_Num;
                int intGoiWoHukumuDocumentNum;

                //SELECTコマンドを作成　語彙ＩＤとＴＦを取得

                CommandText = "SELECT ";
                CommandText += " TF_ID , ";
                CommandText += " KeywordID , ";
                CommandText += " Frequency ";
                CommandText += "FROM Tbl204_KeywordFrequencyTF ";
                CommandText += "WHERE";
                CommandText += " (DF_ID = " + strDF_ID + " ) ";

                cmd = new SqlCommand(CommandText, cn);
                cmd.CommandType = CommandType.Text;


                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows == true)
                    {
                        //データリーダを次の行に位置付ける。
                        //この場合、列タイトル行からレコード行へ
                        reader.Read();

                        // 取得できたレコード数分ループし、TF*IDFを計算していく。

                        intTF_Num = (int)reader["Frequency"];

                        //SELECTコマンドを作成　語彙が出現するドキュメント数を取得
                        CommandText = "SELECT ";
                        CommandText += " Frequency ";
                        CommandText += "FROM Tbl203_KeywordFrequencyDF ";
                        CommandText += "WHERE";
                        CommandText += " (DF_ID = " + strDF_ID + " ) ";
                        CommandText += " AND (KeywordID = " + reader["KeywordID"] + " ) ";

                        var cmd2 = new SqlCommand(CommandText, cn);
                        cmd2.CommandType = CommandType.Text;

                        using (var reader2 = cmd2.ExecuteReader())
                        {
                            //データリーダを次の行に位置付ける。
                            //この場合、列タイトル行からレコード行へ
                            reader2.Read();

                            intGoiWoHukumuDocumentNum = (int)reader2["Frequency"];

                            CommandText = "UPDATE  Tbl204_KeywordFrequencyTF SET TFIDF = ";
                            CommandText += String.Format("{0:###.#####}", intTF_Num * (intAllDocumentNum / intGoiWoHukumuDocumentNum));
                            CommandText += " WHERE ";
                            CommandText += " (DF_ID = " + strDF_ID + " ) ";
                            CommandText += " AND (TF_ID = " + reader["TF_ID"] + " ) ";
                            CommandText += " AND (KeywordID = " + reader["KeywordID"] + " ) ";

                            var cmd3 = new SqlCommand(CommandText, cn);
                            cmd3.CommandType = CommandType.Text;
                            cmd3.ExecuteNonQuery();
                        }
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                //Mail.SendMail(ex, "cDB", "Set_Tbl201_File_Add");
            }
        }


        //****************************************************************************************************
        //***    DELETE系     ********************************************************************************
        //****************************************************************************************************


        //ﾌｧｲﾙ（TF） クリア
        public static void Tbl201_File_Del(SqlConnection cn,
            string strDF_ID,
            string strTF_ID = "")
        {
            try
            {
                var CommandText = "DELETE FROM Tbl201_File";
                CommandText += " WHERE DF_ID = " + strDF_ID;

                if (strTF_ID != "")
                    CommandText += " AND TF_ID = " + strTF_ID;

                var cmd = new SqlCommand(CommandText, cn);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                //Mail.SendMail(ex, "cDB", "Tbl201_File_Del");
            }
        }


        //頻度(DF*語彙) クリア
        public static void Tbl203_KeywordFrequencyDF_Del(SqlConnection cn, string strDF_ID)
        {
            try
            {
                var CommandText = "DELETE FROM Tbl203_KeywordFrequencyDF";
                CommandText += " WHERE DF_ID = " + strDF_ID;

                var cmd = new SqlCommand(CommandText, cn);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                //Mail.SendMail(ex, "cDB", "Tbl203_KeywordFrequencyDF_Del");
            }
        }


        //頻度(TF*語彙) クリア
        public static void Tbl204_KeywordFrequencyTF_Del(SqlConnection cn,
            string strDF_ID,
            string strTF_ID = "")
        {
            try
            {
                var CommandText = "DELETE FROM Tbl204_KeywordFrequencyTF";
                CommandText += " WHERE DF_ID = " + strDF_ID;

                if (strTF_ID != "")
                    CommandText += " AND TF_ID = " + strTF_ID;

                var cmd = new SqlCommand(CommandText, cn);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                //Mail.SendMail(ex, "cDB", "Tbl204_KeywordFrequencyTF_Del");
            }

        }


        //語彙区分 オールクリア
        public static void Tbl102_KeywordClassification_Del(SqlConnection cn)
        {
            try
            {
                var CommandText = "DELETE FROM Tbl102_KeywordClassification";

                var cmd = new SqlCommand(CommandText, cn);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                //Mail.SendMail(ex, "cDB", "Tbl102_KeywordClassification_Del");
            }
        }


        //語彙 オールクリア
        public static void Tbl202_Keyword_Del(SqlConnection cn)
        {
            try
            {
                var CommandText = "DELETE FROM Tbl202_Keyword";

                var cmd = new SqlCommand(CommandText, cn);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                //Mail.SendMail(ex, "cDB", "Tbl202_Keyword_Del");
            }
        }





        private static void DB_TEST()
        {


            //コネクションストリングを用意
            //var UserID = "Admin"
            //var Password = ""
            //var cnctStr As String
            //var MDBFile = "C:\Program Files\NXTM\KeywordAnalyzer\base.mdb"
            //cnctStr = "Provider=""Microsoft.Jet.OLEDB.4.0"";"
            //cnctStr += "Data Source=""" & MDBFile + """;"
            //cnctStr += "User ID=" & UserID + ";"
            //cnctStr += "Jet OLEDB:Database Password=" & Password

            //'データベースとの接続関係を設定
            //var db = new SqlConnection
            //db.ConnectionString = cnctStr

            //'SELECTコマンドを作成
            //var cmnd = new System.Data.OleDb.OleDbCommand
            //cmnd.Connection = db
            //CommandText = "SELECT * FROM PurchaseHistory"

            //'データベースをオープン
            //db.Open()

            //'データリーダを用意
            //var reader As OleDbDataReader
            //reader = cmnd.ExecuteReader

            //'列総数を取得
            //var cn As Integer
            //cn = reader.FieldCount

            //'レコードがあるかチェック
            //if reader.HasRows = true

            //    //データリーダを次の行に位置付ける。
            //    //この場合、列タイトル行からレコード行へ
            //    reader.Read()

            //    //１行目２列目の値を取得
            //    var ss As String
            //    ss = reader[332].ToString()

            //End if

            //'データベースをクローズ
            //db.Close()


            //'データベースとの接続関係を設定
            //var db = new SqlConnection
            //db.ConnectionString = cnctStr

            //'INSERTコマンドを作成
            //var cmnd = new System.Data.OleDb.OleDbCommand
            //cmnd.Connection = db
            //CommandText = "INSERT INTO PurchaseHistory (intUserID, intProductID, dtPurchase) VALUES (2, 2, 2008/01/01)"

            //'データベースをオープン
            //db.Open()

            //'コマンドを実行
            //cmnd.ExecuteNonQuery()

            //'データベースをクローズ
            //db.Close()


            //'データベースとの接続関係を設定
            //var db = new SqlConnection
            //db.ConnectionString = cnctStr

            //'INSERTコマンドを作成
            //var cmnd = new System.Data.OleDb.OleDbCommand
            //cmnd.Connection = db
            //CommandText = "UPDATE PurchaseHistory SET intUserID=3 WHERE intUserID=2"

            //'データベースをオープン
            //db.Open()

            //'コマンドを実行
            //cmnd.ExecuteNonQuery()

            //'データベースをクローズ
            //db.Close()



            //'データベースとの接続関係を設定
            //var db = new SqlConnection
            //db.ConnectionString = cnctStr

            //'INSERTコマンドを作成
            //var cmnd = new System.Data.OleDb.OleDbCommand
            //cmnd.Connection = db
            //CommandText = "DELETE FROM PurchaseHistory"

            //'データベースをオープン
            //db.Open()

            //'コマンドを実行
            //cmnd.ExecuteNonQuery()

            //'データベースをクローズ
            //db.Close()


        }

        //private Function CreateSelectCommandFromID() As OleDbCommand
        //    var cmd = new OleDbCommand
        //    cmd.Connection = Me.Cn

        //    cmd.CommandText = "SELECT "
        //    cmd.CommandText += "    CustomerID, Name, Sex "
        //    cmd.CommandText += "FROM "
        //    cmd.CommandText += "    Customer "
        //    cmd.CommandText += "WHERE "
        //    cmd.CommandText += "    CustomerID = @CustomerID"

        //    cmd.Parameters.Add("@CustomerID", OleDbType.Integer, 0, "CustomerID")

        //    Return cmd
        //End Function
    }
}
