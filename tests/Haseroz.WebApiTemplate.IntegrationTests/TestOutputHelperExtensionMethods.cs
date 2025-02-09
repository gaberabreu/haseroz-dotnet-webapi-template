using System.Net;
using Xunit.Abstractions;

namespace Haseroz.WebApiTemplate.IntegrationTests;

public static class TestOutputHelperExtensionMethods
{
    public static void LogHttpRequest(this ITestOutputHelper output, string requestMethod, string requestPath, HttpStatusCode statusCode)
    {
        output.WriteLine("{0} {1} - Status: {2}", requestMethod, requestPath, statusCode);
    }
}
