namespace OrderService.Data.Models;

public class Restaurant
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Coordinate { get; set; }
    public string Phone { get; set; }
    public string Avatar { get; set; }
    public virtual IEnumerable<Order> Orders { get; set; }
    public virtual IEnumerable<Food> Foods { get; set; }
    public virtual IEnumerable<Ingredient> Ingredients { get; set; }
}