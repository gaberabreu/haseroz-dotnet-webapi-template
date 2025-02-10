using System.Net;
using Haseroz.WebApiTemplate.FunctionalTests.Utils;
using Haseroz.WebApiTemplate.FunctionalTests.Utils.Extensions;
using Xunit.Abstractions;

namespace Haseroz.WebApiTemplate.FunctionalTests.Endpoints.RestrictedAccess;

public class ReaderRestrictedAccessTests(FunctionalTestFactory factory, ITestOutputHelper output) : IClassFixture<FunctionalTestFactory>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task Given_AuthorizedUser_Then_ReturnNoContent()
    {
        // Arrange
        _client.AddAuthorizationHeader(role: "reader");

        // Act
        var response = await _client.ExecuteGetAsync("api/v1/restricted-access/reader", output);

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task Given_NotAuthorizedUser_Then_ReturnForbidden()
    {
        // Arrange
        _client.AddAuthenticationHeader();

        // Act
        var response = await _client.ExecuteGetAsync("api/v1/restricted-access/reader", output);

        // Assert
        Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
    }

    [Fact]
    public async Task Given_NotAuthenticatedUser_Then_ReturnUnauthorized()
    {
        // Act
        var response = await _client.ExecuteGetAsync("api/v1/restricted-access/reader", output);

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}
