using MediatR;
using OrderService.Models.Requests;
using OrderService.Models.Responses;

namespace OrderService.Features.Commands.OrderDetailCommands.CreateOrderDetail;

public class CreateOrderDetailCommand : IRequest<CreateOrderDetailResponse>
{
    public CreateOrderDetailRequest Payload { get; set; }
    public CreateOrderDetailCommand(CreateOrderDetailRequest payload)
    {
        this.Payload = payload;
    }
}