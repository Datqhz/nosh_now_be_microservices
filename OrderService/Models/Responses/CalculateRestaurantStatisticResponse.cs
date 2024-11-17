using Shared.Responses;

namespace OrderService.Models.Responses;

public class CalculateRestaurantStatisticResponse : BaseResponse
{
    public CalculateRestaurantStatisticData Data { get; set; }
}
public class CalculateRestaurantStatisticData
{
    public int TotalOrder { get; set; } = 0;
    public decimal TotalRevenue { get; set; } = 0;
    public List<TopFoodData> TopFoods { get; set; } = new();
}

public class TopFoodData
{
    public string FoodName { get; set; }
    public decimal TotalRevenue { get; set; }
}