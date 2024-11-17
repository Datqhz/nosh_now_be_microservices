using OrderService.Models.Requests;
using OrderService.Models.Responses;
using MediatR;

namespace OrderService.Features.Queries.FoodQueries.GetFoods;

public class GetFoodsQuery : IRequest<GetFoodsResponse>
{
    public GetFoodsRequest Payload { get; set; }
    public GetFoodsQuery(GetFoodsRequest payload)
    {
        Payload = payload;
    }
}