using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SilkCMS.Data;
using SilkCMS.Core;
using SilkCMS.Core.Modules;
using SilkCMS.Data.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using Microsoft.AspNetCore.Mvc.Razor;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
/*var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<DbContext>(options =>
    options.UseSqlite(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();*/


builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

var path = Path.Combine(Environment.CurrentDirectory, builder.Configuration["Modules:Path"]);
builder.Services.AddMvc().AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix).AddModularity(path, builder.Configuration);
builder.Services.AddEmbeddedFiles();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme);
 builder.Services.AddAuthorization( options =>
 {
    options.AddPolicy("Administartor", p => p.RequireRole("Administrator"));
 });

builder.Services.AddIdentity<User,Role>(options => options.SignIn.RequireConfirmedAccount = false)
.AddLiteDBIdentity(options => {
    options.Database = builder.Configuration.GetConnectionString("Connection");
});

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddRazorPages();
builder.Services.AddModules();

var app = builder.Build();


var scope = app.Services.CreateScope();

var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
if (!userManager.Users.Any())
{
    var user=new User() { UserName="admin@site.com"};
   var result = await userManager.CreateAsync(user, "Qazplm-1");

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
    app.UseHsts();
}

app.UseHttpsRedirection();

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
