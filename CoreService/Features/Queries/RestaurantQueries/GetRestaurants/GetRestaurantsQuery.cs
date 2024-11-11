using CoreService.Models.Requests;
using CoreService.Models.Responses;
using MediatR;

namespace CoreService.Features.Queries.RestaurantQueries.GetRestaurants;

public class GetRestaurantsQuery : IRequest<GetRestaurantsResponse>
{
    public GetRestaurantsRequest Payload { get; set; }
    public GetRestaurantsQuery(GetRestaurantsRequest payload)
    {
        Payload = payload;
    }
}