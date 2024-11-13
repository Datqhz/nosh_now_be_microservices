using Shared.Responses;

namespace OrderService.Models.Responses;

public class GetOrderInitialResponse : BaseResponse
{
    public GetOrderInitialData Data { get; set; }
}

public class GetOrderInitialData
{
    public long OrderId { get; set; }
    public List<GetOrderInitialOrderDetailData> OrderDetails { get; set; } = new();
}

public class GetOrderInitialOrderDetailData
{
    public long Id { get; set; }
    public int FoodId { get; set; }
    public int Amount { get; set; }
}