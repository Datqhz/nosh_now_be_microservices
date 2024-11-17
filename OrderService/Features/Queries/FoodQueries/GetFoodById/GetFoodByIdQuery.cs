using OrderService.Models.Responses;
using MediatR;

namespace OrderService.Features.Queries.FoodQueries.GetFoodById;

public class GetFoodByIdQuery : IRequest<GetFoodByIdResponse>
{
    public int FoodId { get; set; }
    public GetFoodByIdQuery(int foodId)
    {
        FoodId = foodId;
    }
}