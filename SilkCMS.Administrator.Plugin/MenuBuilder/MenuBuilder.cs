using SilkCMS.Administrator.Plugin.Data.Menu;

namespace SilkCMS.Administrator.Plugin.MenuBuilder;

public class MenuBuilder
{

    private List<MenuGroup> _groups;

    public IReadOnlyList<MenuGroup> Groups => _groups.AsReadOnly();

    public MenuBuilder AddGroup(MenuGroup group)
    {
        _groups.Add(group);
        return this;
    }
    
    public List<MenuGroup> GetGroups()
    {
        return new List<MenuGroup>()
        {
            new() { Id = 1, Title = "General" },
            new() { Id = 2, Title = "Administrator" }
        };
    }
    
}