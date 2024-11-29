using OrderService.Models.Requests;
using OrderService.Models.Responses;
using MediatR;

namespace OrderService.Features.Queries.RestaurantQueries.GetRestaurants;

public class GetRestaurantsByCategoryQuery : IRequest<GetRestaurantsByCategoryResponse>
{
    public GetRestaurantsByCategoryRequest Payload { get; set; }
    public GetRestaurantsByCategoryQuery(GetRestaurantsByCategoryRequest payload)
    {
        Payload = payload;
    }
}