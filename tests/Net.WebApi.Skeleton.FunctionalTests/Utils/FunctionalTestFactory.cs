using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Net.WebApi.Skeleton.FunctionalTests.Utils.Fakes.Security;
using Net.WebApi.Skeleton.Infrastructure.Data;

namespace Net.WebApi.Skeleton.FunctionalTests.Utils;

public class FunctionalTestFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            FakeInfrastructureServices(services);
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

        services.AddHealthChecks()
            .AddDbContextCheck<AppDbContext>(
                name: "Database",
                tags: ["readiness"],
                failureStatus: HealthStatus.Unhealthy);
    }

    private static void FakeAuthenticationServices(IServiceCollection services)
    {
        var authDescriptors = services
            .Where(s => s.ServiceType == typeof(IConfigureOptions<AuthenticationOptions>))
            .ToList();

        foreach (var descriptor in authDescriptors)
        {
            services.Remove(descriptor);
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

    private static void FakeInfrastructureServices(IServiceCollection services)
    {
        var descriptorsToRemove = services
            .Where(s => s.ServiceType == typeof(AppDbContext) ||
                        s.ServiceType == typeof(DbContextOptions<AppDbContext>) ||
                        s.ServiceType.FullName!.Contains("EntityFrameworkCore"))
            .ToList();

        foreach (var descriptor in descriptorsToRemove)
        {
            services.Remove(descriptor);
        }

        services.AddDbContext<AppDbContext>(options =>
            options.UseInMemoryDatabase("FunctionalTest"));
    }
}
