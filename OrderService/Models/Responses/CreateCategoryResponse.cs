using Shared.Responses;

namespace OrderService.Models.Responses;

public class CreateCategoryResponse : BaseResponse
{
    public string Data { get; set; }
}