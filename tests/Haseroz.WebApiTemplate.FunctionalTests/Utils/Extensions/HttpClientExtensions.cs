using System.Net.Http.Headers;
using Haseroz.WebApiTemplate.FunctionalTests.Utils.Fakes.Security;
using Xunit.Abstractions;

namespace Haseroz.WebApiTemplate.FunctionalTests.Utils.Extensions;

public static partial class HttpClientExtensions
{
    public static HttpClient AddAuthorizationHeader(this HttpClient client, string username = "test-user", string role = "test-role")
    {
        var token = FakeTokenService.GenerateToken(username, role);
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        return client;
    }

    public static HttpClient AddAuthenticationHeader(this HttpClient client, string username = "test-user")
    {
        var token = FakeTokenService.GenerateToken(username);
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        return client;
    }

    public static async Task<HttpResponseMessage> ExecuteGetAsync(this HttpClient client, string route, ITestOutputHelper? output = null)
    {
        var response = await client.GetAsync(route);
        output?.LogHttpRequest("GET", route, response.StatusCode);
        return response;
    }

    public static async Task<HttpResponseMessage> ExecutePostAsync(this HttpClient client, string route, object content, ITestOutputHelper? output = null)
    {
        var response = await client.PostAsync(route, content.FromModelToJson());
        output?.LogHttpRequest("POST", route, response.StatusCode);
        return response;
    }

    public static async Task<HttpResponseMessage> ExecutePutAsync(this HttpClient client, string route, object content, ITestOutputHelper? output = null)
    {
        var response = await client.PutAsync(route, content.FromModelToJson());
        output?.LogHttpRequest("PUT", route, response.StatusCode);
        return response;
    }

    public static async Task<HttpResponseMessage> ExecuteDeleteAsync(this HttpClient client, string route, ITestOutputHelper? output = null)
    {
        var response = await client.DeleteAsync(route);
        output?.LogHttpRequest("DELETE", route, response.StatusCode);
        return response;
    }
}
