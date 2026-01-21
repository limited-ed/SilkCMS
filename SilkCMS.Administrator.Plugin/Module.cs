using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SilkCMS.Core.Modules;

namespace SilkCMS.Administrator.Plugin;

public class AdministratorModule : IModule
{
    public string Title => "Administration plugin";

    public string Description => "Administation plugin";

    public string Author => "Yaros Roman";

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddRazorPages(options =>
        {
            options.Conventions.AuthorizeAreaFolder("Private", "/", "Administrator");
        });
        services.AddSingleton<MenuBuilder.MenuBuilder>();
    }

    public void Configure(WebApplication app)
    {
        app.MapAreaControllerRoute(
            name: "Private",
            areaName: "Private",
            pattern: "Private/{controller=Home}/{action=Index}/{id?}");

        var mb = app.Services.GetService<MenuBuilder.MenuBuilder>();
        mb.AddGroup(new () { Id = 1, Title =  "Administration" });
    }
}
