using ACoreX.Data.Abstractions;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ACoreX.Data.Dapper
{
    public class DapperData : IData
    {
        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection("Server=192.168.105.55\\exp17;Database=AcoreXTest; User Id = ma; Password = 123;");
            }
        }

        //public void Execute(string sQuery, DynamicParameters parameters)
        //{
        //    using (IDbConnection connection = Connection)
        //    {
        //        IEnumerable<dynamic> result = connection.Query(sQuery, parameters);
        //    }
        //}

        public void Execute(string sQuery, params DBParam[] parameters)
        {
            using (IDbConnection connection = Connection)
            {
                IEnumerable<dynamic> result = connection.Query(sQuery, parameters);
            }
        }

        //public Task ExecuteAsync(string sQuery, DynamicParameters parameters)
        //{
        //    using (IDbConnection connection = Connection)
        //    {
        //        Task<IEnumerable<dynamic>> result = connection.QueryAsync(sQuery, parameters);
        //        return Task.CompletedTask;
        //    }
        //}

        public Task ExecuteAsync(string sQuery, params DBParam[] parameters)
        {
            using (IDbConnection connection = Connection)
            {
                Task<IEnumerable<dynamic>> result = connection.QueryAsync(sQuery, parameters);
                return Task.CompletedTask;
            }
        }

        //public List<dynamic> Query(string sQuery, DynamicParameters parameters)
        //{
        //    using (IDbConnection connection = Connection)
        //    {
        //        IEnumerable<dynamic> result = connection.Query(sQuery, parameters);
        //        return result.AsList();

        //    }
        //}

        public IEnumerable<dynamic> Query(string sQuery, params DBParam[] parameters)
        {
            using (IDbConnection connection = Connection)
            {
                IEnumerable<dynamic> result = connection.Query(sQuery, parameters);
                return result.AsList();
            }
        }

        //public Task<IEnumerable<dynamic>> QueryAsync(string sQuery, DynamicParameters parameters)
        //{
        //    using (IDbConnection connection = Connection)
        //    {
        //        Task<IEnumerable<dynamic>> result = connection.QueryAsync(sQuery, parameters);
        //        return result;

        //    }
        //}

        public Task<IEnumerable<dynamic>> QueryAsync(string sQuery, params DBParam[] parameters)
        {
            using (IDbConnection connection = Connection)
            {
                Task<IEnumerable<dynamic>> result = connection.QueryAsync(sQuery, parameters);
                return result;
            }
        }
    }
}
