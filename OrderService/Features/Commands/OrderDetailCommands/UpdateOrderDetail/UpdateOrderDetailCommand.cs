using MediatR;
using OrderService.Models.Requests;
using OrderService.Models.Responses;

namespace OrderService.Features.Commands.OrderDetailCommands.UpdateOrderDetail;

public class UpdateOrderDetailCommand : IRequest<UpdateOrderDetailResponse>
{
    public UpdateOrderDetailRequest Payload { get; set; }
    public UpdateOrderDetailCommand(UpdateOrderDetailRequest payload)
    {
        this.Payload = payload;
    }
}