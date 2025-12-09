using SilkCMS.Abstractions.Navigation;

namespace SilkCMS.Core.Modules;

public interface IModuleMenuBuilder
{
    void Build(IMenuBuilder menuBuilder);
}
