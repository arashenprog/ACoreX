using System;
using System.ComponentModel;
using System.Data;

namespace ACoreX.Data.Abstractions
{
    public abstract class DBParam : IDBParam
    {
        public abstract DbType DbType { get; set; }
   
        [DefaultValue(ParameterDirection.Input)]
        public abstract ParameterDirection Direction { get; set; }
      
        public abstract bool IsNullable { get; set; }
    
        [DefaultValue("")]
        public abstract string Name { get; set; }

        [DefaultValue(null)]
        public abstract object Value { get; set; }
        
    }
}
