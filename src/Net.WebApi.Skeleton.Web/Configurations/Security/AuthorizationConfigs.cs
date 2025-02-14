namespace Net.WebApi.Skeleton.Web.Configurations.Security;

public static class AuthorizationConfigs
{
    public static IServiceCollection AddAuthorizationConfigs(this IServiceCollection services)
    {
        return services.AddAuthorization();
    }
}
