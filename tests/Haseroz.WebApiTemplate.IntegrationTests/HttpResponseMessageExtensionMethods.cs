using Xunit.Abstractions;

namespace Haseroz.WebApiTemplate.IntegrationTests;

public static class HttpResponseMessageExtensionMethods
{
    public static async Task<T> DeserializeAsync<T>(this HttpResponseMessage response, ITestOutputHelper? output = null)
    {
        var stringResponse = await response.Content.ReadAsStringAsync();
        var result = stringResponse.FromJsonToModel<T>();
        output?.WriteLine("Result: {0}", result);
        return result ?? throw new HttpRequestException($"Error deserializing response body. Response body: {stringResponse}");
    }
}
