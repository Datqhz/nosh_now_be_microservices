using OrderService.Enums;

namespace OrderService.Models.Requests;

public class UpdateOrderDetailRequest
{
    public long OrderDetailId { get; set; }
    public int Amount { get; set; }
}