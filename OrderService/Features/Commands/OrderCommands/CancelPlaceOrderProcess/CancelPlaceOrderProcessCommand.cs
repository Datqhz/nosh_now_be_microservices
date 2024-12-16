using MediatR;

namespace OrderService.Features.Commands.OrderCommands.CancelPlaceOrderProcess;

public class CancelPlaceOrderProcessCommand : IRequest
{
    public long OrderId { get; set; }
    public CancelPlaceOrderProcessCommand(long orderId)
    {
        OrderId = orderId;
    }
}