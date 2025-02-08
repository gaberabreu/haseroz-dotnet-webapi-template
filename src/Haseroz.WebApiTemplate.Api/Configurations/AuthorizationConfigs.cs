namespace Haseroz.WebApiTemplate.Api.Configurations;

internal static class AuthorizationConfigs
{
    internal static IServiceCollection AddAuthorizationConfigs(this IServiceCollection services)
    {
        return services.AddAuthorization();
    }
}
