using Microsoft.AspNetCore.Routing;

namespace SilkCMS.Abstractions.Navigation;

public interface IMenuNode
{
    public int Id { get; }
    public string Title { get; }
    public string Icon { get; }
    public RouteValueDictionary RouteValues { get; }

    public IEnumerable<IMenuNode> MenuNodes { get; }

    IMenuNode Set(string title, string icon, RouteValueDictionary routeValues);

    IMenuNode Set(int id, string title, string icon, RouteValueDictionary routeValues);

    IMenuNode Add(Action<IMenuNode> buildNode);

}
