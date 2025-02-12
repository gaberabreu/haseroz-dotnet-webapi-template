using System.Net;
using Xunit.Abstractions;

namespace Haseroz.WebApiTemplate.FunctionalTests.Utils.Extensions;

public static class TestOutputHelperExtensions
{
    public static void LogHttpRequest(this ITestOutputHelper output, string requestMethod, string requestPath, HttpStatusCode statusCode)
    {
        output.WriteLine("{0} {1} - Status: {2}", requestMethod, requestPath, statusCode);
    }
}
