using SilkCMS.Administrator.Plugin.Data.Menu;

namespace SilkCMS.Administrator.Plugin.MenuBuilder;

public class MenuBuilder
{

    private List<MenuItem> _groups=new();

    public IReadOnlyList<MenuItem> Groups => _groups.AsReadOnly();

    public MenuBuilder AddItems(IEnumerable<MenuItem> items)
    {
        _groups.AddRange(items);
        return this;
    }
   

    
}