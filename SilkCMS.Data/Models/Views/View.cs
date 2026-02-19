namespace SilkCMS.Data.Models.Views;

public class View
{
    public int Id { get; set; }
    public string Location { get; set; }
    public string Content { get; set; }
    public DateTime Modified { get; set; }
    public DateTime Requested { get; set; }
}