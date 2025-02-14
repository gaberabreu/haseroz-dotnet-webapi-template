using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Net.WebApi.Skeleton.Infrastructure.Data;

namespace Net.WebApi.Skeleton.Infrastructure;

public static class InfrastructureServiceExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer("name=ConnectionStrings:SqlServer"));

        return services;
    }
}
