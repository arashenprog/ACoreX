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

        public SqlConnection _connection;
        //public IDbConnection Connection => _connection;


        public string _connectionstring;

        public IDbConnection Connection => throw new System.NotImplementedException();

        public DapperData(string connectionString)
        {
            // _connection = new SqlConnection(connectionString);
            _connectionstring = connectionString;
        }

        //public DapperData(IDbConnection connection)
        //{
        //    _connection = connection;
        //}

        //public void Execute(string sQuery, DynamicParameters parameters)
        //{
        //    using (IDbConnection connection = Connection)
        //    {
        //        IEnumerable<dynamic> result = connection.Query(sQuery, parameters);
        //    }
        //}

        //public void Execute(string sQuery, params DBParam[] parameters)
        //{
        //    using (IDbConnection connection = Connection)
        //    {
        //        IEnumerable<dynamic> result = connection.Query(sQuery, parameters);
        //    }
        //}

        //public Task ExecuteAsync(string sQuery, DynamicParameters parameters)
        //{
        //    using (IDbConnection connection = Connection)
        //    {
        //        Task<IEnumerable<dynamic>> result = connection.QueryAsync(sQuery, parameters);
        //        return Task.CompletedTask;
        //    }
        //}

        //public Task ExecuteAsync(string sQuery, params DBParam[] parameters)
        //{
        //    using (IDbConnection connection = Connection)
        //    {
        //        Task<IEnumerable<dynamic>> result = connection.QueryAsync(sQuery, parameters);
        //        return Task.CompletedTask;
        //    }
        //}

        //public List<dynamic> Query(string sQuery, DynamicParameters parameters)
        //{
        //    using (IDbConnection connection = Connection)
        //    {
        //        IEnumerable<dynamic> result = connection.Query(sQuery, parameters);
        //        return result.AsList();

        //    }
        //}
        //public IDbConnection CreateConnection()
        //{
        //    SqlConnection connection = new SqlConnection(_connectionstring);
        //    // Properly initialize your connection here.
        //    return connection;
        //}
        public IEnumerable<T> Query<T>(string sQuery, params DBParam[] parameters)
        {
            try
            {
                DynamicParameters dbArgs = new DynamicParameters();
                foreach (DBParam pair in parameters)
                {
                    dbArgs.Add(pair.Name, pair.Value);
                }
                //
                IEnumerable<T> result = _connection.Query<T>(sQuery, dbArgs);
                return result.AsList();
            }
            catch (System.Exception ex)
            {

                throw ex;
            }


        }

        public dynamic Query(string sQuery, params DBParam[] parameters)
        {
            try
            {
                if (parameters.Length > 0)
                {
                    DynamicParameters dbArgs = new DynamicParameters();
                    foreach (DBParam pair in parameters)
                    {
                        dbArgs.Add(pair.Name, pair.Value);
                    }
                    //
                    var result = _connection.Query(sQuery, dbArgs);
                    return result.AsList();
                }
                else
                {
                    var result = _connection.Query(sQuery, commandType: CommandType.Text);
                    return result.AsList();
                }
            }
            catch (System.Exception ex)
            {

                throw ex;
            }



        }

        public SqlConnection OpenConnection()
        {
            string constr = _connectionstring;
            _connection = new SqlConnection(constr);
            _connection.Open();
            return _connection;
        }

        public Task<IEnumerable<T>> QueryAsync<T>(string sQuery, params DBParam[] parameters)
        {
            throw new System.NotImplementedException();
        }

        public dynamic Execute(string sQuery, CommandType commandType = CommandType.Text, params DBParam[] parameters)
        {
            try
            {
                DynamicParameters dbArgs = new DynamicParameters();
                foreach (DBParam pair in parameters)
                {
                    dbArgs.Add(pair.Name, pair.Value);
                }
                //
                var result = _connection.Execute(sQuery, dbArgs, commandType: commandType);

                return result;

            }
            catch (System.Exception ex)
            {

                throw ex;
            }

        }

        public Task ExecuteAsync(string sQuery, params DBParam[] parameters)
        {
            throw new System.NotImplementedException();
        }
        public void Dispose()
        {
            if (_connection != null && _connection.State == ConnectionState.Open)
            {
                _connection.Close();
                _connection = null;
            }
            if (_connection != null && _connection.State == ConnectionState.Connecting)
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

        //public Task<IEnumerable<T>> QueryAsync<T>(string sQuery, params DBParam[] parameters)
        //{
        //    using (IDbConnection connection = Connection)
        //    {
        //        Task<IEnumerable<T>> result = connection.QueryAsync<T>(sQuery, parameters);
        //        return result;
        //    }
        //}
    }
}
