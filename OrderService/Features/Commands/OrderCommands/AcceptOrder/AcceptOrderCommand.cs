using MediatR;
using OrderService.Models.Responses;

namespace OrderService.Features.Commands.OrderCommands.AcceptOrder;

public class AcceptOrderCommand : IRequest<AcceptOrderResponse>
{
    public long OrderId { get; set; }
    public AcceptOrderCommand(long orderId)
    {
        OrderId = orderId;
    }
}