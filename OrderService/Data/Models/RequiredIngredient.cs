using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderService.Data.Models;

public class RequiredIngredient
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int FoodId { get; set; }
    public int IngredientId { get; set; }
    public double Quantity { get; set; }
    public virtual Food Food { get; set; }
    public virtual Ingredient Ingredient { get; set; }
}