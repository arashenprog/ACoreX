using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ACoreX.AssemblyLoader;
using ACoreX.Authentication.Core;
using ACoreX.Authentication.JWT;
using ACoreX.Injector.Abstractions;
using ACoreX.Injector.Core;
using ACoreX.Data.Abstractions;
using ACoreX.Data.Dapper;
using Microsoft.AspNetCore.Http;
using ACoreX.Injector.NetCore;
using ACoreX.WebAPI;

namespace Sample.Run
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            string libPath = Configuration["Moduels:Path"];
            IContainerBuilder builder = services.AddBuilder(new NetCoreContainerBuilder(services));
            builder.AddSingleton<ACoreX.Configurations.Abstractions.IConfiguration, ACoreX.Configurations.NetCore.NetCoreConfiguration>();
            //builder.AddTransient<IData, DapperData>(new DapperData(Configuration["ConnectionString:SQLConnection"]));
            _ = services
           //.AddAuthenticationInstance<JWTAuthService>()
           .AddCors(options =>
           {
               options.AddPolicy(MyAllowSpecificOrigins,
               builder =>
               {
                   builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
               });
           })
           .AddMvc()
           //.AddNewtonsoftJson()
           .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
           .LoadModules(builder, libPath)
           .AddControllers()
           ;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAPIResponseWrapperMiddleware();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
