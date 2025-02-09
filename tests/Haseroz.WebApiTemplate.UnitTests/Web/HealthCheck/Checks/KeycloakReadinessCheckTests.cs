using System.Net;
using Haseroz.WebApiTemplate.Web.HealthCheck.Checks;
using Haseroz.WebApiTemplate.Web.Identity;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;

namespace Haseroz.WebApiTemplate.UnitTests.Web.HealthCheck.Checks;

public class KeycloakReadinessCheckTests
{
    private readonly Mock<IHttpClientFactory> _httpClientFactoryMock;
    private readonly Mock<IOptions<KeycloakConfig>> _keycloakConfigMock;

    public KeycloakReadinessCheckTests()
    {
        _httpClientFactoryMock = new Mock<IHttpClientFactory>();
        _keycloakConfigMock = new Mock<IOptions<KeycloakConfig>>();

        _keycloakConfigMock.Setup(x => x.Value).Returns(new KeycloakConfig
        {
            AuthServerUrl = "http://localhost:18080",
            Realm = "realm-test",
            ClientId = "client-test",
        });
    }

    [Fact]
    public async Task GIVEN_KeycloakIsAvailable_WHEN_CheckHealth_THEN_ReturnHealthy()
    {
        // Arrange
        var handlerMock = new Mock<HttpMessageHandler>();

        handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage { StatusCode = HttpStatusCode.OK });

        var httpClient = new HttpClient(handlerMock.Object);
        _httpClientFactoryMock.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(httpClient);

        var healthCheck = new KeycloakReadinessCheck(_keycloakConfigMock.Object, _httpClientFactoryMock.Object);

        // Act
        var result = await healthCheck.CheckHealthAsync(new HealthCheckContext());

        // Assert
        Assert.Equal(HealthStatus.Healthy, result.Status);
        Assert.Equal("Keycloak is available.", result.Description);
    }

    [Fact]
    public async Task GIVEN_KeycloakIsUnreachable_WHEN_CheckHealth_THEN_ReturnUnhealthy()
    {
        // Arrange
        var handlerMock = new Mock<HttpMessageHandler>();
        handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage { StatusCode = HttpStatusCode.ServiceUnavailable });

        var httpClient = new HttpClient(handlerMock.Object);
        _httpClientFactoryMock.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(httpClient);

        var healthCheck = new KeycloakReadinessCheck(_keycloakConfigMock.Object, _httpClientFactoryMock.Object);

        // Act
        var result = await healthCheck.CheckHealthAsync(new HealthCheckContext());

        // Assert
        Assert.Equal(HealthStatus.Unhealthy, result.Status);
        Assert.Equal("Keycloak is unreachable.", result.Description);
    }


    [Fact]
    public async Task GIVEN_KeycloakConnectionFails_WHEN_CheckHealth_THEN_ReturnUnhealthy()
    {
        // Arrange
        var handlerMock = new Mock<HttpMessageHandler>();
        handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ThrowsAsync(new HttpRequestException("Connection failed"));

        var httpClient = new HttpClient(handlerMock.Object);
        _httpClientFactoryMock.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(httpClient);

        var healthCheck = new KeycloakReadinessCheck(_keycloakConfigMock.Object, _httpClientFactoryMock.Object);

        // Act
        var result = await healthCheck.CheckHealthAsync(new HealthCheckContext());

        // Assert
        Assert.Equal(HealthStatus.Unhealthy, result.Status);
        Assert.Equal("Keycloak connection failed.", result.Description);
    }
}
