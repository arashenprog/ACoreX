using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ACoreX.Data.Abstractions
{
    public interface IData 
    {
        IEnumerable<T> Query<T>(string sQuery, params DBParam[] parameters);
        Task<IEnumerable<T>> QueryAsync<T>(string sQuery, params DBParam[] parameters);
        void Execute(string sQuery, params DBParam[] parameters);
        Task ExecuteAsync(string sQuery, params DBParam[] parameters);

        IDbConnection Connection { get; }

        SqlConnection OpenConnection();

        void Dispose();

    }
}
