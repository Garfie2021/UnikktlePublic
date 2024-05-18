using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Renci.SshNet;
using 定数;
using DB;
using Logging;
using AppDirectory;
using Common;
using System.Text.RegularExpressions;

namespace _8Export.Common
{
    public static class Ssh
    {
        // 接続情報
        public static ConnectionInfo ConnNfo { set; get; }

        // 接続ホスト名
        private static string HostName { set; get; }

        // ポート
        private static Int32 Port { set; get; }

        // ユーザー名
        private static string UserName { set; get; }
        //// パスワード
        //private static string Password { set; get; }

        // コンストラクタ
        public static void Initialize(string hostName, int port, 
            string userName, string keyFile, string passPhrase)
        {
            //HostName = "xxx";             // 接続先ホスト名
            //Port = xxx;                       // ポート
            //UserName = "unikktleU";     // ユーザー名
            ////Password = "xxx";                  // パスワード
            //string KeyFile = @"C:\Users\xxx\Documents\id_rsa.openssh";     // 秘密鍵
            //string PassPhrase = "xxx";                                     // パスフレーズ

            HostName = hostName;
            Port = port;
            UserName = userName;

            // パスワード認証
            //var _PassAuth = new PasswordAuthenticationMethod(UserName, Password);

            // 秘密鍵認証
            var _PrivateKey = new PrivateKeyAuthenticationMethod(UserName, new PrivateKeyFile[]{
                        new PrivateKeyFile(keyFile, passPhrase)
                    });

            // 接続情報の生成
            ConnNfo = new ConnectionInfo(HostName, Port, UserName,
                new AuthenticationMethod[]{
                    //_PassAuth,          // パスワード認証
                    _PrivateKey,        // 秘密鍵認証
                }
            );

            ログ.ログ書き出し($"SftpClient Host:{ConnNfo.Host}  Port:{ConnNfo.Port}  Username:{ConnNfo.Username}");

        }



        public static void Command(SshClient sshClient, string command)
        {
            using (var cmd = sshClient.CreateCommand(command))
            {
                ログ.ログ書き出し("Command開始 : " + command);
                ログ.ログ書き出し(cmd.Execute());
                ログ.ログ書き出し("ExitStatus : " + cmd.ExitStatus);
                ログ.ログ書き出し("Result : " + cmd.Result);
            }
        }

        public static void Command_Sudo(SshClient sshClient, string command)
        {
            var promptRegex = new Regex(@"\][#$>]"); // regular expression for matching terminal prompt
            var modes = new Dictionary<Renci.SshNet.Common.TerminalModes, uint>();
            using (var stream = sshClient.CreateShellStream("xterm", 255, 50, 800, 600, 1024, modes))
            {
                ログ.ログ書き出し("Command開始 : " + command);
                stream.Write(command + "\n");
                stream.Expect("パスワード");
                stream.Write("xxx\n"); // sudo password

                //var output = stream.Expect(promptRegex);
                var result = stream.Read();

                ログ.ログ書き出し("Result : " + result);
            }
        }

        // ファイル表示
        public static void printFiles(
            SftpClient _sftp,   // sftpクライアント
            string _Path        // パス
            )
        {
            // 指定パスを調べる
            foreach (var file in _sftp.ListDirectory(_Path))
            {
                if (file.Name.StartsWith(".")) continue;

                if (file.IsDirectory)
                {
                    // ディレクトリなら再帰して調べる
                    printFiles(_sftp, file.FullName);
                }
                else
                {
                    // 表示
                    Console.WriteLine($"{file.FullName}\t\t{file.LastAccessTime}\t{file.LastWriteTime}");
                }
            }
        }

        // 指定テキストファイルの表示
        public static void printTxtFile(
            SftpClient _sftp,       // sftpクライアント
            string _FilePath        // ファイルパス
            )
        {
            var _CurDir = Path.GetDirectoryName(_FilePath).Substring(1);
            var _FileName = Path.GetFileName(_FilePath);

            // カレントディレクトリ変更
            _sftp.ChangeDirectory(_CurDir);

            foreach (var file in _sftp.ListDirectory("./"))
            {
                if (file.IsDirectory) continue;
                if (file.Name != _FileName) continue;

                // 読み込み
                Int64 _Size = file.Length;
                var _Buf = new byte[_Size];
                using (var _St = new MemoryStream(_Buf, 0, (int)_Size))
                {
                    _sftp.DownloadFile(file.FullName, _St);
                }

                // SJIS変換
                string _str = Encoding.GetEncoding(932).GetString(_Buf);

                // 内容表示
                Console.WriteLine();
                Console.WriteLine($"------------------{file.Name}");
                Console.WriteLine($"{_str}");
                Console.WriteLine("------------------");
            }
        }

        // ファイルのアップロード
        public static void uploadFile(
            SftpClient _sftp,       // sftpクライアント
            string _UploadPath,    // アップロードパス
            string _UploadFile     // アップロードファイル名
            )
        {
            // カレントディレクトリ変更
            _sftp.ChangeDirectory(_UploadPath);

            // アップロード先パス
            var _RemotePath = _UploadPath + "/" + Path.GetFileName(_UploadFile);

            using (var _uploadStream = File.OpenRead(_UploadFile))
            {
                _sftp.UploadFile(_uploadStream, _RemotePath, true);
            }
        }
    }
}
