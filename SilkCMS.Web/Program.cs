using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SilkCMS.Data;
using SilkCMS.Core;
using SilkCMS.Core.Modules;
using SilkCMS.Data.Identity;
using SilkCMS.Core.Navigation;
using SilkCMS.Core.Localization;

using System.Globalization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Localization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
/*var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<DbContext>(options =>
    options.UseSqlite(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();*/




var path = Path.Combine(Environment.CurrentDirectory, builder.Configuration["Modules:Path"]);
builder.Services.AddLocalizationCore();
builder.Services.AddMvc().AddMvcLocalization().AddModularity(path, builder.Configuration);
builder.Services.AddEmbeddedFiles();


builder.Services.AddIdentity<User, Role>(options => options.SignIn.RequireConfirmedAccount = false)
.AddLiteDBIdentity(options =>
{
    options.Database = builder.Configuration.GetConnectionString("Connection");
});

builder.Services.AddAuthentication().AddCookie(options => {
    options.LoginPath = "/Login";
    options.LogoutPath = "/Logout";
});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Administrator", p => p.RequireRole("Administrator"));
});


builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddRazorPages().AddDataAnnotationsLocalization();
builder.Services.AddModules().AddAdminMenu();


var app = builder.Build();


var scope = app.Services.CreateScope();

var fact =  scope.ServiceProvider.GetServices<IStringLocalizerFactory>();
var f = scope.ServiceProvider.GetService<IStringLocalizerFactory>();

var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
if (!userManager.Users.Any())
{
    var user = new User() { UserName = "admin@site.com" };
    var result = await userManager.CreateAsync(user, "Qazplm-1");
    var roleManager = scope.ServiceProvider.GetService<RoleManager<Role>>();
    result = await roleManager.CreateAsync(new Role() { Name = "Administrator" });
    result = await userManager.AddToRoleAsync(user, "Administrator");
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
  //  app.UseHsts();
}

//app.UseHttpsRedirection();

var supportedCultures = new[]
            {
                new CultureInfo("en"),
                new CultureInfo("ru"),
            };

app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("ru"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
});

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();



app.Run();
