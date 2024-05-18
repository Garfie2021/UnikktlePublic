using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using OpenPop.Mime;
using OpenPop.Pop3;
using Message = OpenPop.Mime.Message;
using 定数;

namespace CollectEmailMEagazine
{
    public class Pop3Mail
    {

        public List<CollectMailRow> ReceiveMails(Pop3Client client)
        {
            var receivedMailList = new List<CollectMailRow>();
            int messageCount = client.GetMessageCount();
            string subject = string.Empty;
            string body = string.Empty;

            for (int i = messageCount; i >= 1; i -= 1)
            {
                Message message = client.GetMessage(i);
                MessagePart plainTextPart = message.FindFirstPlainTextVersion();
                subject = message.Headers.Subject;
                body = plainTextPart.GetBodyAsText();

                var collectMailRow = new CollectMailRow();
                collectMailRow.SendDate = message.Headers.DateSent;
                collectMailRow.FromMailAddress = message.Headers.From.Address;
                collectMailRow.FromDisplayName = message.Headers.From.DisplayName;
                collectMailRow.CurrentSubject = subject;
                collectMailRow.CurrentBody = body;
                collectMailRow.CurrentMessageID = message.Headers.MessageId;
                receivedMailList.Add(collectMailRow);
            }

            return receivedMailList;
        }

        public void DeleteMessageByMessageId(Pop3Client client, string messageId)
        {
            try
            {
                int messageCount = client.GetMessageCount();

                for (int messageItem = messageCount; messageItem > 0; messageItem--)
                {
                    if (client.GetMessageHeaders(messageItem).MessageId == messageId)
                    {
                        client.DeleteMessage(messageItem);
                    }
                }
            }
            catch (Exception e)
            {
                // 「既に削除済み」の場合はエラー処理をしない
                if (e.Message.IndexOf("ERR Message is deleted") == -1)
                {
                    throw new Exception("メール削除時に例外が発生しました。");
                }
            }
        }
    }

}
