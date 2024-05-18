using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using 定数;
using Common;
using DB;
using Logging;
using AppDirectory;
using _8Export;
using _8Export.Common;


namespace UnitTest_x64
{
    [TestClass]
    public class SSHFileUploadTest
    {
        // SSHでコマンド実行するテスト。
        [TestMethod]
        public void TestMethod2()
        {
            try
            {
                App.Initialize(@"D:\work\5_Unikktle\trunk\2_Source\1_収集サーバ\Console\config\appsettings.json",
                    typeof(Program).Namespace);

                Ssh.Initialize(AppSetting.SSH_HostName, int.Parse(AppSetting.SSH_Port),
                    AppSetting.SSH_UserName, AppSetting.SSH_KeyFile, AppSetting.SSH_PassPhrase);

                //Ssh.ExecSshCommand();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        //// AppConfigクラスをシリアライズして、ファイル保存するサンプルプログラム。単体テストではない。
        //[TestMethod]
        //public void TestMethod2()
        //{
        //    try
        //    {
        //        var filePath = @"D:\Unikktele\Work\appConfig.json";
        //        var appConfig = new AppData();

        //        // 初期データ書き込み
        //        var jsonOut = JsonUtility.Serialize(appConfig);
        //        File.WriteAllText(filePath, jsonOut, Encoding.UTF8);

        //        // 初期データ書き込み
        //        var str = File.ReadAllText(filePath, Encoding.UTF8);
        //        var jsonIn = JsonUtility.Deserialize<AppData>(str);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        throw;
        //    }
        //}

        // SFTPでファイルをアップロードするテスト。
        [TestMethod]
        public void TestMethod1()
        {
            try
            {
                App.Initialize(@"D:\work\5_Unikktle\trunk\2_Source\1_収集サーバ\Console\config\appsettings.json", 
                    typeof(Program).Namespace);

                Ssh.Initialize(AppSetting.SSH_HostName, int.Parse(AppSetting.SSH_Port),
                    AppSetting.SSH_UserName, AppSetting.SSH_KeyFile, AppSetting.SSH_PassPhrase);

                //Ssh.Execute();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
