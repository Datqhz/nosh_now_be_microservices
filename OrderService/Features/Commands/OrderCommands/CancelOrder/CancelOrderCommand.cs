using MediatR;
using OrderService.Models.Responses;

namespace OrderService.Features.Commands.OrderCommands.CancelOrder;

public class CancelOrderCommand : IRequest<CancelOrderResponse>
{
    public long OrderId { get; set; }
    public CancelOrderCommand(long orderId)
    {
        OrderId = orderId;
    }
}