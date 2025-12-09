using LiteDB;

namespace SilkCMS.Core;

public abstract class ContentBase
{
    public ObjectId Id { get; set; }
    public DateTime CreatedDate { get; set; }
    private bool Published { get; set; }
    public ObjectId Author { get; set; }

    public abstract void Register();

}
