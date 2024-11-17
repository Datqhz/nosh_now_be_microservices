using MediatR;
using OrderService.Models.Responses;

namespace OrderService.Features.Commands.FoodCommands.DeleteFood;

public class DeleteFoodCommand : IRequest<DeleteFoodResponse>
{
    public int  FoodId { get; set; }
    public DeleteFoodCommand(int foodId)
    {
        FoodId = foodId;
    }
}