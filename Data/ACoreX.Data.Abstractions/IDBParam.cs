using System.Data;

namespace ACoreX.Data.Abstractions
{

    public interface IDBParam
    {

        DbType DbType { get; set; }

        ParameterDirection Direction { get; set; }

        bool IsNullable { get; }
    
        string Name { get; set; }
      
        object Value { get; set; }

       
    }
}
