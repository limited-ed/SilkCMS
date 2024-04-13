using Microsoft.Extensions.DependencyInjection;

namespace SilkCMS.Core;

public static class EmbeddedFilesExtensions
{
    public static void AddEmbeddedFiles(this IServiceCollection services)
    {
        services.ConfigureOptions(typeof(EmbeddedFileConfigureOptions));
    }
}