using System.Collections;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;
using SilkCMS.Web.Data;

namespace SilkCMS.Core.FileProviders;

public class DbFileProvider: IFileProvider
{
    class NotExists: IDirectoryContents
    {
        public IEnumerator<IFileInfo> GetEnumerator()
        {
            return new List<IFileInfo>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool Exists => false;
    }
    

    private readonly ApplicationDbContext _context;
    
    public DbFileProvider(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public IDirectoryContents GetDirectoryContents(string subpath)
    {
        return new NotExists();
    }

    public IFileInfo GetFileInfo(string subpath)
    {
        var result = new DbFileInfo(_context, subpath);
        return result.Exists ? result as IFileInfo : new NotFoundFileInfo(subpath);
    }

    public IChangeToken Watch(string filter)
    {
        return  new DbChangeToken();
    }
}