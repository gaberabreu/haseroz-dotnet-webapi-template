namespace Haseroz.WebApiTemplate.Web.Identity;

public class KeycloakConfig
{
    public required string AuthServerUrl { get; init; }
    public required string Realm { get; init; }
    public required string ClientId { get; init; }
    public required string ClientSecret { get; init; }

    public string Authority => $"{AuthServerUrl}/realms/{Realm}";
    public string AuthorizationUrl => $"{AuthServerUrl}/realms/{Realm}/protocol/openid-connect/auth";
    public string TokenUrl => $"{AuthServerUrl}/realms/{Realm}/protocol/openid-connect/token";
}
