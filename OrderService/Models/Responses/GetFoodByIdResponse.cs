using Shared.Responses;

namespace OrderService.Models.Responses;

public class GetFoodByIdResponse : BaseResponse
{
    public GetFoodByIdData Data { get; set; }
}

public class GetFoodByIdData
{
    public int FoodId { get; set; }
    public string FoodName { get; set; }
    public string FoodDescription { get; set; }
    public string FoodImage { get; set; }
    public decimal FoodPrice { get; set; }
}