using MediatR;
using OrderService.Models.Responses;

namespace OrderService.Features.Commands.IngredientCommands.DeleteIngredient;

public class DeleteIngredientCommand : IRequest<DeleteIngredientResponse>
{
    public int IngredientId { get; set; }
    public DeleteIngredientCommand(int ingredientId)
    {
        IngredientId = ingredientId;
    }
}