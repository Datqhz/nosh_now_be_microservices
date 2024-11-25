using System.Text.Json;
using MassTransit;
using Shared.MassTransits.Contracts;

namespace CommunicationService.Consumers;

public class NotifyOrderConsumer : IConsumer<NotifyOrder>
{
    public async Task Consume(ConsumeContext<NotifyOrder> context)
    {
        Console.WriteLine(JsonSerializer.Serialize(context.Message));
        await Task.CompletedTask;
    }
}