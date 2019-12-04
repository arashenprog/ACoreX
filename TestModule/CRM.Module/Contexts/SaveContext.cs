using ACoreX.Data.Abstractions;
using ACoreX.WebAPI;
using CRM.Module.Contracts.Models;
using System;

namespace CRM.Module.Contexts
{
    public class SaveContext : ISaveContext
    {
        private IData _data;
        public SaveContext(IData data)
        {
            _data = data;
        }
        //[WebApi(Authorized = true, Route = "api/test")]
        //public string Save(SaveModel saveModel)
        //{

        //    return DateTime.Now.ToLongDateString();
        //}

    }
}
