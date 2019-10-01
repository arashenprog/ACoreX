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

        public IDbConnection _connection;
        public IDbConnection Connection
        {
            get
            {
                return _connection;
            }
        }

        public DapperData(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
        }

        public DapperData(IDbConnection connection)
        {
            _connection = connection;
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

        public IEnumerable<T> Query<T>(string sQuery, params DBParam[] parameters)
        {
            using (IDbConnection connection = Connection)
            {
                var dbArgs = new DynamicParameters();
                foreach (var pair in parameters) dbArgs.Add(pair.Name, pair.Value);
                //
                IEnumerable<T> result = connection.Query<T>(sQuery, dbArgs);
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

        public Task<IEnumerable<T>> QueryAsync<T>(string sQuery, params DBParam[] parameters)
        {
            using (IDbConnection connection = Connection)
            {
                Task<IEnumerable<T>> result = connection.QueryAsync<T>(sQuery, parameters);
                return result;
            }
        }
    }
}
