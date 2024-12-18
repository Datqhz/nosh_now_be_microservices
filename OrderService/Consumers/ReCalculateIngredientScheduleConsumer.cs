using System.Text.Json;
using MassTransit;
using Shared.Extensions;
using Shared.MassTransits.Contracts;

namespace OrderService.Consumers;

public class ReCalculateIngredientScheduleConsumer : IConsumer<ReCalculateIngredientSchedule>
{
    private readonly ILogger<ReCalculateIngredientScheduleConsumer> _logger;

    public ReCalculateIngredientScheduleConsumer(ILogger<ReCalculateIngredientScheduleConsumer> logger)
    {
        _logger = logger;
    }
    public async Task Consume(ConsumeContext<ReCalculateIngredientSchedule> context)
    {
        var message = context.Message;
        var functionName = $"{nameof(ReCalculateIngredientScheduleConsumer)} Message = {JsonSerializer.Serialize(message)} =>";
        
        try
        {
            _logger.LogInformation(functionName);
            var queueName = new KebabCaseEndpointNameFormatter(false).SanitizeName(nameof(ReCalculateIngredient));
            Uri notificationService = new Uri($"queue:{queueName}");
   
            await context.ScheduleSend<ReCalculateIngredient>(notificationService,
                context.Message.Duration, new ReCalculateIngredient
                {
                    OrderId = message.OrderId
                });
        }
        catch (Exception ex)
        {
            ex.LogError($"{functionName} Has error: {ex.Message}", _logger);
        }
    }
}