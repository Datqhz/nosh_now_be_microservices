using MediatR;
using Shared.MassTransits.Contracts;

namespace CommunicationService.Features.Commands.HubCommands.SendOrderNotification;

public class SendNotificationCommand : IRequest
{
    public NotifyOrder Event  { get; set; }

    public SendNotificationCommand(NotifyOrder eventToSend)
    {
        Event = eventToSend;
    }
    
}