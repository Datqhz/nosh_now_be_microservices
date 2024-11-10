namespace OrderService.Data.Models;

public class Customer
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Avatar { get; set; }
    public virtual IEnumerable<Order> Orders { get; set; }
}