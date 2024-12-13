using MediatR;
using OrderService.Models.Requests;
using OrderService.Models.Responses;

namespace OrderService.Features.Commands.IngredientCommands.AddIngredient;

public class AddIngredientCommand : IRequest<CreateIngredientResponse>
{
    public AddIngredientRequest Payload { get; set; }
    public AddIngredientCommand(AddIngredientRequest payload)
    {
        Payload = payload;
    }
}