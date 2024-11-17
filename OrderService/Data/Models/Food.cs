namespace OrderService.Data.Models;

public class Food
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public bool IsDeleted { get; set; }
    public string Image { get; set; }
    public string RestaurantId { get; set; }
    public Guid CategoryId { get; set; }
    public virtual Category Category { get; set; }
    public virtual Restaurant Restaurant { get; set; }
    public virtual IEnumerable<RequiredIngredient> RequiredIngredients { get; set; }
    public virtual IEnumerable<OrderDetail> OrderDetails { get; set; }
}