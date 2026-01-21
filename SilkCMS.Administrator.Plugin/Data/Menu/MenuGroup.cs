namespace SilkCMS.Administrator.Plugin.Data.Menu;

public class MenuGroup
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Icon { get; set; }
    public List<MenuItem> Children { get; set; }
}