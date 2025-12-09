namespace SilkCMS.Abstractions.Navigation;

public interface IMenuBuilder
{
    IEnumerable<IMenuNode> GetNodes();
    
    IMenuNode AddMenuNode(int parentId, Action<IMenuNode> node);

    IMenuBuilder AddRootMenuNode(Action<IMenuNode> node);
}
