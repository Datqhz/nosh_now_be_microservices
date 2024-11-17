namespace OrderService.Data.Models;

public class RequiredIngredient
{
    public int Id { get; set; }
    public int FoodId { get; set; }
    public int IngredientId { get; set; }
    public double Quantity { get; set; }
    public virtual Food Food { get; set; }
    public virtual Ingredient Ingredient { get; set; }
}