namespace OrderService.Data.Models;

public class Food
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }
    public virtual IEnumerable<OrderDetail> OrderDetails { get; set; }
}