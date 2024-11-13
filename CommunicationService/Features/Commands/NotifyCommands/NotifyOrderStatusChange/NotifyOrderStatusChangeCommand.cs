using CommunicationService.Models.Requests;
using MediatR;

namespace CommunicationService.Features.Commands.NotifyCommands.NotifyOrderStatusChange;

public class NotifyOrderStatusChangeCommand : IRequest
{
    public NotifyOrderStatusChangeRequest Payload { get; set; }
    public NotifyOrderStatusChangeCommand(NotifyOrderStatusChangeRequest payload)
    {
        this.Payload = payload;
    }
}