using Microsoft.Extensions.DependencyInjection;
using Polly;

namespace Shared.HttpClientCustom;

public static class HttpClientCustomExtensions
{

    #region Public Methods

    public static IServiceCollection AddHttpClientCustom<T>(this IServiceCollection services, T clientConfig) where T : ClientConfig
    {
        var clientName = $"{typeof(T).Name}{DateTime.UtcNow.Ticks}" ;

        var throttlerPolicy = Policy.BulkheadAsync<HttpResponseMessage>(10000, Int32.MaxValue);
        services.AddHttpClient<IHttpClientCustom<T>, HttpClientCustom<T>>(clientName, c =>
            {
                c.BaseAddress = new Uri(clientConfig.BaseUrl);
                c.DefaultRequestHeaders.Add("Accept", "application/json");
                c.Timeout = TimeSpan.FromMinutes(clientConfig.Timeout);
            })
            .AddPolicyHandler(throttlerPolicy)
            .ConfigurePrimaryHttpMessageHandler(() => new SocketsHttpHandler()
            {
                PooledConnectionLifetime = TimeSpan.FromMinutes(5),
                MaxConnectionsPerServer = 10000,
            })
            .SetHandlerLifetime(TimeSpan.FromMinutes(clientConfig.HttpClientTimeout * 2));

        return services;
    }
    
    #endregion
}