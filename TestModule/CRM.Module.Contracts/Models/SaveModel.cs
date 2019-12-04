using System.Collections.Generic;

namespace CRM.Module.Contracts.Models
{
    public class SaveModel
    {
        public string Value { get; set; }
        public string Entity { get; set; }
        public string Field { get; set; }
        public string Module { get; set; }
    }

    public class SaveInputModel
    {
        public List<EntityField> Fields { get; set; }
        public string Entity { get; set; }
        public string Module { get; set; }
    }

    public class EntityField
    {
        public object Value { get; set; }
        public string Name { get; set; }
    }
}
