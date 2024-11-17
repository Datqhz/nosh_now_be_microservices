using Shared.Models.Dtos;
using Shared.Responses;

namespace OrderService.Models.Responses;

public class GetFoodsResponse : BaseResponse
{
    public List<GetRandomFoodData> Data { get; set; }
    public PagingDto Paging { get; set; }
}

public class GetRandomFoodData
{
    public int FoodId { get; set; }
    public string FoodName { get; set; }
    public decimal Price { get; set; }
    public string FoodImage { get; set; }
    public string RestaurantId { get; set; }
}