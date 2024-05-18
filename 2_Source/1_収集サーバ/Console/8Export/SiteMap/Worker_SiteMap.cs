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
using _8Export.Common;


namespace _8Export.SiteMap
{
    public static class Worker_SiteMap
    {
        public static void Exec_SiteMap()
        {
            ログ.ログ書き出し("[SiteMap] 開始");

            ログ.ログ書き出し("[SiteMap] outputフォルダ再作成");
            if (Directory.Exists(AppSetting.OutputFolder_SiteMap))
            {
                Directory.Delete(AppSetting.OutputFolder_SiteMap, true);
            }
            Directory.CreateDirectory(AppSetting.OutputFolder_SiteMap);
            Directory.CreateDirectory(AppSetting.OutputFolder_SiteMap_Bing);
            Directory.CreateDirectory(AppSetting.OutputFolder_SiteMap_Google);

            SiteMap_Bing.ExecuteCreateFile();
            SiteMap_Google.ExecuteCreateFile();

            Ssh_SiteMap.FileUpload_SiteMap(Ssh.ConnNfo);

            // sudo mv が安定しない。サーバのシェル実行に任せる。
            //Ssh.MvFile_SiteMapXml();

            ログ.ログ書き出し("[SiteMap] 終了");
        }
    }
}
