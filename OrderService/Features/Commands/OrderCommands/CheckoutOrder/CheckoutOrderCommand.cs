using MediatR;
using OrderService.Models.Requests;
using OrderService.Models.Responses;

namespace OrderService.Features.Commands.OrderCommands.CheckoutOrder;

public class CheckoutOrderCommand : IRequest<CheckoutOrderResponse>
{
    public CheckoutOrderRequest Payload { get; set; }
    public CheckoutOrderCommand(CheckoutOrderRequest payload)
    {
        this.Payload = payload;
    }
}