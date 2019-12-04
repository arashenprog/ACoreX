

using ACoreX.Data.Abstractions;
using ACoreX.Injector.Abstractions;
using CRM.Contracts.Module;
using CRM.Module.Contexts;

namespace CRM.Module
{
    public class Module : IModule
    {
        public void Register(IContainerBuilder builder)
        {
            builder.AddTransient<ISampleContext, SampleContext>();
            //builder.AddTransient<IMailInboxContract, MailInboxContext>();
            //builder.AddScope<IData, DapperData>(new DapperData("Server=192.168.105.55\\exp17;Database=CRM; User Id = ma; Password = 123;"));
        }
    }
}
