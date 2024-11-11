using Shared.Responses;

namespace CoreService.Models.Responses;

public class GetFoodsByRestaurantResponse : BaseResponse
{
    public List<GetFoodsByRestaurantData> Data { get; set; }
}

public class GetFoodsByRestaurantData
{
    public int FoodId { get; set; }
    public string FoodName { get; set; }
    public string FoodImage { get; set; }
    public decimal Price { get; set; }
}