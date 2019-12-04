
using CRM.Contracts.Module.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Contracts.Module
{
    public interface IMailInboxContract
    {
        IEnumerable<MailMessagePreviewDTO> GetMailInbox();
    }
}
