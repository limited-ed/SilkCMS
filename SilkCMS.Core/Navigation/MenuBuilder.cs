using Microsoft.AspNetCore.Routing;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.Extensions.DependencyInjection;
using SilkCMS.Abstractions.Navigation;
using SilkCMS.Core.Modules;

namespace SilkCMS.Core.Navigation;

public class MenuBuilder : IMenuBuilder
{
    private List<MenuNode> _menuNodes;

    public MenuBuilder(IEnumerable<IModuleMenuBuilder> moduleMenuBuilders)
    {
        _menuNodes = new List<MenuNode>();
        foreach (var moduleMenuBuilder in moduleMenuBuilders)
        {
            moduleMenuBuilder.Build(this);
        }
    }
    public IMenuNode AddMenuNode(int parentId, Action<IMenuNode> node)
    {
        var parentNode = _menuNodes.FirstOrDefault(f => f.Id == parentId);
        if (parentNode is null)
        {
            throw new ArgumentException($"Parent \"ParentId:{parentId}\" not found");
        }
        var newId =parentNode.MenuNodes.Select(s => s.Id).Max()+1;
        var newNode = new MenuNode();
        node?.Invoke(newNode);
        //parentNode. MenuNodes.Add(newNode);
        return newNode;
    }

    public IMenuBuilder AddRootMenuNode(Action<IMenuNode> node)
    {
        var menuNode = new MenuNode();
        node?.Invoke(menuNode);
        if (_menuNodes.Any(f => f.Id == menuNode.Id))
        {
            throw new ArgumentException($"Node \"Id:{menuNode.Id}\" is already exists");
        }
        _menuNodes.Add(menuNode);
        return this;
    }

    public IEnumerable<IMenuNode> GetNodes()
    {
        return _menuNodes;
    }
}

public static class MenuBuilderExtensions
{
    public static IServiceCollection AddAdminMenu(this IServiceCollection services)
    {
        services.AddSingleton<IMenuBuilder, MenuBuilder>();

        return services;
    }

}