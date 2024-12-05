using MediatR;
using OrderService.Models.Responses;

namespace OrderService.Features.Commands.OrderCommands.RejectOrder;

public class RejectOrderCommand : IRequest<RejectOrderResponse>
{
    public long OrderId { get; set; }
    public RejectOrderCommand(int orderId)
    {
        OrderId = orderId;
    }
}