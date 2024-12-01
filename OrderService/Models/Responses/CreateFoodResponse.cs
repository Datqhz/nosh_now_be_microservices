using Shared.Responses;

namespace OrderService.Models.Responses;

public class CreateFoodResponse : BaseResponse
{
    public int Data { get; set; }
}