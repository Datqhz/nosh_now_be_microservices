using OrderService.Enums;

namespace OrderService.Data.Models;

public class OrderDetail
{
    public long Id { get; set; }
    public long OrderId { get; set; }
    public int FoodId { get; set; }
    public int Amount { get; set; }
    public decimal Price { get; set; }
    public PrepareStatus Status { get; set; }
    public virtual Food Food { get; set; }
    public virtual Order Order { get; set; }
}