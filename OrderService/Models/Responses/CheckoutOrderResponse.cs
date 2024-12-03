using OrderService.Models.Requests;
using Shared.Responses;

namespace OrderService.Models.Responses;

public class CheckoutOrderResponse : BaseResponse
{
    public CheckoutOrderPostProcessorData PostProcessorData { get; set; }
}

public class CheckoutOrderPostProcessorData
{
    public string RestaurantId { get; set; }
    public decimal Total { get; set; }
}