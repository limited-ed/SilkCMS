using SilkCMS.Core.Modules;

namespace SilkCMS.Core.Modules;

public class ModuleManager
{
    public static ModuleManager Current { get; } = new ModuleManager();

    private IModuleLoader _moduleLoader;

    private List<IModuleInfo> _modules;

    public List<IModuleInfo> ModulesInfo
    {
        get
        {
            return _modules;
        }
    }

    private ModuleManager()
    {
        _moduleLoader = new ModuleLoader();
    }

    public void LoadModules(string path)
    {
        _modules = _moduleLoader.LoadFromFolder(path);
    }

}
