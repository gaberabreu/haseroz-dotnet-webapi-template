using Xunit.Abstractions;

namespace Haseroz.WebApiTemplate.IntegrationTests;

public static partial class HttpClientExtensionMethods
{
    public static async Task<HttpResponseMessage> ExecuteGetAsync(
        this HttpClient client,
        string route,
        ITestOutputHelper? output = null)
    {
        var response = await client.GetAsync(route);
        output?.LogHttpRequest("GET", route, response.StatusCode);
        return response;
    }

    public static async Task<HttpResponseMessage> ExecutePostAsync(
        this HttpClient client,
        string route,
        object content,
        ITestOutputHelper? output = null)
    {
        var response = await client.PostAsync(route, content.FromModelToJson());
        output?.LogHttpRequest("POST", route, response.StatusCode);
        return response;
    }

    public static async Task<HttpResponseMessage> ExecutePutAsync(
        this HttpClient client,
        string route,
        object content,
        ITestOutputHelper? output = null)
    {
        var response = await client.PutAsync(route, content.FromModelToJson());
        output?.LogHttpRequest("PUT", route, response.StatusCode);
        return response;
    }

    public static async Task<HttpResponseMessage> ExecuteDeleteAsync(
        this HttpClient client,
        string route,
        ITestOutputHelper? output = null)
    {
        var response = await client.DeleteAsync(route);
        output?.LogHttpRequest("DELETE", route, response.StatusCode);
        return response;
    }
}
