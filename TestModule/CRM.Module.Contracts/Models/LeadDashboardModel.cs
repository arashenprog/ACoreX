using System;

namespace CRM.Contracts.Module.Models
{
    public class LeadDashboardDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public byte[] FileTest { get; set; }
    }
}
