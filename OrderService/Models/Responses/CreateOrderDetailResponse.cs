using Shared.Responses;

namespace OrderService.Models.Responses;

public class CreateOrderDetailResponse : BaseResponse
{
    public int Data { get; set; }
}