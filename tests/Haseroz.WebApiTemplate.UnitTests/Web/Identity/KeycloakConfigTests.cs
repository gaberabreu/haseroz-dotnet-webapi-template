using Haseroz.WebApiTemplate.Web.Identity;

namespace Haseroz.WebApiTemplate.UnitTests.Web.Identity;

public class KeycloakConfigTests
{
    [Fact]
    public void GIVEN_ValidProperties_WHEN_CreatingInstance_THEN_ValuesAreSetProperly()
    {
        // Arrange
        var authServerUrl = "http://localhost:18080";
        var realm = "realm-test";
        var clientId = "client-test";
        var clientSecret = "client-secret-test";
        var authority = "http://localhost:18080/realms/realm-test";
        var authorizationUrl = "http://localhost:18080/realms/realm-test/protocol/openid-connect/auth";
        var tokenUrl = "http://localhost:18080/realms/realm-test/protocol/openid-connect/token";

        // Act
        var keycloakConfig = new KeycloakConfig
        {
            AuthServerUrl = authServerUrl,
            Realm = realm,
            ClientId = clientId,
            ClientSecret = clientSecret
        };

        // Assert
        Assert.Equal(authServerUrl, keycloakConfig.AuthServerUrl);
        Assert.Equal(realm, keycloakConfig.Realm);
        Assert.Equal(clientId, keycloakConfig.ClientId);
        Assert.Equal(clientSecret, keycloakConfig.ClientSecret);
        Assert.Equal(authority, keycloakConfig.Authority);
        Assert.Equal(authorizationUrl, keycloakConfig.AuthorizationUrl);
        Assert.Equal(tokenUrl, keycloakConfig.TokenUrl);
    }
}
