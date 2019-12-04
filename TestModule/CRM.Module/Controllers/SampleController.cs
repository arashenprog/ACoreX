using System;
using Microsoft.AspNetCore.Mvc;
using ACoreX.Infrastructure.Authentication;
using System.Threading.Tasks;
using CRM.Contracts.Module;
using CRM.Contracts.Module.Models;

namespace CRM.Module.Controllers
{
    [ApiController]
    public class MailInboxContextController : ControllerBase
    {
        private CRM.Contracts.Module.IMailInboxContract context;
        public MailInboxContextController(CRM.Contracts.Module.IMailInboxContract context) { this.context = context; }
        [HttpGet("api/email/inbox")]
        public System.Collections.Generic.IEnumerable<CRM.Contracts.Module.Models.MailMessagePreviewDTO> GetMailInbox() { return context.GetMailInbox(); }

    }

    [ApiController]
    public class SampleContextController : ControllerBase
    {
        private CRM.Contracts.Module.ISampleContext context;
        public SampleContextController([FromRoute]CRM.Contracts.Module.ISampleContext context) { this.context = context; }
        [HttpGet("api/test")]
        public string GetText() { return context.GetText(); }

        [Authentication]
        [HttpGet("api/sample")]
        public string GetTextAuthorized() { return context.GetTextAuthorized(); }

        [HttpGet("api/sample/list")]
        public async System.Threading.Tasks.Task<System.Collections.Generic.IEnumerable<CRM.Contracts.Module.Models.LeadDashboardDTO>> GetList() { return await context.GetList(); }

    }
}
