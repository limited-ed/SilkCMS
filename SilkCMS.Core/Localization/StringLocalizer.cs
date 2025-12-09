using System.Collections.Concurrent;
using Microsoft.Extensions.Localization;

namespace SilkCMS.Core.Localization;

public class JsonStringLocalizer : IStringLocalizer
{
    private readonly ConcurrentDictionary<string, string> _localizedStrings;

    private readonly JsonReader _jsonReader;

    public JsonStringLocalizer()
    {
        var a=1;
    }

    public JsonStringLocalizer(JsonReader jsonReader)
    {
        _jsonReader = jsonReader;
        _localizedStrings = new ConcurrentDictionary<string, string>(_jsonReader.TryLoad());
    }    

    public LocalizedString this[string name] => Get(name);

    public LocalizedString this[string name, params object[] arguments] =>  Get(name);

    public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
    {
        return new List<LocalizedString>();
    }

    private LocalizedString Get(string name)
    {   
        return new LocalizedString(name, _localizedStrings.GetValueOrDefault(name)??name);
    }
    private LocalizedString Get(string name, params object[] arguments)
    {   
        return new LocalizedString(name, _localizedStrings.GetValueOrDefault(name)??name);
    }


}
