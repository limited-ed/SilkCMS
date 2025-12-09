using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Localization;
using SilkCMS.Abstractions.Navigation;
using SilkCMS.Core.Modules;
using SilkCMS.Core.Navigation;

namespace SilkCMS.Administrator.Module;

public class AdminMenuBuilder : IModuleMenuBuilder
{
    private readonly IStringLocalizer<AdminMenuBuilder> S;
    public AdminMenuBuilder(IStringLocalizer<AdminMenuBuilder> localizer)
    {
        S = localizer;
    }
    public void Build(IMenuBuilder menuBuilder)
    {
        menuBuilder.AddRootMenuNode(node => node.Set((int)MenuId.Content,S["Content"],"news", null)
            .Add(node => node.Set(S["Add"], "square-rounded-plus", new RouteValueDictionary()))
            .Add(node => node.Set(S["View"], "list-details", new RouteValueDictionary())))
        .AddRootMenuNode( node => node.Set((int)MenuId.Media, S["Media"],"files", null));

    }
}
