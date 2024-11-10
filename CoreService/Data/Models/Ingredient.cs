using Shared.Enums;

namespace CoreService.Data.Models;

public class Ingredient
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }
    public double Quantity { get; set; }
    public IngredientUnit Unit { get; set; }
    public Guid RestaurantId { get; set; }
    public virtual Restaurant Restaurant { get; set; }
    public virtual IEnumerable<RequiredIngredient> RequiredIngredients { get; set; }
}