using MediatR;
using OrderService.Models.Requests;
using OrderService.Models.Responses;

namespace OrderService.Features.Commands.FoodCommands.CreateFood;

public class CreateFoodCommand : IRequest<CreateFoodResponse>
{
    public CreateFoodRequest Payload { get; set; }
    public CreateFoodCommand(CreateFoodRequest payload)
    {
        Payload = payload;
    }
}