using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Localization;

namespace SilkCMS.Core.Localization;

public static class LocalizationExtensions
{
    public static IServiceCollection AddLocalizationCore(this IServiceCollection services)
    {
        AddLocalizationServices(services);
        return services;
    }

    public static IServiceCollection AddLocalizationCore(this IServiceCollection services, Action<LocalizationOptions> setupAction)
    {
        AddLocalizationServices(services);
        return services;
    }

    internal static void AddLocalizationServices(IServiceCollection services)
    {
        services.AddSingleton<IStringLocalizerFactory, StringLocalizerFactory>();
        services.TryAddTransient<IStringLocalizer, JsonStringLocalizer>();
        services.TryAddTransient(typeof(IStringLocalizer<>), typeof(StringLocalizer<>));
    }
}
