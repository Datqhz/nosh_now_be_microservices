using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Shared.Enums;

namespace OrderService.Data.Models;

public class Ingredient
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }
    public double Quantity { get; set; }
    public IngredientUnit Unit { get; set; }
    public string RestaurantId { get; set; }
    public virtual Restaurant Restaurant { get; set; }
    public virtual IEnumerable<RequiredIngredient> RequiredIngredients { get; set; }
}