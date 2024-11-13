using Shared.Enums;
using Shared.Responses;

namespace OrderService.Models.Responses;

public class GetOrderByStatusResponse : BaseResponse
{
    public List<GetOrderByStatusData> Data { get; set; }
}

public class GetOrderByStatusData
{
    public long OrderId { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalPay { get; set; }
    public string RestaurantName { get; set; }
}