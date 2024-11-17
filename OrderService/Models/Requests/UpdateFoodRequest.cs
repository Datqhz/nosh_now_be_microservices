using OrderService.Enums;

namespace OrderService.Models.Requests;

public class UpdateFoodRequest
{
    public int FoodId { get; set; }
    public string FoodName { get; set; }
    public string FoodDescription { get; set; }
    public decimal FoodPrice { get; set; }
    public string CategoryId { get; set; }
    public string FoodImage { get; set; }
    public List<UpdateRequiredIngredientInput> Ingredients { get; set; }
}

public class UpdateRequiredIngredientInput
{
    public int RequiredIngredientId { get; set; }
    public double Quantity { get; set; }
    public ModifyOption ModifyOption { get; set; }
}