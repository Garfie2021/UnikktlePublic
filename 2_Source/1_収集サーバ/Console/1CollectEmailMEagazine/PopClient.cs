using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Collections.Generic;
using OpenPop.Mime;
using OpenPop.Pop3;
using Message = OpenPop.Mime.Message;

namespace CollectEmailMEagazine
{
    /// <summary>TcpClientを継承してSSL接続とPOPコマンドの送受信に最適化したクライアント。</summary>
    class PopClient : TcpClient
    {
        private Stream stream;
        private string host;
        private byte[] receiveBuffer = new byte[1024];

        public PopClient(string host, int port)
          : base(host, port)
        {
            this.host = host;
            this.stream = GetStream();
        }

        /// <summary>現在の接続をSSLにアップグレードする。</summary>
        public void UpgradeToSsl()
        {
            var sslStream = new SslStream(stream, false, ValidateRemoteCertificate);

            sslStream.AuthenticateAsClient(host);

            stream = sslStream;
        }

        /// <summary>サーバー証明書の検証を行う。</summary>
        private static bool ValidateRemoteCertificate(object sender,
            X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            // 証明書の検証を省略したい場合は、常にtrueを返す
            //return true;

            if (sslPolicyErrors == SslPolicyErrors.None)
            {
                return true;
            }
            else
            {
                // エラーがあれば標準エラーに表示
                Console.Error.WriteLine(sslPolicyErrors);
                return false;
            }
        }

        /// <summary>POPコマンドを送信する。</summary>
        public void Send(string command)
        {
            var commandBytes = Encoding.ASCII.GetBytes(command);

            stream.Write(commandBytes, 0, commandBytes.Length);

            // 送信した内容をコンソールに表示
            Console.Write("C:\t{0}", command);
        }

        /// <summary>POPレスポンスを受信する。</summary>
        public string Receive(bool expectMultiline)
        {
            var sb = new StringBuilder();

            for (; ; )
            {
                var len = stream.Read(receiveBuffer, 0, receiveBuffer.Length);

                sb.Append(Encoding.ASCII.GetString(receiveBuffer, 0, len));

                // 読み取り可能なデータがある場合はさらに受信を続ける
                if (0 < Available)
                    continue;

                if (expectMultiline)
                {
                    // レスポンスが複数行の場合は、CRLF.CRLFで終端されるまで受信した時点で受信を終了する
                    if (5 <= sb.Length && sb.ToString(sb.Length - 5, 5) == "\r\n.\r\n")
                        break;
                }
                else
                {
                    // レスポンスが一行の場合は、CRLFで終端されるまで受信した時点で受信を終了する
                    if (2 <= sb.Length && sb[sb.Length - 2] == '\r' && sb[sb.Length - 1] == '\n')
                        break;
                }
            }

            var response = sb.ToString();

            // 受信した内容を整形してコンソールに表示
            Console.Write("S:\t{0}", sb.Replace("\r\n", "\r\n\t").ToString(0, sb.Length - 1));

            return response;
        }
    }

}
