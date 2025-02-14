using Microsoft.Extensions.Options;
using Net.WebApi.Skeleton.Web.Configurations.Security;

namespace Net.WebApi.Skeleton.Web.Configurations.HttpClient;

public static class HttpClientConfigs
{
    public static IServiceCollection AddHttpClientConfigs(this IServiceCollection services)
    {
        services.AddHttpClient(HttpClientNames.Keycloak, (serviceProvider, client) =>
        {
            var keycloakConfig = serviceProvider.GetRequiredService<IOptions<KeycloakSettings>>().Value;

            client.BaseAddress = new Uri(keycloakConfig.Authority);
        });

        return services;
    }
}
