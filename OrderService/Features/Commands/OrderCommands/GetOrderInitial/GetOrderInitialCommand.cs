using MediatR;
using OrderService.Models.Responses;

namespace OrderService.Features.Commands.OrderCommands.GetOrderInitial;

public class GetOrderInitialCommand : IRequest<GetOrderInitialResponse>
{
    public string RestaurantId { get; set; }
    public GetOrderInitialCommand(string restaurantId)
    {
        RestaurantId = restaurantId;
    }
}
