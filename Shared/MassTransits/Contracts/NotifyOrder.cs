using Shared.Enums;

namespace Shared.MassTransits.Contracts;

public class NotifyOrder
{
    public string OrderId { get; set; }
    public string RestaurantName { get; set; }
    public decimal Total { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public List<string> Receivers { get; set; } = new();
    public ReceiverType ReceiverType { get; set; }
}