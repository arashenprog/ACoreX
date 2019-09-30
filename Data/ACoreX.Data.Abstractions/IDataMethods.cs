using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ACoreX.Data.Abstractions
{
    public interface IDataMethods
    {
        List<T> Read<T>();
        Task<List<T>> ReadAsync<T>();
        void Insert<T>(T model);
        Task InsertAsync<T>(T model);

        void Update<T>(T model, object updateObject);
        Task UpdateAsync<T>(T model);

        void Delete<T>(T model, Guid uID);
        Task DeleteAsync<T>(T model, Guid uID);

        void SPCall<T>(string SPName, T model);
    }
}
