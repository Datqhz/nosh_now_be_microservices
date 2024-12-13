namespace OrderService.Models.Requests;

public class AddOrderDetailRequest
{
    public long OrderId { get; set; }
    public int FoodId { get; set; }
    public int Amount { get; set; }
}