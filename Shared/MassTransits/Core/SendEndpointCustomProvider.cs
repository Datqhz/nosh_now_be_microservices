using MassTransit;
using Microsoft.Extensions.Logging;
using Shared.MassTransits.Enums;

namespace Shared.MassTransits.Core;

public class SendEndpointCustomProvider : ISendEndpointCustomProvider
{
    private readonly IBusControl _busControl;
    private readonly ILogger<SendEndpointCustomProvider> _logger;

    public SendEndpointCustomProvider(IBusControl busControl, 
        ILogger<SendEndpointCustomProvider> logger)
    {
        _busControl = busControl;
        _logger = logger;
    }
    
    public async Task SendMessage<T>(object message, ExchangeType type, CancellationToken cancellationToken) where T : class
    {
        const string funcName = $"{nameof(SendEndpointCustomProvider)} {nameof(SendMessage)} =>";
        try
        {
            _logger.LogDebug($"{funcName} is called ...");
            var kebabFormatter =  new KebabCaseEndpointNameFormatter(false);
            var target = kebabFormatter.SanitizeName(typeof(T).Name);
        
            if (!string.IsNullOrWhiteSpace(target))
            {
                switch (type)
                {
                    case ExchangeType.Direct:
                    {
                        _logger.LogInformation($"{funcName} queue: {target}");
                        var sendEndpoint = await _busControl.GetSendEndpoint(new Uri($"queue:{target}"));
                        await sendEndpoint.Send<T>(message, cancellationToken);
                        break;
                    }
                    case ExchangeType.Topic:
                    {
                        _logger.LogInformation($"{funcName} Exchange: {target}");
                        await _busControl.Publish<T>(message, cancellationToken);
                        break;
                    }
                    default:
                    {
                        return;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"{nameof(SendEndpointCustomProvider)} {nameof(SendMessage)} => {ex.Message}");
        }
        
    }
    
    public Task<ISendEndpoint> GetSendEndpoint(Uri address)
    {
        return _busControl.GetSendEndpoint(address);
    }

    public ConnectHandle ConnectSendObserver(ISendObserver observer)
    {
        return _busControl.ConnectSendObserver(observer);
    }
}