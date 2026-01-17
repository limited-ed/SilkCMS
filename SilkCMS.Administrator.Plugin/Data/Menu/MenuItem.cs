namespace SilkCMS.Administrator.Plugin.Data.Menu;

public class MenuItem
{
    public int Id { get; set; }
    public string Title { get; set; }
    public bool IsSeparator { get; set; }
    public string Link { get; set; }
    public string Description { get; set; }
    public List<MenuItem> Children { get; set; }
}