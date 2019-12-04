using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Contracts.Module.Models
{
    public class MailMessageDTO
    {
        public DateTime Date { get; set; }

        public string Message { get; set; }

        public string Sender { get; set; }
        public string Subject { get; set; }
    }


    public class MailMessagePreviewDTO
    {
        public DateTime Date { get; set; }

        public string Sender { get; set; }
        public string Subject { get; set; }
    }
}
