using System;
using System.IO;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using 定数;
using Common;
using DB;
using Logging;
using AppDirectory;
using _8Export;
using _8Export.Common;
using _8Export.SiteMap;


namespace UnitTest_x64
{
    [TestClass]
    public class SiteMapTest
    {
        [TestMethod]
        public void Test_Ssh_Mv()
        {
            try
            {
                App.Initialize(@"D:\work\5_Unikktle\trunk\2_Source\1_収集サーバ\Console\config\appsettings.json",
                    typeof(Program).Namespace);

                Ssh.Initialize(AppSetting.SSH_HostName, int.Parse(AppSetting.SSH_Port),
                    AppSetting.SSH_UserName, AppSetting.SSH_KeyFile, AppSetting.SSH_PassPhrase);

                Ssh_SiteMap.MvFile_SiteMapXml();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }


        [TestMethod]
        public void Test_SiteMap通し()
        {
            try
            {
                App.Initialize(@"D:\work\5_Unikktle\trunk\2_Source\1_収集サーバ\Console\config\appsettings.json",
                    typeof(Program).Namespace);

                Ssh.Initialize(AppSetting.SSH_HostName, int.Parse(AppSetting.SSH_Port),
                    AppSetting.SSH_UserName, AppSetting.SSH_KeyFile, AppSetting.SSH_PassPhrase);

                Worker_SiteMap.Exec_SiteMap();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

    }
}
