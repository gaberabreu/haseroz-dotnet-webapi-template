namespace Net.WebApi.Skeleton.Web.Extensions.Security;

public static class AuthorizationServiceExtensions
{
    public static IServiceCollection AddAuthorizationConfigs(this IServiceCollection services)
    {
        return services.AddAuthorization();
    }
}
