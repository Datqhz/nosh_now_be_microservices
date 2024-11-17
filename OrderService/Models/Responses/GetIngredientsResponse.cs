using OrderService.Data.Models;
using Shared.Enums;
using Shared.Responses;

namespace OrderService.Models.Responses;

public class GetIngredientsResponse : BaseResponse
{
    public List<GetIngredientsData> Data { get; set; }
}

public class GetIngredientsData
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }
    public double Quantity { get; set; }
    public IngredientUnit Unit { get; set; }
}