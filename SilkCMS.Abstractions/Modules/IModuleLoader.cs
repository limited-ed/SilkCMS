namespace SilkCMS.Core.Modules;

public interface IModuleLoader
{
    List<IModuleInfo> LoadFromFolder(string path, bool skip=true);
}
