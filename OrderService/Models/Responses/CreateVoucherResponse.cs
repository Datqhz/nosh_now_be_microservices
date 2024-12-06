using Shared.Responses;

namespace OrderService.Models.Responses;

public class CreateVoucherResponse : BaseResponse
{
    public long VoucherId { get; set; }
}
