using System.Collections.Concurrent;
using System.Globalization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;

namespace SilkCMS.Core.Localization;

public class StringLocalizerFactory : IStringLocalizerFactory
{

    private readonly ConcurrentDictionary<string, JsonStringLocalizer> _localizerCache = new();
    private readonly IConfiguration _configuration;

    public StringLocalizerFactory(IConfiguration configuration)
    {
        _configuration = configuration;

    }
    public IStringLocalizer Create(Type resourceSource)
    {
        if (resourceSource.Name != "Controller" && resourceSource.Name != "ControllerBase")
        {
            var path = Path.Combine(Path.GetDirectoryName(resourceSource.Assembly.Location), CultureInfo.CurrentCulture.Name, $"{resourceSource.Name}.json");
            var jsonReader = new JsonReader(path);
            var key = $"culture={CultureInfo.CurrentCulture.Name},type={resourceSource.FullName}";
            return _localizerCache.GetOrAdd(key, new JsonStringLocalizer(jsonReader));
        }

        return new JsonStringLocalizer();

    }

    public IStringLocalizer Create(string baseName, string location)
    {
        var workDir = Environment.CurrentDirectory;
        baseName=baseName.Replace(location, "");
        var path = Path.Combine(workDir,"Localization", CultureInfo.CurrentCulture.Name, $"{baseName}.json");
        var jsonReader = new JsonReader(path);
        var key = $"culture={CultureInfo.CurrentCulture.Name},baseName={baseName},lacation={location}";
        return _localizerCache.GetOrAdd(key, new JsonStringLocalizer(jsonReader));
    }
}
