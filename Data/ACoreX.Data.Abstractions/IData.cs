using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

namespace ACoreX.Data.Abstractions
{
    public interface IData 
    {
        IEnumerable<dynamic> Query(string sQuery, params DBParam[] parameters);
        Task<IEnumerable<dynamic>> QueryAsync(string sQuery, params DBParam[] parameters);
        void Execute(string sQuery, params DBParam[] parameters);
        Task ExecuteAsync(string sQuery, params DBParam[] parameters);

    }
}
