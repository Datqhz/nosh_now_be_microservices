using MediatR;
using Shared.MassTransits.Contracts;

namespace CommunicationService.Features.Commands.HubCommands;

public class SendScheduleNotificationCommand : IRequest
{
    public NotifyOrderSchedule Event  { get; set; }

    public SendScheduleNotificationCommand(NotifyOrderSchedule eventToSend)
    {
        Event = eventToSend;
    }
}