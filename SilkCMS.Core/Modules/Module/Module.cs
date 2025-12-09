using System.Reflection;
using SilkCMS.Core.Modules;


namespace SilkCMS.Core.Modules;

public class ModuleInfo : IModuleInfo
{
    public string Name { get; set; }

    public string AssemblyName { get; set; }

    public Assembly Assembly { get; set; }

    public Exception Exception { get; set; }
    public IModule Module { get ; set; }
    public IModuleMenuBuilder ModuleMenuBuilder { get ; set; }
}

