using Shared.Enums;

namespace CommunicationService.Hubs.Models;

public class OrderStatusChangeNotification
{
    public string Title { get; set; }
    public string Content { get; set; }
}