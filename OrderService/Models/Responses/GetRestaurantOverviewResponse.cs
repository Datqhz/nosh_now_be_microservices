using Shared.Responses;

namespace OrderService.Models.Responses;

public class GetRestaurantOverviewResponse : BaseResponse
{
    public GetRestaurantOverviewData Data { get; set; }
}

public class GetRestaurantOverviewData
{
    public decimal TotalRevenueInMonth { get; set; }
    public decimal TotalRevenueInDay { get; set; }
    public int TotalSuccessOrder { get; set; }
}