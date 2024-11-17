using MediatR;
using OrderService.Models.Requests;
using OrderService.Models.Responses;

namespace OrderService.Features.Commands.IngredientCommands.UpdateIngredient;

public class UpdateIngredientCommand : IRequest<UpdateIngredientResponse>
{
    public UpdateIngredientRequest Payload { get; set; }
    public UpdateIngredientCommand(UpdateIngredientRequest payload)
    {
        Payload = payload;
    }
}