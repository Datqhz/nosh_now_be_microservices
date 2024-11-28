using OrderService.Data.Models;
using Shared.Enums;
using Shared.Responses;

namespace OrderService.Models.Responses;

public class GetOrderByIdResponse : BaseResponse
{
    public GetOrderByIdData Data { get; set; }
}

public class GetOrderByIdData
{
    public long OrderId { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal Substantial { get; set; }
    public decimal Total { get; set; }
    public decimal ShippingFee { get; set; }
    public string RestaurantName { get; set; }
    public DeliveryInfo DeliveryInfo { get; set; }
    public string? ShipperName { get; set; }
    public string? ShipperImage { get; set; }
    public string RestaurantCoordinate { get; set; }
    public string CustomerName { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public List<OrderDetailData> OrderDetails { get; set; }
}

public class OrderDetailData
{
    public long OrderDetailId { get; set; }
    public string FoodName { get; set; }
    public string FoodImage { get; set; }
    public decimal FoodPrice { get; set; }
    public int Amount { get; set; }
}