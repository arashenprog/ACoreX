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
        public IDbConnection Connection => _connection;


        public string _connectionstring;

        public DapperData(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
            _connectionstring = connectionString;
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
        private IDbConnection CreateConnection()
        {
            SqlConnection connection = new SqlConnection(_connectionstring);
            // Properly initialize your connection here.
            return connection;
        }
        public IEnumerable<T> Query<T>(string sQuery, params DBParam[] parameters)
        {
            //using (var cn = CreateConnection())
            //{
            //    cn.Open();
            //    using (var tran = cn.BeginTransaction())
            //    {
            //        try
            //        {
            //            // multiple operations involving cn and tran here
            //            var dbArgs = new DynamicParameters();
            //            foreach (var pair in parameters) dbArgs.Add(pair.Name, pair.Value);
            //            var affectedRows = cn.Query<T>(sQuery, dbArgs);
            //            tran.Commit();
            //            return affectedRows.AsList();
            //        }
            //        catch
            //        {
            //            tran.Rollback();
            //            throw;
            //        }
            //    }
            //}
            //using (var transaction = new TransactionScope())
            //{
            //    using (var connection = My.ConnectionFactory())
            //    using (IDbConnection connection = Connection)
            //    {
            //        connection.Open();
            //        var dbArgs = new DynamicParameters();
            //        foreach (var pair in parameters) dbArgs.Add(pair.Name, pair.Value);
            //        var result = connection.Query<T>(sQuery, dbArgs);
            //        transaction.Complete();
            //        return result.AsList();
            //    }

            //}
            //using (IDbConnection connection = Connection)
            //{
                DynamicParameters dbArgs = new DynamicParameters();
                foreach (DBParam pair in parameters)
                {
                    dbArgs.Add(pair.Name, pair.Value);
                }
                //
                IEnumerable<T> result = Connection.Query<T>(sQuery, dbArgs);
                Dispose();
                return result.AsList();
            //}
        }
        public void Dispose()
        {
            if (_connection != null && _connection.State == ConnectionState.Open)
            {
                _connection.Close();
                _connection = null;
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
