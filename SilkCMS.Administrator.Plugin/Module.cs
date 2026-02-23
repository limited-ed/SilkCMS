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
            new MenuItem(){Order = 1, Title = "Content Management", IsHeader = true},
            new() { Order = 1, Title = "Content", Icon="lni-paperclip-1", Children =
                [
                    new() { Order = 1, Title = "Content types" },
                    new() { Order = 2, Title = "Add" },
                ]
            },
            new() { Order = 1, Title = "Media", Icon="lni-paperclip-1", Children =
                [
                    new() { Order = 1, Title = "Media gallery" },
                    new() { Order = 2, Title = "Upload media" },
                ]
            },
            new() { Order = 2, Title = "Security", IsHeader = true},
            new() { Order = 2, Title = "Users Administration", Icon = "lni-user-multiple-4" ,Children = 
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
                ]
            },
            new MenuItem(){Order = 1, Title = "Content Management", IsHeader = true},
            new() { Order = 1, Title = "General", Icon="lni-paperclip-1", Children =
                [
                    new() { Order = 1, Title = "Settings" },
                    new() { Order = 2, Title = "Plugins" },
                    new() { Order = 3, Title = "Documentation" },
                    new() { Order = 4, Title = "About" }
                ]
            },
        };
        var mb = app.Services.GetService<MenuBuilder.MenuBuilder>();
        mb.AddItems(items);
    }
}
