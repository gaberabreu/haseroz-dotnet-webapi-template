using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Net.WebApi.Skeleton.Web.Configurations.Security;

public static class AuthenticationConfigs
{
    public static IServiceCollection AddAuthenticationConfigs(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<KeycloakSettings>(configuration.GetSection(KeycloakSettings.Identifier));
        var config = configuration.GetSection(KeycloakSettings.Identifier).Get<KeycloakSettings>()!;

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
            });

        return services;
    }
}
