using CommunicationService.Features.Commands.HubCommands.SendOrderNotification;
using MassTransit;
using MediatR;
using Shared.MassTransits.Contracts;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace CommunicationService.Consumers;

public class NotifyOrderConsumer : IConsumer<NotifyOrder>
{
    private readonly IMediator _mediator;
    private readonly ILogger<NotifyOrderConsumer> _logger;
    public NotifyOrderConsumer(IMediator mediator, ILogger<NotifyOrderConsumer> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }
    public async Task Consume(ConsumeContext<NotifyOrder> context)
    {
        var message = context.Message;
        var functionName = $"{nameof(NotifyOrderConsumer)} Message: {JsonSerializer.Serialize(message)} =>";
        _logger.LogInformation(functionName);
        
        try
        {
            await _mediator.Send(new SendNotificationCommand(message));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"{functionName} Has error: {ex.Message}");
        }
    }
}