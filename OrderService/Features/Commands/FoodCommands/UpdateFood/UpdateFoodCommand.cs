using MediatR;
using OrderService.Models.Requests;
using OrderService.Models.Responses;

namespace OrderService.Features.Commands.FoodCommands.UpdateFood;

public class UpdateFoodCommand : IRequest<UpdateFoodResponse>
{
    public UpdateFoodRequest Payload { get; set; }
    public UpdateFoodCommand(UpdateFoodRequest payload)
    {
        Payload = payload;
    }
}