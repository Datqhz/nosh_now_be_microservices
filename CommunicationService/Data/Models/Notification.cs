using Shared.Enums;

namespace CommunicationService.Data.Models;

public class Notification
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string UserId { get; set; }
    public ReceiverType ReceiverType { get; set; }
}