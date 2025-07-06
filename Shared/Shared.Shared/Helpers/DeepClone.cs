using System.Text.Json;

namespace Shared.Shared.Helpers;

public static class DeepClone
{
    public static T Clone<T>(T obj)
    {
        var json = JsonSerializer.Serialize(obj);
        return JsonSerializer.Deserialize<T>(json);
    }
}