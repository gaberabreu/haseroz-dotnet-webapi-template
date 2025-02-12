using Haseroz.WebApiTemplate.Web.Configurations.Security;
using Microsoft.Extensions.Options;

namespace Haseroz.WebApiTemplate.Web.Configurations.HttpClient;

internal static class HttpClientConfigs
{
    internal static IServiceCollection AddHttpClientConfigs(this IServiceCollection services)
    {
        services.AddHttpClient(HttpClientNames.Keycloak, (serviceProvider, client) =>
        {
            var keycloakConfig = serviceProvider.GetRequiredService<IOptions<KeycloakSettings>>().Value;
            
            client.BaseAddress = new Uri(keycloakConfig.Authority);
        });

        return services;
    }
}
