using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;


namespace Unikktle.Common
{
    public static class MailNotify
    {
        public static void DebugMail(IEmailSender emailSender, string body)
        {
            // IEmailSenderを継承した、Unikktle.Services.EmailSenderクラスの
            // SendEmailAsync() メソッドを実行してる。
            emailSender.SendEmailAsync("xxx@xxx.com", "Unikktle web Debug", body);
        }

        public static void ExceptionMail(IEmailSender emailSender, string para, Exception ex)
        {
            // IEmailSenderを継承した、Unikktle.Services.EmailSenderクラスの
            // SendEmailAsync() メソッドを実行してる。
            emailSender.SendEmailAsync(
                "xxx@xxx.com",
                "[" + Dns.GetHostName() + "] Unikktle web exception",
                "[URL parameters] <br>" +
                (string.IsNullOrEmpty(para) ? "無し<br><br>" : para + "<br><br>") +
                "[Message] <br>" + 
                ex.Message + "<br><br>" +
                "[StackTrace] <br>" +
                ex.StackTrace);
        }

    }
}
