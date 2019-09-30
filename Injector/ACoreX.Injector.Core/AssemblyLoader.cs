using Microsoft.Extensions.DependencyModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

namespace ACoreX.Injector.Core
{
    public static class AssemblyLoader
    {
        public static Assembly LoadFromAssemblyPath(string assemblyFullPath)
        {
            string fileNameWithOutExtension = Path.GetFileNameWithoutExtension(assemblyFullPath);
            string fileName = Path.GetFileName(assemblyFullPath);
            string directory = Path.GetDirectoryName(assemblyFullPath);

            bool inCompileLibraries = DependencyContext.Default.CompileLibraries.Any(l => l.Name.Equals(fileNameWithOutExtension, StringComparison.OrdinalIgnoreCase));
            bool inRuntimeLibraries = DependencyContext.Default.RuntimeLibraries.Any(l => l.Name.Equals(fileNameWithOutExtension, StringComparison.OrdinalIgnoreCase));

            Assembly assembly = (inCompileLibraries || inRuntimeLibraries)
                ? Assembly.Load(new AssemblyName(fileNameWithOutExtension))
                : AssemblyLoadContext.Default.LoadFromAssemblyPath(assemblyFullPath);

            if (assembly != null)
            {
                LoadReferencedAssemblies(assembly, fileName, directory);
            }

            return assembly;
        }

        private static void LoadReferencedAssemblies(Assembly assembly, string fileName, string directory)
        {
            List<string> filesInDirectory = Directory.GetFiles(directory).Where(x => x != fileName).Select(x => Path.GetFileNameWithoutExtension(x)).ToList();
            AssemblyName[] references = assembly.GetReferencedAssemblies();

            foreach (AssemblyName reference in references)
            {
                if (filesInDirectory.Contains(reference.Name))
                {
                    string loadFileName = reference.Name + ".dll";
                    string path = Path.Combine(directory, loadFileName);
                    Assembly loadedAssembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(path);
                    if (loadedAssembly != null)
                    {
                        LoadReferencedAssemblies(loadedAssembly, loadFileName, directory);
                    }
                }
            }

        }

    }
}
