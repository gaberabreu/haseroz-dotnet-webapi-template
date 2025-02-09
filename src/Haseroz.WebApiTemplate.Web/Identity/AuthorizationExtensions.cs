namespace Haseroz.WebApiTemplate.Web.Identity;

internal static class AuthorizationExtensions
{
    internal static IServiceCollection AddAuthorizationConfigs(this IServiceCollection services)
    {
        return services.AddAuthorization();
    }
}
