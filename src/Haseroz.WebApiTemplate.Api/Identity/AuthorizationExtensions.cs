namespace Haseroz.WebApiTemplate.Api.Identity;

internal static class AuthorizationExtensions
{
    internal static IServiceCollection AddAuthorizationConfigs(this IServiceCollection services)
    {
        return services.AddAuthorization();
    }
}
