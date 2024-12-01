using System.Text.Json;
using MassTransit;
using Shared.Extensions;
using Shared.MassTransits.Contracts;

namespace CommunicationService.Consumers;


public class ScheduleNotificationConsumer :
    IConsumer<NotifyOrderSchedule>
{
    private readonly ILogger<ScheduleNotificationConsumer> _logger;
    public ScheduleNotificationConsumer(ILogger<ScheduleNotificationConsumer> logger)
    {
        _logger = logger;
    }
    public async Task Consume(ConsumeContext<NotifyOrderSchedule> context)
    {
        var message = context.Message;
        var functionName = $"{nameof(ScheduleNotificationConsumer)} Message = {JsonSerializer.Serialize(message)} =>";
        
        try
        {
            _logger.LogInformation(functionName);
            var queueName = new KebabCaseEndpointNameFormatter(false).SanitizeName(nameof(NotifyOrder));
            Uri notificationService = new Uri($"queue:{queueName}");
   
            await context.ScheduleSend<NotifyOrder>(notificationService,
                context.Message.DeliveryTime, message.Notification);
        }
        catch (Exception ex)
        {
            ex.LogError($"{functionName} Has error: {ex.Message}", _logger);
        }
    }
}