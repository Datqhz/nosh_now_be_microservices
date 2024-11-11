using CoreService.Models.Requests;
using CoreService.Models.Responses;
using MediatR;

namespace CoreService.Features.Queries.FoodQueries.GetRandomFood;

public class GetFoodsQuery : IRequest<GetFoodsResponse>
{
    public GetFoodsRequest Payload { get; set; }
    public GetFoodsQuery(GetFoodsRequest payload)
    {
        Payload = payload;
    }
}