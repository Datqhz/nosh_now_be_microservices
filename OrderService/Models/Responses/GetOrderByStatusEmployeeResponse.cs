using Shared.Enums;
using Shared.Responses;

namespace OrderService.Models.Responses;

public class GetOrderByStatusEmployeeResponse : BaseResponse
{
    public List<GetOrderByStatusEmployeeData> Data { get; set; }
}

public class GetOrderByStatusEmployeeData
{
    public long OrderId { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalPay { get; set; }
    public string CustomerName { get; set; }
}