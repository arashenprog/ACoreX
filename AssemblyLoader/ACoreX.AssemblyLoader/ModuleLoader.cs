using ACoreX.Extensions.Types;
using ACoreX.Injector.Abstractions;
using ACoreX.WebAPI;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using System.Threading.Tasks;

namespace ACoreX.AssemblyLoader
{
    public static class ApiModuleLoaderMiddlewareExtension
    {
        public static IMvcBuilder LoadModules(this IMvcBuilder mvcBuilder, IContainerBuilder builder, string location = null)
        {
            mvcBuilder.ConfigureApplicationPartManager(apm =>
            {
                if (string.IsNullOrWhiteSpace(location))
                {
                    location = new FileInfo(Assembly.GetEntryAssembly().Location).DirectoryName;
                }


                IEnumerable<string> dlls = System.IO.Directory.EnumerateFiles(location).Where(f => f.Contains("Module.dll"));

                foreach (string dll in dlls)
                {
                    bool flag = false;
                    Assembly assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(dll);
                    IEnumerable<System.Reflection.TypeInfo> modules = assembly
                        .DefinedTypes
                        .Where(type => type.ImplementedInterfaces.Contains(typeof(IModule)));
                    foreach (System.Reflection.TypeInfo item in modules)
                    {
                        IModule instance = (IModule)Activator.CreateInstance(item);
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

        public static IMvcBuilder AddControllers(this IMvcBuilder mvcBuilder)
        {
            mvcBuilder.ConfigureApplicationPartManager(apm =>
            {
                apm.FeatureProviders.Add(new RemoteControllerFeatureProvider());
            });
            return mvcBuilder;

        }


        public class RemoteControllerFeatureProvider : IApplicationFeatureProvider<ControllerFeature>
        {
            public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature)
            {

                IEnumerable<Assembly> all = AppDomain.CurrentDomain.GetAssemblies().Where(c => !c.FullName.Contains("DynamicMethods"));
                IEnumerable<Assembly> allSdk = all;
                IEnumerable<Assembly> modulesAssemblies = all.Where(c => c.FullName.Contains(".Module") && !c.FullName.Contains("Contract"));


                foreach (Assembly module in modulesAssemblies)
                {
                    string moduleName = module.FullName.Split(",")[0];
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("using System;");
                    //sb.AppendLine("using ACoreX.Core;");
                    //sb.AppendLine("using ACoreX.Core.Injector;");
                    sb.AppendLine("using Microsoft.AspNetCore.Mvc;");
                    sb.AppendLine("using ACoreX.Authentication.Core;");
                    //sb.AppendLine("using CRM.Module.Contexts;");
                    //sb.AppendLine("using Microsoft.AspNetCore.Authorization;");
                    //sb.AppendLine("using ACoreX.Infrastructure.Authentication;");
                    sb.AppendLine("using System.Threading.Tasks;");
                    //sb.AppendLine("using ACoreX.Core.WepApi;");
                    //sb.AppendLine("using ACoreX.Core.Authentication;");
                    sb.AppendLine();
                    sb.AppendFormat("namespace {0}.Controllers {{", moduleName);

                    IEnumerable<global::System.Reflection.TypeInfo> controllers = module.DefinedTypes.Where(c => c.GetMethods().Where(m => m.GetCustomAttributes<WebApiAttribute>().Count() > 0).Count() > 0);

                    foreach (global::System.Reflection.TypeInfo ct in controllers)
                    {
                        string interfaceName = ct.GetInterfaces()[0].FullName;
                        sb.AppendLine();
                        sb.AppendLine("[ApiController]");
                        sb.AppendFormat("public class {0}Controller : ControllerBase {{", ct.Name);
                        sb.AppendLine();
                        sb.AppendFormat(" private {0} context;", interfaceName);
                        sb.AppendLine();
                        sb.AppendFormat("public {0}Controller({1} context) {{ ", ct.Name, interfaceName);
                        sb.AppendFormat("this.context = context;", interfaceName);
                        sb.AppendLine("}");
                        IEnumerable<MethodInfo> actions = ct.GetMethods().Where(m => m.GetCustomAttributes<WebApiAttribute>().Count() > 0);
                        foreach (MethodInfo a in actions)
                        {
                            WebApiAttribute attr = a.GetCustomAttribute<WebApiAttribute>();
                            if (attr.Authorized)
                            {
                                sb.AppendLine("[Authentication]");
                            }
                            //else
                            //{
                            //    sb.AppendLine("[AllowAnonymous]");
                            //}
                            switch (attr.Method)
                            {
                                case WebApiMethod.Get:
                                    sb.AppendFormat(@"[HttpGet(""{0}"")]", attr.Route);
                                    break;
                                case WebApiMethod.Post:
                                    sb.AppendFormat(@"[HttpPost(""{0}"")]", attr.Route);
                                    break;
                                case WebApiMethod.Put:
                                    sb.AppendFormat(@"[HttpPut(""{0}"")]", attr.Route);
                                    break;
                                case WebApiMethod.Delete:
                                    sb.AppendFormat(@"[HttpDelete(""{0}"")]", attr.Route);
                                    break;
                                case WebApiMethod.Head:
                                    sb.AppendFormat(@"[HttpHead(""{0}"")]", attr.Route);
                                    break;
                                case WebApiMethod.Patch:
                                    sb.AppendFormat(@"[HttpPatch(""{0}"")]", attr.Route);
                                    break;
                                default:
                                    sb.AppendFormat(@"[HttpGet(""{0}"")]", attr.Route);
                                    break;
                            }
                            sb.AppendLine();
                            List<string> paramsList = new List<string>();

                            List<string> paramsValue = new List<string>();
                            bool hasFile = false;
                            foreach (ParameterInfo p in a.GetParameters())
                            {
                                //paramsList.Add(String.Format("[FromBody]{0} {1}",p.ParameterType.PrettyName(),p.Name));
                                if(p.GetType() == typeof(byte))
                                {
                                    hasFile = true;
                                }
                                paramsList.Add(string.Format("{0} {1}{2}", p.ParameterType.GetFriendlyName(), p.Name,
                                    p.HasDefaultValue &&
                                    p.ParameterType.GetFriendlyName() == "string" ? "=" + @"""" + p.DefaultValue + @"""" : p.HasDefaultValue ? "=" + p.DefaultValue.ToString() : ""));
                                paramsValue.Add(string.Format("{0}", p.Name));
                            }
                            string paramsStr = "";
                            if (hasFile)
                            {
                                paramsStr = "[FromFile] ";
                            }
                            paramsStr = string.Join(",", paramsList);
                            string valueStr = string.Join(",", paramsValue);

                            sb.AppendFormat("public {3} {1} {0}({2}){{",
                                a.Name,
                                a.ReturnType.GetFriendlyName() == "System.Void" ? "void" : a.ReturnType.GetFriendlyName(),
                                paramsStr,
                                a.ReturnType.BaseType == typeof(Task) ? "async" : "");
                            sb.AppendFormat("{0} {3} context.{1}({2});",
                                a.ReturnType != typeof(void) ? "return" : "",
                                a.Name,
                                valueStr,
                                a.ReturnType.BaseType == typeof(Task) ? "await" : "");
                            sb.AppendLine("}");
                            //
                            sb.AppendLine();

                        }
                        sb.AppendLine("}");
                    }
                    sb.AppendLine("}");
                    SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(sb.ToString());
                    string assemblyName = $"{moduleName}.Controllers";
                    List<MetadataReference> references = new List<MetadataReference>();
                    foreach (Assembly r in allSdk)
                    {
                        references.Add(MetadataReference.CreateFromFile(r.Location));
                    }
                    references.Add(MetadataReference.CreateFromFile(module.Location));

                    //
                    CSharpCompilation compilation = CSharpCompilation.Create(
                          assemblyName,
                          syntaxTrees: new[] { syntaxTree },
                          references: references,
                          options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));


                    using (MemoryStream ms = new MemoryStream())
                    {
                        Microsoft.CodeAnalysis.Emit.EmitResult result = compilation.Emit(ms);
                        if (!result.Success)
                        {
                            IEnumerable<Diagnostic> failures = result.Diagnostics.Where(diagnostic =>
                                diagnostic.IsWarningAsError ||
                                diagnostic.Severity == DiagnosticSeverity.Error);

                            foreach (Diagnostic diagnostic in failures)
                            {
                                Console.Error.WriteLine("{0}: {1}", diagnostic.Id, diagnostic.GetMessage());
                            }
                        }
                        else
                        {
                            ms.Seek(0, SeekOrigin.Begin);
                            Assembly assembly = AssemblyLoadContext.Default.LoadFromStream(ms);
                            Type[] candidates = assembly.GetExportedTypes();
                            foreach (Type candidate in candidates)
                            {
                                feature.Controllers.Add(candidate.GetTypeInfo());
                            }

                        }
                    }
                }
            }



        }


    }
}
