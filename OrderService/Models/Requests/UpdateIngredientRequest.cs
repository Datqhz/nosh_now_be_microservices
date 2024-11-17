using Shared.Enums;

namespace OrderService.Models.Requests;

public class UpdateIngredientRequest
{
    public int IngredientId { get; set; }
    public string IngredientName { get; set; }
    public double Quantity { get; set; }
    public string Image { get; set; }
    public IngredientUnit Unit { get; set; }
}