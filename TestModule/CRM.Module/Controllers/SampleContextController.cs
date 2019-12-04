using System;
using Microsoft.AspNetCore.Mvc;
using ACoreX.Authentication.Core;
using System.Threading.Tasks;

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
        public SampleContextController(CRM.Contracts.Module.ISampleContext context) { this.context = context; }
        [HttpGet("api/test")]
        public string GetText(string test) { return context.GetText(test); }

        [HttpPost("api/{entityName}/delete")]
        public System.Threading.Tasks.Task Delete([FromRoute]string entityName) { return context.Delete(entityName, model); }

        [HttpGet("api/sample")]
        public string GetTextAuthorized([FromForm] CRM.Contracts.Module.Models.LeadDashboardDTO dto) { return context.GetTextAuthorized(dto); }

        [HttpGet("api/sample/list")]
        public async System.Threading.Tasks.Task<System.Collections.Generic.IEnumerable<CRM.Contracts.Module.Models.LeadDashboardDTO>> GetList() { return await context.GetList(); }

        [HttpPost("api/save")]
        public string GetContact(CRM.Module.Contracts.Models.SaveInputModel saveInput) { return context.GetContact(saveInput); }

    }
}
