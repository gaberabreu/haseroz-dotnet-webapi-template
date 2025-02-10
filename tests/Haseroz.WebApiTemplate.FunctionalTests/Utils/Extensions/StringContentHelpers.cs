using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Haseroz.WebApiTemplate.FunctionalTests.Utils.Extensions;

public static class StringContentHelpers
{
    private static readonly JsonSerializerOptions DefaultJsonOptions = new()
    {
        WriteIndented = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public static StringContent FromModelToJson(this object model)
    {
        return new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, MediaTypeNames.Application.Json);
    }

    public static T? FromJsonToModel<T>(this string json)
    {
        return JsonSerializer.Deserialize<T>(json, DefaultJsonOptions);
    }
}