using System.Reflection;

namespace SilkCMS.Core.Modules;

public interface IModuleInfo
{
    string Name { get; }
    string AssemblyName { get; }
    Assembly Assembly { get; }
    Exception Exception {get;}
    IModule Module {get; set;} 
}
