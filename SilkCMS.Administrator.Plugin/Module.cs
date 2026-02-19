using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SilkCMS.Administrator.Plugin.Data.Menu;
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

        
        var items = new List<MenuItem>()
        {
            new() { Order = 1, Title = "General", IsHeader = true, Children =
                [
                    new() { Order = 1, Title = "Settings" },
                    new() { Order = 2, Title = "Plugins" },
                    new() { Order = 3, Title = "Documentation" },
                    new() { Order = 4, Title = "About" }
                ]
            },
            new() { Order = 2, IsSeparator = true},
            new() { Order = 2, Title = "Users Administration", IsHeader = true, Children = 
                [
                    new ()
                    {
                        Order = 1, Title = "Users Administration", IsHeader = true, Children =
                        [ 
                            new () {Order = 1, Title = "Users Administration"},
                            new () {Order = 2, Title = "User Settings"}
                        ]
                    },
                    new ()
                    {
                        Order = 2, Title = "Groups Administration", Children = 
                        [
                            new() {Order = 1, Title = "Groups Administration"}
                        ]
                    },
                    new() {Order = 3, Title = "Registration providers", IsHeader = true}
                    
                ]
            }
        };
        var mb = app.Services.GetService<MenuBuilder.MenuBuilder>();
        mb.AddItems(items);
    }
}
