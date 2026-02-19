namespace SilkCMS.Administrator.Plugin.Data.Menu;

public class MenuItem
{
    public int Order { get; set; }
    public string Title { get; set; }
    public bool IsSeparator { get; set; }
    public bool IsHeader { get; set; }
    public string Icon { get; set; }
    public string Link { get; set; }
    public string Description { get; set; }
    public List<MenuItem> Children { get; set; }
}