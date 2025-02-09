using Haseroz.WebApiTemplate.Web.Identity;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Haseroz.WebApiTemplate.Web.Docs.OpenApi;

internal class ConfigureKeycloakAuthentication(IOptions<KeycloakConfig> keycloakConfig) : IConfigureOptions<SwaggerGenOptions>
{
    private const string SecurityDefinition = "Keycloak";

    public void Configure(SwaggerGenOptions options)
    {
        var securitySchema = new OpenApiSecurityScheme
        {
            Description = "Keycloak Authorization Header",
            Name = "Bearer",
            Type = SecuritySchemeType.OAuth2,
            Flows = new OpenApiOAuthFlows
            {
                AuthorizationCode = new OpenApiOAuthFlow
                {
                    AuthorizationUrl = new Uri(keycloakConfig.Value.AuthorizationUrl),
                    TokenUrl = new Uri(keycloakConfig.Value.TokenUrl),
                    Scopes = new Dictionary<string, string>
                    {
                        { "openid", "openid" },
                        { "profile", "profile" }
                    }
                }
            },
            In = ParameterLocation.Header,
            Scheme = "oauth2",
            BearerFormat = "JWT",
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = SecurityDefinition
            }
        };

        var securityRequirement = new OpenApiSecurityRequirement
        {
            {
                securitySchema, new[] { SecurityDefinition }
            }
        };

        options.AddSecurityDefinition(SecurityDefinition, securitySchema);
        options.AddSecurityRequirement(securityRequirement);
    }
}
