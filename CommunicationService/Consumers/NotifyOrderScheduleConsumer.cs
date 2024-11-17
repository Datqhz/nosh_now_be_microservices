using MassTransit;
using Shared.MassTransits.Contracts;

namespace CommunicationService.Consumers;

public class NotifyOrderScheduleConsumer : IConsumer<NotifyOrderSchedule>
{
    public async Task Consume(ConsumeContext<NotifyOrderSchedule> context)
    {
        await Task.CompletedTask;
    }
}