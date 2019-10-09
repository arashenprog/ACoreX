using ACoreX.AssemblyLoader;
using ACoreX.Authentication.Abstractions;
using ACoreX.Authentication.Core;
using ACoreX.Authentication.JWT;
using ACoreX.Data.Abstractions;
using ACoreX.Data.Dapper;
using ACoreX.Injector.Abstractions;
using ACoreX.Injector.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ACoreX.View.Status
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            IContainerBuilder builder = services.AddBuilder(new NetCoreContainerBuilder(services));
            services
             .AddAuthenticationInstance<JWTAuthService>()
             //.AddLogger<FileLogger>()
             //.AddLogger<MyCustomLogger>(false)
             .AddMvc()
             .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
             .LoadModules(builder, "D:/app/lib/")
            //.LoadPlugins(builder, "D:/app/lib/")
             .AddControllers();
            builder.AddSingleton<IAuthHandler, MyAuthClass>();
            builder.AddScope<IData, DapperData>(new DapperData("Server=192.168.105.55\\exp17;Database=CRM; User Id = ma; Password = 123;"));


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }

    public class MyAuthClass : IAuthHandler
    {

        private IData _Idata;
        public MyAuthClass(IData data)
        {

            _Idata = data;
        }

        public bool Check(IToken token)
        {
            bool checkToken = token.Value == "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYmYiOjE1NjAwODIwNzYsImV4cCI6MTU2MjY3NDA3NiwiaWF0IjoxNTYwMDgyMDc2fQ.T5kpSeVyM8lLNIT72oCoxy5uIo6UkK0tptWJEzkpV0I";
            System.Security.Claims.ClaimsPrincipal checkTheToken = token.ValidateToken(token.Value);
            string userid = token.GetValue("Id");

            //_Idata.Query<Contact>("select * from contacts");
            //_Idata.Query<Contact>("select * from contacts");
            return checkToken;
        }
    }
}
