using MediatR;
using OrderService.Models.Requests;
using OrderService.Models.Responses;

namespace OrderService.Features.Commands.FoodCommands.AddFood;

public class AddFoodCommand : IRequest<CreateFoodResponse>
{
    public AddFoodRequest Payload { get; set; }
    public AddFoodCommand(AddFoodRequest payload)
    {
        Payload = payload;
    }
}