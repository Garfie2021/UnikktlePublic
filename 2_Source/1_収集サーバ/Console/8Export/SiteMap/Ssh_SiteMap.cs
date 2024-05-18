using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    public static class Ssh_SiteMap
    {
        public static void FileUpload_SiteMap(ConnectionInfo connNfo)
        {
            using (var sftp = new SftpClient(connNfo))
            {
                // 接続
                sftp.Connect();

                // 確認
                if (sftp.IsConnected)
                {
                    // 接続に成功
                    ログ.ログ書き出し("Connection success!");
                }
                else
                {
                    // 接続に失敗
                    ログ.ログ書き出し("Connection failed.");
                    return;
                }

                //using (var _uploadStream = File.OpenRead(AppSetting.BcpExportFilePath_hst_t6CollaborateKeyword))
                //{
                //    ログ.ログ書き出し($"SFTP UploadFile t6CollaborateKeyword 開始");

                //    ログ.ログ書き出し($"BcpExportFilePath:{AppSetting.BcpExportFilePath_hst_t6CollaborateKeyword}");
                //    sftp.UploadFile(_uploadStream, AppSetting.UploadFileName_hst_t6CollaborateKeyword, true);
                //}

                //using (var _uploadStream = File.OpenRead(AppSetting.BcpExportFilePath_mst_tKeyword))
                //{
                //    ログ.ログ書き出し($"SFTP UploadFile tKeyword 開始");

                //    ログ.ログ書き出し($"BcpExportFilePath:{AppSetting.BcpExportFilePath_mst_tKeyword}");
                //    sftp.UploadFile(_uploadStream, AppSetting.UploadFileName_mst_tKeyword, true);
                //}

                FileUpload(sftp, AppSetting.OutputFolder_SiteMap_Bing, "/home/unikktleImport/sitemap/bing");
                FileUpload(sftp, AppSetting.OutputFolder_SiteMap_Google, "/home/unikktleImport/sitemap/google");

                // 切断
                sftp.Disconnect();
            }
        }

        public static void FileUpload(SftpClient sftp, string siteMapOutputFolder, string uploadDirectory)
        {
            ログ.ログ書き出し($"SFTP UploadFile SiteMap 開始");
            ログ.ログ書き出し("siteMapOutputFolder : " + siteMapOutputFolder);
            ログ.ログ書き出し("uploadDirectory : " + uploadDirectory);


            sftp.ChangeDirectory(uploadDirectory);

            foreach (var file in Directory.GetFiles(siteMapOutputFolder, "*"))
            {
                var filename = file.Substring(file.LastIndexOf("\\") + "\\".Length);
                using (var _uploadStream = File.OpenRead(file))
                {
                    ログ.ログ書き出し($"UploadFileName : {file}");
                    sftp.UploadFile(_uploadStream, filename, true);
                }
            }
        }

        // sudo mv が安定しない。サーバのシェル実行に任せる。
        public static void MvFile_SiteMapXml()
        {
            ログ.ログ書き出し($"SSH mv SiteMap.xml 開始");

            using (var sshClient = new SshClient(Ssh.ConnNfo))
            {
                sshClient.Connect();

                // 接続確認
                if (sshClient.IsConnected)
                {
                    ログ.ログ書き出し("Connection success!");
                }
                else
                {
                    ログ.ログ書き出し("Connection failed.");
                    return;
                }

                //Command(sshClient, "echo -e 'xxx\\n' | sudo mv /home/unikktleImport/SiteMap.xml /var/aspnet/Unikktle/wwwroot/SiteMap.xml");
                Ssh.Command_Sudo(sshClient, AppSetting.Cmd_mvfile_SiteMap);

                sshClient.Disconnect();
            }
        }


    }
}
