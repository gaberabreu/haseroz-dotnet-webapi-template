namespace Haseroz.WebApiTemplate.Web.Configurations.Security;

internal static class AuthorizationConfigs
{
    internal static IServiceCollection AddAuthorizationConfigs(this IServiceCollection services)
    {
        return services.AddAuthorization();
    }
}
