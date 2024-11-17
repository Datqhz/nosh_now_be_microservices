using OrderService.Enums;

namespace OrderService.Models.Requests;

public class UpdateOrderDetailRequest
{
    public int OrderDetailId { get; set; }
    public int Amount { get; set; }
}