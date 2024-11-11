using CoreService.Models.Responses;
using MediatR;

namespace CoreService.Features.Queries.FoodQueries.GetFoodsByRestaurant;

public class GetFoodsByRestaurantQuery : IRequest<GetFoodsByRestaurantResponse>
{
    public string RestaurantId { get; set; }
    public GetFoodsByRestaurantQuery(string restaurantId)
    {
        RestaurantId = restaurantId;
    }
}