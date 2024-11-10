using MassTransit;
using MassTransit.Transports;
using Shared.MassTransits.Enums;

namespace Shared.MassTransits.Core;

public interface ISendEndpointCustomProvider : ISendEndpointProvider
{
    Task SendMessage<T>(object message, ExchangeType type, CancellationToken cancellationToken) where T : class;
}