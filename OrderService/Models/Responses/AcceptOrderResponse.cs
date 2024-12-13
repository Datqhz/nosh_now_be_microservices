using OrderService.Data.Models;
using Shared.Responses;

namespace OrderService.Models.Responses;

public class AcceptOrderResponse : BaseResponse
{
    internal AcceptOrderPostProcessorData PostProcessorData { get; set; }
}

public class AcceptOrderPostProcessorData
{
    public Order Order { get; set; }
    public string RestaurantId { get; set; }
    public string RestaurantName { get; set; }
}