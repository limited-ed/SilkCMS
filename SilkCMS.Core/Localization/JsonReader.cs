using System.Collections.Concurrent;
using System.Text.Json;

namespace SilkCMS.Core;

public class JsonReader
{
    private readonly string _path;

    public JsonReader(string path)
    {
        _path = path;
    }

    public IDictionary<string, string> TryLoad()
    {
        var dictionary = new Dictionary<string, string>();
        if (File.Exists(_path))
        {
            using var stream = new StreamReader(_path);
            using var json = JsonDocument.Parse(stream.BaseStream);

            foreach (var item in json.RootElement.EnumerateObject())
            {
                dictionary.Add(item.Name, item.Value.GetString());
            }
        }
        return dictionary;
    }



}
