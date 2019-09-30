using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ACoreX.Data.Dapper
{
    public class DapperMethodData 
    {
        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection("Server=192.168.105.55\\exp17;Database=AcoreXTest; User Id = ma; Password = 123;");
            }
        }
        public void Delete<T>(T model, Guid uID)
        {
            string sql = "DELETE FROM" + typeof(T).FullName + "  WHERE Id =" + uID;

            using (IDbConnection connection = Connection)
            {
                connection.Execute(sql);
            }
        }

        public Task DeleteAsync<T>(T model, Guid uID)
        {
            string sql = "DELETE FROM" + typeof(T).FullName + "  WHERE Id =" + uID;

            using (IDbConnection connection = Connection)
            {
                connection.ExecuteAsync(sql);
                return Task.CompletedTask;
            }
        }

        public List<T> Read<T>()
        {
            using (IDbConnection connection = Connection)
            {
                string sQuery = "SELECT * FROM " + typeof(T).Name + "s";
                connection.Open();
                IEnumerable<T> result = connection.Query<T>(sQuery);
                return result.AsList();
            }
        }



        public async Task<List<T>> ReadAsync<T>()
        {
            using (IDbConnection connection = Connection)
            {
                string sQuery = "SELECT * FROM" + typeof(T).FullName;
                connection.Open();
                IEnumerable<T> result = await connection.QueryAsync<T>(sQuery);
                return result.AsList();
            }
        }

        public void Update<T>(T model, object updateObject)
        {
            string sql = "UPDATE" + typeof(T).FullName + " SET Description = @Description WHERE CategoryID = @CategoryID;";

            using (IDbConnection connection = Connection)
            {
                connection.Execute(sql, typeof(T));
            }
        }

        public Task UpdateAsync<T>(T model)
        {
            string sql = "UPDATE" + typeof(T).FullName + " SET Description = @Description WHERE CategoryID = @CategoryID;";

            using (IDbConnection connection = Connection)
            {
                connection.ExecuteAsync(sql, typeof(T));
                return Task.CompletedTask;
            }
        }

        public void Insert<T>(T model)
        {
            string sql = "INSERT INTO" + typeof(T).FullName + "(CustomerName)Values(@CustomerName); ";

            using (IDbConnection connection = Connection)
            {
                connection.Execute(sql, typeof(T));

            }

        }

        public Task InsertAsync<T>(T model)
        {
            string sql = "INSERT INTO" + typeof(T).FullName + "(CustomerName)Values(@CustomerName); ";

            using (IDbConnection connection = Connection)
            {
                connection.ExecuteAsync(sql, typeof(T));
                return Task.CompletedTask;
            }
        }

        public void SPCall<T>(string SPName, T model)
        {

            using (IDbConnection connection = Connection)
            {
                int affectedRows = connection.Execute(SPName, typeof(T),
                    commandType: CommandType.StoredProcedure);

            }
        }

    }
}
