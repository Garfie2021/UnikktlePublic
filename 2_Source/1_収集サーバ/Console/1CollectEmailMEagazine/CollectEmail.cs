using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using OpenPop.Mime;
using OpenPop.Pop3;
using Message = OpenPop.Mime.Message;
using 定数;
using DB;
using AppDirectory;



namespace CollectEmailMEagazine
{
    public static class CollectEmail
    {
        public static bool 古いメール削除 = false;


        public static long CollectTargetNo(SqlConnection cn, List<CollectTargetRow> collectTargetList, CollectMailRow mail)
        {
            var collectTarget = collectTargetList.Where(x => x.From_MailAddress == mail.FromMailAddress);

            if (collectTarget.Count() > 0)
            {
                return collectTarget.First().No;
            }

            var target = new CollectTargetRow()
            {
                名称 = mail.FromDisplayName,
                From_MailAddress = mail.FromMailAddress,
            };

            // DB登録
            target.No = DB.Collect.SP_CollectTarget.Insert(cn, target);

            collectTargetList.Add(target);

            return target.No;
        }

        public static Pop3Client GetPop3Client()
        {
            var client = new Pop3Client();
            client.Connect(AppSetting.Mail_Host, AppSetting.Mail_Port, true);
            client.Authenticate(AppSetting.Mail_Username, AppSetting.Mail_Password);

            return client;
        }


        public static List<CollectMailRow> Exec()
        {
            ////////////////////////////////////////////////////////////////////////////////////
            // Todo 最後に正常終了したメールマガジンの受信日時より前のメールは削除する

            //pop3.DeleteMessageByMessageId(pop3Client, "1"); // MessageId

            ////////////////////////////////////////////////////////////////////////////////////
            // メール受信
            List<CollectMailRow> 受信MailList;
            using (Pop3Client pop3Client = GetPop3Client())
            {
                var pop3 = new Pop3Mail();
                受信MailList = pop3.ReceiveMails(pop3Client);

                if (古いメール削除)
                {
                    foreach (var mail in 受信MailList)
                    {
                        pop3.DeleteMessageByMessageId(pop3Client, mail.CurrentMessageID);
                    }
                }
            }

            //////////////////////////////////////////////////////////////////////////////////////
            //// Todo 受信したメールのfromからNoを取得
            //foreach (var mail in 受信MailList)
            //{
            //    mail.CollectTargetNo = MorphologicalAnalysisDB.GetCollectTargetNo(mail);
            //}

            return 受信MailList;
        }

        public static void Write(CollectMailRow mail)
        {
            File.AppendAllText(AppSetting.ResultPath_CollectEmailMEagazine + "\\" + Path.GetRandomFileName(),
                mail.CollectTargetNo + "\t" + mail.SendDate + "\t" + mail.FromMailAddress + "\t" + mail.FromDisplayName + "\t" + mail.CurrentMessageID + "\r\n" +
                //mail.CurrentSubject + "\r\n" +
                mail.CurrentBody,
                Encoding.UTF8);
        }

    }
}
