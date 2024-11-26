using Shared.Enums;

namespace CommunicationService.Models.Requests;

public class NotifyOrderStatusChangeRequest
{
    public string OrderId { get; set; }
    public string RestaurantName { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public List<string> Receivers { get; set; } = new();
}