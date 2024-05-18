using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Logging;

namespace Common
{
    public static class Mail
    {
        public static void SendMailAndLog(string subject, Exception ex)
        {
            ログ.ログ書き出し(ex);

            SendMail(subject, ex.Message + "\r\n" + ex.StackTrace);
        }

        public static void SendMailAndLogErr(string subject, string body)
        {
            ログ.ログ書き出し(subject + "\r\n" + body);

            SendMail(subject, body);
        }

        public static void SendMailAndLogErr(string subject, Exception ex)
        {
            SendMailAndLog("エラー" + subject, ex);
        }

        public static void SendMail(string subject, string body)
        {
            string id = "xxx@xxx.com";
            string pass = "xxx";
            string fromEmail = "xxx@xxx.com";
            string toEmail = "xxx@xxx.com";

            var smtp = new System.Net.Mail.SmtpClient();
            smtp.Host = "smtp.xxx.com";
            smtp.Port = 587;
            smtp.Credentials = new NetworkCredential(id, pass);
            smtp.EnableSsl = true;

            var msg = new System.Net.Mail.MailMessage(fromEmail, toEmail, "[" + Dns.GetHostName() + "] Unikktle " + subject, body);
            smtp.Send(msg);
        }

    }
}
