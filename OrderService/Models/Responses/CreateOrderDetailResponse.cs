using Shared.Responses;

namespace OrderService.Models.Responses;

public class CreateOrderDetailResponse : BaseResponse
{
    public long Data { get; set; }
}