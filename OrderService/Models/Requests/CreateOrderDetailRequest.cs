namespace OrderService.Models.Requests;

public class CreateOrderDetailRequest
{
    public long OrderId { get; set; }
    public int FoodId { get; set; }
    public int Amount { get; set; }
}