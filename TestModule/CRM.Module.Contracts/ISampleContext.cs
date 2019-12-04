using ACoreX.Data.Abstractions;
using CRM.Contracts.Module.Models;
using CRM.Module.Contracts.Models;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Contracts.Module
{
    public interface ISampleContext
    {
        dynamic GetText(string test);
        string GetTextAuthorized(LeadDashboardDTO dto);

        Task Delete(string entityName, GeneralDataModel model);
        string GetContact(SaveInputModel saveInput);
        Task<IEnumerable<LeadDashboardDTO>> GetList();
    }

    public class ADMUser
    {
        public dynamic data { get; set; }
        public string username { get; set; }
        public string email { get; set; }
    }
}
