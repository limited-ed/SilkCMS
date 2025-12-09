using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using SilkCMS.Abstractions.Navigation;

namespace SilkCMS.Core.Navigation;

public class MenuNode : IMenuNode
{
    private readonly List<MenuNode> _menuNodes;
    public int Id { get; private set; }
    public string Title { get; private set; }
    public RouteValueDictionary RouteValues { get; private set; }
    public IEnumerable<IMenuNode> MenuNodes => _menuNodes;
    public string Icon { get; set; }

    public MenuNode()
    {
        _menuNodes = new List<MenuNode>();
    }

    public IMenuNode Set(string title, string icon,RouteValueDictionary routeValues)
    {
        Title = title;
        Icon = icon;
        RouteValues = routeValues;
        return this;
    }

    public IMenuNode Set(int id, string title, string icon,RouteValueDictionary routeValues)
    {
        Id = id;
        Title = title;
        Icon = icon;
        RouteValues = routeValues;
        return this;
    }

    public IMenuNode Add(Action<IMenuNode> buildNode)
    {
        var newId = _menuNodes.Count() + 1;
        var newNode = new MenuNode();
        buildNode?.Invoke(newNode);
        _menuNodes.Add(newNode);
        return this;
    }


}


