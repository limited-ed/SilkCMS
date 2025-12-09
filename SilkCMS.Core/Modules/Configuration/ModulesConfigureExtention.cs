using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;


namespace SilkCMS.Core.Modules;

public static class ModulesConfigureEx
{
    public static IMvcBuilder AddModularity(this IMvcBuilder builder, string path, IConfiguration configuration)
    {

        var mm = ModuleManager.Current;
        mm.LoadModules(path);

        
        foreach (var module in mm.ModulesInfo)
        {
            var menuBuilderType = module.Assembly.GetExportedTypes().FirstOrDefault(t => t.GetInterfaces().Any(a => a == typeof(IModuleMenuBuilder)));
            if (menuBuilderType is not null)
            {
                builder.Services.AddTransient(typeof(IModuleMenuBuilder), menuBuilderType);
            }
        }
        builder.ConfigureApplicationPartManager(apm =>
        {
            foreach (var module in mm.ModulesInfo)
            {
                var parts = new ConsolidatedAssemblyApplicationPartFactory().GetApplicationParts(module.Assembly);
                foreach (var part in parts)
                {
                    apm.ApplicationParts.Add(part);
                }

            }
        });

        return builder;
    }

}
