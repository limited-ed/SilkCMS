using Microsoft.AspNetCore.Builder;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.DependencyInjection;
using SilkCMS.Core.Modules;

namespace SilkCMS.Core;

public static class ModuleStartupExtention
{
    public static IServiceCollection AddModules(this IServiceCollection services)
    {
        var mm = ModuleManager.Current;

        foreach (var module in mm.ModulesInfo)
        {
            module.Module.ConfigureServices(services);
        }

        return services;
    }

    public static IApplicationBuilder UseModules(this WebApplication app)
    {
        var mm = ModuleManager.Current;

        foreach (var module in mm.ModulesInfo)
        {
            module.Module.Configure(app);
        }

        return app;
    }

}
