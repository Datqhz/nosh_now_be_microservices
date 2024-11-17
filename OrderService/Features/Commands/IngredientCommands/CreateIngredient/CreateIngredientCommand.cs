using MediatR;
using OrderService.Models.Requests;
using OrderService.Models.Responses;

namespace OrderService.Features.Commands.IngredientCommands.CreateIngredient;

public class CreateIngredientCommand : IRequest<CreateIngredientResponse>
{
    public CreateIngredientRequest Payload { get; set; }
    public CreateIngredientCommand(CreateIngredientRequest payload)
    {
        Payload = payload;
    }
}