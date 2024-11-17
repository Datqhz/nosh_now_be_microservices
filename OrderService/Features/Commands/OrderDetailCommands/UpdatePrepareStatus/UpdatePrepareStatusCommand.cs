using MediatR;
using OrderService.Models.Requests;
using OrderService.Models.Responses;

namespace OrderService.Features.Commands.OrderDetailCommands.UpdatePrepareStatus;

public class UpdatePrepareStatusCommand : IRequest<UpdatePrepareStatusResponse>
{
    public UpdatePrepareStatusRequest Payload { get; set; }
    public UpdatePrepareStatusCommand(UpdatePrepareStatusRequest payload)
    {
        Payload = payload;
    }
}