using Shared.Enums;

namespace Shared.MassTransits.Contracts;

public class NotifyOrderSchedule
{
    public string OrderId { get; set; }
    public string RestaurantName { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public List<string> Receivers { get; set; } = new();
}