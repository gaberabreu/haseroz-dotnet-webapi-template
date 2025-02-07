using Haseroz.WebApiTemplate.Api.Docs;
using Haseroz.WebApiTemplate.Api.Services;

namespace Haseroz.WebApiTemplate.Api.Extensions;

internal static class ServiceCollectionExtensions
{
    internal static IServiceCollection AddDependencies(this IServiceCollection services)
    {
        services
            .AddControllers()
            .AddHttpContextAccessor()
            .AddSwaggerGen()
            .AddExceptionHandler<GlobalExceptionHandler>();

        services.AddHealthChecks();

        return services;
    }
}
