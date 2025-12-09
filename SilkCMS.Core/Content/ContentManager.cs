using SilkCMS.Data;

namespace SilkCMS.Core;

public class ContentManager
{
    private readonly List<ContentBase> _contentList = new();

    public ContentManager(ILiteDbContext liteDbContext)
    {

    }


}
