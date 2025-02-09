using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Haseroz.WebApiTemplate.Web.Identity;

internal static class AuthenticationExtensions
{
    private const string ConfigKey = "KeycloakConfig";
    private const string ResourceAccess = "resource_access";
    private const string Roles = "roles";

    internal static IServiceCollection AddAuthenticationConfigs(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<KeycloakConfig>(configuration.GetSection(ConfigKey));

        var config = configuration.GetSection(ConfigKey).Get<KeycloakConfig>()!;

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = config.Authority;
                options.Audience = config.ClientId;
                options.RequireHttpsMetadata = false;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true
                };

                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        var user = context.Principal;
                        var identity = user!.Identity as ClaimsIdentity;

                        var resourceAccess = user.FindFirst(ResourceAccess)?.Value;

                        if (!string.IsNullOrEmpty(resourceAccess))
                        {
                            using var doc = JsonDocument.Parse(resourceAccess);

                            if (doc.RootElement.TryGetProperty(config.ClientId, out var webApiTemplate))
                            {
                                if (webApiTemplate.TryGetProperty(Roles, out var roles))
                                {
                                    foreach (var role in roles.EnumerateArray())
                                    {
                                        identity!.AddClaim(new Claim(ClaimTypes.Role, role.GetString()!));
                                    }
                                }
                            }
                        }

                        return Task.CompletedTask;
                    }
                };
            });

        return services;
    }
}
