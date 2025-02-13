namespace Net.WebApi.Skeleton.Web.Configurations.Security;

internal static class AuthorizationConfigs
{
    internal static IServiceCollection AddAuthorizationConfigs(this IServiceCollection services)
    {
        return services.AddAuthorization();
    }
}
