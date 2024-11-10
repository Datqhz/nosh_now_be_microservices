using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Shared.HttpContextAccessor;

public static class HttpContextAccessorExtension
{
    public static IServiceCollection AddCustomHttpContextAccessor(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.TryAddSingleton<ICustomHttpContextAccessor, CustomHttpContextAccessor>();
        return services;
    }
}