using MediatR;
using OrderService.Models.Requests;
using OrderService.Models.Responses;

namespace OrderService.Features.Commands.OrderDetailCommands.AddOrderDetail;

public class AddOrderDetailCommand : IRequest<CreateOrderDetailResponse>
{
    public AddOrderDetailRequest Payload { get; set; }
    public AddOrderDetailCommand(AddOrderDetailRequest payload)
    {
        this.Payload = payload;
    }
}