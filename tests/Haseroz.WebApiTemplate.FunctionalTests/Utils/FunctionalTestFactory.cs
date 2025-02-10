using System.Text;
using Haseroz.WebApiTemplate.FunctionalTests.Utils.Fakes.HealthCheck;
using Haseroz.WebApiTemplate.FunctionalTests.Utils.Fakes.Security;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Haseroz.WebApiTemplate.FunctionalTests.Utils;

public class FunctionalTestFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            FakeHealthCheckServices(services);
            FakeAuthenticationServices(services);
        });
    }

    private static void FakeHealthCheckServices(IServiceCollection services)
    {
        services.Configure<HealthCheckServiceOptions>(options =>
        {
            options.Registrations.Clear();
        });

        services.AddHealthChecks().AddCheck<FakeKeycloakCheck>("keycloak");
    }

    private static void FakeAuthenticationServices(IServiceCollection services)
    {
        var authDescriptors = services.Where(s => s.ServiceType == typeof(IConfigureOptions<AuthenticationOptions>)).ToList();
        foreach (var descriptor in authDescriptors)
        {
            services.Remove(descriptor);
        }

        var jwtDescriptor = services.SingleOrDefault(s => s.ServiceType == typeof(JwtBearerOptions));
        if (jwtDescriptor != null)
        {
            services.Remove(jwtDescriptor);
        }

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "test-issuer",
                    ValidAudience = "test-audience",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(FakeTokenService.SecretKey))
                };
            });

        services.AddAuthorization();
    }
}
