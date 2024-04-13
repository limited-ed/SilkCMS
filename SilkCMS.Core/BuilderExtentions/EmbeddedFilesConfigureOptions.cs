using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using SilkCMS.Core.Modules;



namespace SilkCMS.Core;
public class EmbeddedFileConfigureOptions : IPostConfigureOptions<StaticFileOptions>
{
    private readonly IWebHostEnvironment _environment;
    public EmbeddedFileConfigureOptions(IWebHostEnvironment environment)
    {
        _environment = environment;
    }
    public void PostConfigure(string name, StaticFileOptions options)
    {
            // Basic initialization in case the options weren't initialized by any other component
            options.ContentTypeProvider = options.ContentTypeProvider ?? new FileExtensionContentTypeProvider();
            if (options.FileProvider == null && _environment.WebRootFileProvider == null)
            {
                throw new InvalidOperationException("Missing FileProvider.");
            }
            options.FileProvider = options.FileProvider ?? _environment.WebRootFileProvider;
            var fileProviders=new List<IFileProvider>();
            foreach(var module in ModuleManager.Current.ModulesInfo)
            {
                try
                {
                    fileProviders.Add(new ManifestEmbeddedFileProvider(module.Assembly,"wwwroot"));
                }
                catch (System.Exception)
                {

                }                
            }
            fileProviders.Add(options.FileProvider);
            options.FileProvider = new CompositeFileProvider(fileProviders.ToArray());
    }
}