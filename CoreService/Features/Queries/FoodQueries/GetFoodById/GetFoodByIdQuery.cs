using CoreService.Models.Responses;
using MediatR;

namespace CoreService.Features.Queries.FoodQueries.GetFoodById;

public class GetFoodByIdQuery : IRequest<GetFoodByIdResponse>
{
    public int FoodId { get; set; }
    public GetFoodByIdQuery(int foodId)
    {
        FoodId = foodId;
    }
}