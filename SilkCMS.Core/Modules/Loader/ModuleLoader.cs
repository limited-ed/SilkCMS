
using System.Reflection;
using System.Runtime.Loader;
using Microsoft.AspNetCore.Mvc;
using SilkCMS.Core.Modules;

namespace SilkCMS.Core.Modules;

public class ModuleLoader : IModuleLoader
{
    public List<IModuleInfo> LoadFromFolder(string path, bool skip = true)
    {
        var list = new List<IModuleInfo>();
        foreach (var dir in Directory.GetDirectories(path))
        {
            try
            {
                var file = Directory.GetFiles(dir,"*.Module.dll").FirstOrDefault();
                if (String.IsNullOrEmpty(file)) 
                    continue;

                Assembly assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(file); //  oadFromStream(new MemoryStream(bytes)); //Assembly.Load(bytes);
                if (assembly is null)
                {
                    throw new NullReferenceException();
                }
                var startup = assembly.GetExportedTypes().FirstOrDefault(t => t.GetInterfaces().Any( a => a == typeof(IModule)));
                if (startup is not null)
                {

                    var moduleInfo =new ModuleInfo()
                    {
                        Assembly = assembly,
                        Name = assembly.GetCustomAttribute<AssemblyTitleAttribute>()?.Title??"",
                        AssemblyName = assembly.FullName??"",
                        Module = (IModule)Activator.CreateInstance(startup)
                    };       


                    list.Add(moduleInfo);             
                }

            }
            catch (System.Exception ex)
            {
                if (!skip)
                {
                    list.Add(new ModuleInfo()
                    {
                        Exception = ex
                    });
                }
            }
        }
        return list;
    }

}
