

using ACoreX.WebAPI;
using CRM.Contracts.Module;
using CRM.Contracts.Module.Models;
using MailKit;
using MailKit.Net.Imap;
using System;
using System.Collections.Generic;

namespace CRM.Module.Contexts
{
    public class MailInboxContext : IMailInboxContract
    {

        [WebApi(Route = "api/email/inbox")]
        public IEnumerable<MailMessagePreviewDTO> GetMailInbox()
        {
            List<MailMessagePreviewDTO> list = new List<MailMessagePreviewDTO>();
            using (ImapClient client = new ImapClient())
            {
                // For demo-purposes, accept all SSL certificates
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                client.Connect("imap.gmail.com", 993, true);

                client.Authenticate("arash.oshnoudi@gmail.com", "V3@pu166@@!!");

                // The Inbox folder is always available on all IMAP servers...
                IMailFolder inbox = client.Inbox;
                inbox.Open(FolderAccess.ReadOnly);
                int index = Math.Max(inbox.Count - 10, 0);
                IList<IMessageSummary> items = inbox.Fetch(index, -1, MessageSummaryItems.UniqueId);

                foreach (IMessageSummary item in items)
                {
                    MimeKit.MimeMessage message = inbox.GetMessage(item.UniqueId);
                    list.Add(new MailMessagePreviewDTO
                    {
                        Date = message.Date.DateTime.ToUniversalTime(),
                        Sender = message.From[0].Name,
                        Subject = message.Subject
                    });
                }



            }
            return list;
        }
    }
}
