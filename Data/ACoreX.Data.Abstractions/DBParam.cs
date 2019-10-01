using System;
using System.ComponentModel;
using System.Data;

namespace ACoreX.Data.Abstractions
{
    public class DBParam
    {
        public DbType DbType { get; set; }

        [DefaultValue(ParameterDirection.Input)]
        public ParameterDirection Direction { get; set; }

        public bool IsNullable { get; set; }

        [DefaultValue("")]
        public string Name { get; set; }

        [DefaultValue(null)]
        public object Value { get; set; }

    }
}
