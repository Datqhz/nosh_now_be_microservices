using MediatR;
using OrderService.Models.Responses;

namespace OrderService.Features.Queries.IngredientQueries.GetIngredients;

public class GetIngredientsQuery : IRequest<GetIngredientsResponse>
{
}