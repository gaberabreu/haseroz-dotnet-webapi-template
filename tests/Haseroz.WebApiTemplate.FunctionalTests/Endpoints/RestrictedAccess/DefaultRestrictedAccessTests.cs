using System.Net;
using System.Net.Http.Headers;
using Haseroz.WebApiTemplate.FunctionalTests.Utils;
using Haseroz.WebApiTemplate.FunctionalTests.Utils.Extensions;
using Haseroz.WebApiTemplate.FunctionalTests.Utils.Fakes.Security;
using Xunit.Abstractions;

namespace Haseroz.WebApiTemplate.FunctionalTests.Endpoints.RestrictedAccess;

public class DefaultRestrictedAccessTests(FunctionalTestFactory factory, ITestOutputHelper output) : IClassFixture<FunctionalTestFactory>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task Given_AuthenticatedUser_Then_ReturnNoContent()
    {
        // Arrange
        _client.AddAuthenticationHeader();

        // Act
        var response = await _client.ExecuteGetAsync("api/v1/restricted-access", output);

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task Given_NotAuthenticatedUser_Then_ReturnUnauthorized()
    {
        // Act
        var response = await _client.ExecuteGetAsync("api/v1/restricted-access", output);

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}
