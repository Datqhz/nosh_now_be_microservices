using CoreService.Models.Requests;
using CoreService.Models.Responses;
using MediatR;

namespace CoreService.Features.Queries.RestaurantQueries.GetRestaurantByCategory;

public class GetRestaurantByCategoryQuery : IRequest<GetRestaurantByCategoryResponse>
{
    public GetRestaurantByCategoryRequest Payload { get; set; }
    public GetRestaurantByCategoryQuery(GetRestaurantByCategoryRequest payload)
    {
        Payload = payload;
    }
}