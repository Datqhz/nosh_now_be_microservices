using OrderService.Models.Responses;
using MediatR;

namespace OrderService.Features.Queries.FoodQueries.GetFoodsByRestaurant;

public class GetFoodsByRestaurantQuery : IRequest<GetFoodsByRestaurantResponse>
{
    public string RestaurantId { get; set; }
    public GetFoodsByRestaurantQuery(string restaurantId)
    {
        RestaurantId = restaurantId;
    }
}