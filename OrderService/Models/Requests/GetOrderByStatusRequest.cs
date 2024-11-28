using Shared.Enums;

namespace OrderService.Models.Requests;

public class GetOrderByStatusRequest
{
    public OrderStatus OrderStatus { get; set; }
    public SortDirection SortDirection { get; set; }
}

public enum SortDirection
{
    Ascending,
    Descending
}