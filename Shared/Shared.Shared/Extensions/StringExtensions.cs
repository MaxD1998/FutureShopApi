using System.Text.Json;

namespace Shared.Shared.Extensions;

public static class StringExtensions
{
    public static List<string> ToListString(this string jsonStr)
    {
        if (string.IsNullOrWhiteSpace(jsonStr))
            return [];

        try
        {
            return JsonSerializer.Deserialize<List<string>>(jsonStr);
        }
        catch (Exception)
        {
            return [];
        }
    }

    public static Guid? ToNullableGuid(this string str)
        => Guid.TryParse(str, out var guid) ? guid : null;
}