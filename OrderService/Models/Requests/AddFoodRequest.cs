namespace OrderService.Models.Requests;

public class AddFoodRequest
{
    public string FoodName { get; set; }
    public string FoodDescription { get; set; }
    public decimal FoodPrice { get; set; }
    public string CategoryId { get; set; }
    public string FoodImage { get; set; }
    public List<IngredientInput> Ingredients { get; set; }
    
}

public class IngredientInput
{
    public int IngredientId { get; set; }
    public double Quantity { get; set; }
}