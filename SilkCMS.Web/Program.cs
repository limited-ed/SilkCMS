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
using Microsoft.Extensions.FileProviders;
using SilkCMS.Core.ControllerModelConversion;
using SilkCMS.Core.FileProviders;
using SilkCMS.Localization;
using SilkCMS.Web.Data;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.
var provider = configuration.GetValue("Provider", "SQLite");

builder.Services.AddDbContext<ApplicationDbContext>(
    options => _ = provider switch
    {
        "SQLite" => options.UseSqlite(configuration.GetConnectionString("SQLite"), b => b.MigrationsAssembly("SilkCMS.SQLite")),
        _ => throw new Exception($"Unsupported provider: {provider}")
    }
);
builder.Services.AddDatabaseDeveloperPageExceptionFilter();


builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

var path = Path.Combine(Environment.CurrentDirectory, builder.Configuration["Modules:Path"]);
builder.Services
    .AddMvc(options =>
    {
        options.Conventions.Add(new PrivateControllerModelConversion());    
    })
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
    .AddModularity(path, builder.Configuration);

builder.Services.AddEmbeddedFiles();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddGoogle(googleOptions =>
{
    googleOptions.ClientId = configuration["Authentication:Google:ClientId"];
    googleOptions.ClientSecret = configuration["Authentication:Google:ClientSecret"];
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Administrator", p => p.RequireRole("Administrator"));
});

builder.Services.AddIdentity<User, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.Configure<RazorViewEngineOptions>(options =>
{
    options.ViewLocationFormats.Add("/Database/{0}"+RazorViewEngine.ViewExtension);
});
builder.Services.AddControllersWithViews()
    .AddDataAnnotationsLocalization(opt =>
    {
        opt.DataAnnotationLocalizerProvider = (type, factory) => factory.Create(typeof(LocalizationResource));
    })
    .AddRazorRuntimeCompilation(options =>
{
    var context=builder.Services.BuildServiceProvider().GetService<ApplicationDbContext>();
    options.FileProviders.Add(new DbFileProvider(context));
});
builder.Services.AddRazorPages();
builder.Services.AddModules();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Login";
    options.LogoutPath = "/Logout";
    options.ExpireTimeSpan = TimeSpan.FromHours(24);
});

var app = builder.Build();

var scope = app.Services.CreateScope();

var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
context.Database.Migrate();

var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
if (!roleManager.Roles.Any())
{
    var role = new IdentityRole()
    {
        Name = "Administrator"
    };
    await roleManager.CreateAsync(role);
}

if (!userManager.Users.Any())
{
    var user = new User() { UserName = "admin@site.com" };
    var result = await userManager.CreateAsync(user, "Qazplm-1");
    await userManager.AddToRoleAsync(user, "Administrator");
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

app.UseModules();

app.MapControllerRoute(
    name: "areaRoute",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.MapRazorPages();

app.Run();