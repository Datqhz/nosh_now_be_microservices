namespace Shared.MassTransits.Contracts;

public class NotifyOrderSchedule
{
    public TimeSpan DeliveryTime { get; set; }
    public NotifyOrder Notification { get; set; }
}