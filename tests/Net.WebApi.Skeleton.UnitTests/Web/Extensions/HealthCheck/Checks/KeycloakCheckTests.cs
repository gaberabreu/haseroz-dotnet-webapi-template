using System.Net;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using Net.WebApi.Skeleton.Web.Extensions.HealthCheck.Checks;
using Net.WebApi.Skeleton.Web.Extensions.Security;

namespace Net.WebApi.Skeleton.UnitTests.Web.Extensions.HealthCheck.Checks;

public class KeycloakCheckTests
{
    private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;
    private readonly HttpClient _httpClient;
    private readonly IOptions<KeycloakSettings> _keycloakOptions;
    private readonly KeycloakCheck _keycloakCheck;

    public KeycloakCheckTests()
    {
        _httpMessageHandlerMock = new Mock<HttpMessageHandler>();

        _httpClient = new HttpClient(_httpMessageHandlerMock.Object);

        _keycloakOptions = Options.Create(new KeycloakSettings
        {
            AuthServerUrl = "http://localhost:18080",
            Realm = "realm-test",
            ClientId = "client-test"
        });

        _keycloakCheck = new KeycloakCheck(_keycloakOptions, _httpClient);
    }

    [Fact]
    public async Task Given_KeycloakIsAvailable_When_CheckHealth_Then_ReturnHealthy()
    {
        // Arrange
        _httpMessageHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK));

        // Act
        var result = await _keycloakCheck.CheckHealthAsync(new HealthCheckContext());

        // Assert
        Assert.Equal(HealthStatus.Healthy, result.Status);
        Assert.Equal("Keycloak is available.", result.Description);
    }

    [Fact]
    public async Task Given_KeycloakIsUnreachable_When_CheckHealth_Then_ReturnUnhealthy()
    {
        // Arrange
        _httpMessageHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.ServiceUnavailable));

        // Act
        var result = await _keycloakCheck.CheckHealthAsync(new HealthCheckContext());

        // Assert
        Assert.Equal(HealthStatus.Unhealthy, result.Status);
        Assert.Equal("Keycloak is unreachable.", result.Description);
    }

    [Fact]
    public async Task Given_KeycloakThrowsException_When_CheckHealth_Then_ReturnUnhealthy()
    {
        // Arrange
        var exception = new HttpRequestException("Connection failed");
        _httpMessageHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ThrowsAsync(exception);

        // Act
        var result = await _keycloakCheck.CheckHealthAsync(new HealthCheckContext());

        // Assert
        Assert.Equal(HealthStatus.Unhealthy, result.Status);
        Assert.Equal("Keycloak connection failed.", result.Description);
        Assert.Equal(exception, result.Exception);
    }
}
