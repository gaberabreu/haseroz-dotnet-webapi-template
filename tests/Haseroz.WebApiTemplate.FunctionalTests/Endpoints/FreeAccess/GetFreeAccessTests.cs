using System.Net;
using Haseroz.WebApiTemplate.FunctionalTests.Utils;
using Haseroz.WebApiTemplate.FunctionalTests.Utils.Extensions;
using Xunit.Abstractions;

namespace Haseroz.WebApiTemplate.FunctionalTests.Endpoints.FreeAccess;

public class GetFreeAccessTests(FunctionalTestFactory factory, ITestOutputHelper output) : IClassFixture<FunctionalTestFactory>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task Given_Get_When_Called_Then_ReturnNoContent()
    {
        // Act
        var response = await _client.ExecuteGetAsync("api/v1/free-access", output);

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }
}
