using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using SilkCMS.Web.Data;

namespace SilkCMS.Core.FileProviders;

public class DbFileInfo : IFileInfo
{
    private string _viewPath;
    private readonly byte[] _viewContent;
    private readonly DateTimeOffset _lastModified;
    private readonly bool _exists;


    public bool Exists => _exists;
    public bool IsDirectory => false;
    public DateTimeOffset LastModified => _lastModified;
    public long Length => _viewContent.Length;
    public string Name => _viewPath;
    public string PhysicalPath =>  null;
    
    
    public DbFileInfo(ApplicationDbContext context, string subpath)
    {
       var view = context.Views.FirstOrDefault(v => v.Location == subpath);
       _exists = view != null;
       if (view is not null)
       {
           _viewContent = Encoding.UTF8.GetBytes(view.Content);
           _lastModified = view.Modified;
           view.Requested = DateTime.Now;
           context.Entry(view).State = EntityState.Modified;
           context.SaveChanges();
       }
    }
    
    
    public Stream CreateReadStream()
    {
        return new MemoryStream(_viewContent);
    }


}