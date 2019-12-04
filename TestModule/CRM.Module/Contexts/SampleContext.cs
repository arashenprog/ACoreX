
using ACoreX.Configurations.Abstractions;
using ACoreX.Data.Abstractions;
using ACoreX.WebAPI;
using ACoreX.WebAPI.Abstractions;
using CRM.Contracts.Module;
using CRM.Contracts.Module.Models;
using CRM.Module.Contracts.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft;

namespace CRM.Module.Contexts
{
    public class SampleContext : ISampleContext
    {
        IConfiguration _configuration;
        public SampleContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [WebApi(Authorized = false, Route = "api/test")]
        public dynamic GetText(string test)
        {
            using (SqlConnection conn = new SqlConnection(_configuration.Get("ConnectionString:SQLConnection")))
            {
                try
                {

                    DBParam p0 = new DBParam();
                    p0.Name = "@refId";
                    p0.Value = "af70eb19-dc7e-7bb9-05a9-fc544ebc7fb2";

                    DBParam p1 = new DBParam();
                    p1.Name = "@date";
                    p1.Value = "1953 - 01 - 15 07:39:52.470";

                    DBParam p2 = new DBParam();
                    p2.Name = "@note";
                    p2.Value = "data.Note";

                    DBParam p3 = new DBParam();
                    p3.Name = "@userId";
                    p3.Value = null;
                    p3.IsNullable = true;

                    var tes = conn.ExecuteReader(sql:"select * FROM [CRM].[ADM].[ADM_VW_IMPORT]", commandType: System.Data.CommandType.Text);



                    var tes1 = conn.Query<dynamic>("select * from ADM.ADM_USERS");
                    
                    conn.Dispose();
                    return tes1;
                }
                catch (Exception ex)
                {
                    conn.Dispose();
                    throw ex;
                }
                finally
                {

                    conn.Dispose();
                }

            }
        }

        //[WebApi(Route = "api/{entityName}/delete", Authorized = false, Method = WebApiMethod.Post)]
        //public Task Delete(string entityName, GeneralDataModel model)
        //{
        //    using (SqlConnection cnn = _Idata.OpenConnection())
        //    {
        //        try
        //        {
        //            StringBuilder sb = new StringBuilder();

        //            sb.AppendFormat("DELETE FROM {0} WHERE {1} = @param ", entityName, model.Conditions);
        //            DBParam p1 = new DBParam { Name = "@param", Value = model.Values };
        //            var result = _Idata.Execute(sb.ToString(), commandType: System.Data.CommandType.Text, p1);
        //            return Task.CompletedTask;
        //        }
        //        catch (Exception ex)
        //        {
        //            _Idata.Dispose();
        //            throw ex;
        //        }
        //    }

        //}

        [WebApi(Route = "api/sample", Authorized = false)]
        public string GetTextAuthorized(LeadDashboardDTO dto)
        {
            return DateTime.Now.ToLongDateString();
        }

        [WebApi(Route = "api/sample/list")]
        public async Task<IEnumerable<LeadDashboardDTO>> GetList()
        {
            List<LeadDashboardDTO> list = new List<LeadDashboardDTO>();
            Task task1 = Task.Run(() =>
             {
                 for (int i = 0; i < 100; i++)
                 {
                     list.Add(new LeadDashboardDTO { ID = i, Name = $"Item{i}", Date = DateTime.Now.AddDays(i) });
                 }
             });

            Task task2 = Task.Run(() =>
            {
                for (int i = 101; i < 200; i++)
                {
                    list.Add(new LeadDashboardDTO { ID = i, Name = $"Item{i}", Date = DateTime.Now.AddDays(i) });
                }
            });

            //throw new CustomException("This is an exception");

            await Task.WhenAll(task1, task2);

            return list;
        }
        [WebApi(Authorized = false, Route = "api/save", Method = WebApiMethod.Post)]
        public string GetContact(SaveInputModel saveInput)
        {
            throw new NotImplementedException();
        }

        public Task Delete(string entityName, GeneralDataModel model)
        {
            throw new NotImplementedException();
        }
    }


}
