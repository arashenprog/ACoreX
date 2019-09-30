using ACoreX.Injector.Abstractions;
using ACoreX.Plugin;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

namespace ACoreX.AssemblyLoader
{
    public static class ApiPluginLoaderMiddlewareExtension
    {
        public static IMvcBuilder LoadPlugins(this IMvcBuilder mvcBuilder, IContainerBuilder builder, string location = null)
        {
            mvcBuilder.ConfigureApplicationPartManager(apm =>
            {
                if (string.IsNullOrWhiteSpace(location))
                {
                    location = new FileInfo(Assembly.GetEntryAssembly().Location).DirectoryName;
                }


                IEnumerable<string> allplugins = System.IO.Directory.EnumerateFiles(location).Where(f => f.Contains(".Plugin.dll"));

                foreach (string dll in allplugins)
                {
                    bool flag = false;
                    Assembly assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(dll);
                    IEnumerable<System.Reflection.TypeInfo> plugins = assembly
                        .DefinedTypes
                        .Where(type => type.ImplementedInterfaces.Contains(typeof(IPlugin)));
                    foreach (System.Reflection.TypeInfo item in plugins)
                    {
                        IPlugin instance = (IPlugin)Activator.CreateInstance(item);
                        instance.Register(builder);
                        flag = true;
                    }
                    if (flag)
                    {
                        apm.ApplicationParts.Add(new AssemblyPart(assembly));
                    }
                }

            });
            return mvcBuilder;
        }
    }
}
