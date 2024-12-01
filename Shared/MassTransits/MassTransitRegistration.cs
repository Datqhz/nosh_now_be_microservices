using System.Reflection;
using MassTransit;
using MassTransit.Metadata;
using Microsoft.Extensions.DependencyInjection;
using Shared.MassTransits.Core;

namespace Shared.MassTransits;

public static class MassTransitRegistration
{
    public static IServiceCollection AddMassTransitRegistration(
            this IServiceCollection services,
            Assembly? assembly = null,
            Action<IBusRegistrationContext,  
            IRabbitMqBusFactoryConfigurator>? registrationConfigure = null)
    {
        services.AddMassTransit(x =>
        {
            x.SetKebabCaseEndpointNameFormatter();
            x.AddDelayedMessageScheduler();
            x.AddConsumers(Assembly.GetEntryAssembly());
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(HostMetadataCache.IsRunningInContainer ? "rabbitmq" : "localhost", 5672,"/", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });  
                cfg.UseDelayedMessageScheduler();
                registrationConfigure?.Invoke(context, cfg);
                cfg.ConfigureEndpoints(context);
            });
        });
       

        services.AddScoped<ISendEndpointCustomProvider, SendEndpointCustomProvider>();
        return services;
    }
}