using Shared.Responses;

namespace OrderService.Models.Responses;

public class GetFoodByIdResponse : BaseResponse
{
    public GetFoodByIdData Data { get; set; }
}

public class GetFoodByIdData
{
    public int FoodId { get; set; }
    public string FoodName { get; set; }
    public string FoodDescription { get; set; }
    public string FoodImage { get; set; }
    public decimal FoodPrice { get; set; }
    public FoodCategoryData Category { get; set; }
    public List<FoodIngredientData> FoodIngredients { get; set; }
}

public class FoodIngredientData
{
    public long RequiredIngredientId { get; set; }
    public string IngredientName { get; set; }
    public double RequiredAmount { get; set; }
    public string IngredientImage { get; set; }
    public string Unit { get; set; }
}

public class FoodCategoryData
{
    public string CategoryName { get; set; }
    public string CategoryId { get; set; }
}