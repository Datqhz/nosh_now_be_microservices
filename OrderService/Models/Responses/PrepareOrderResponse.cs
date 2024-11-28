using Shared.Responses;

namespace OrderService.Models.Responses;

public class PrepareOrderResponse: BaseResponse
{
    public PrepareOrderData Data { get; set; }
}

public class PrepareOrderData
{
    public long OrderId { get; set; }
    public string RestaurantName { get; set; }
    public string RestaurantCoordinate { get; set; }
    public List<PrepareOrderOrderDetailData> OrderDetails { get; set; }
    public decimal Substantial { get; set; }
}

public class PrepareOrderOrderDetailData
{
    public long OrderDetailId { get; set; }
    public string FoodName { get; set; }
    public string FoodImage { get; set; }
    public decimal FoodPrice { get; set; }
    public int Amount { get; set; }
}