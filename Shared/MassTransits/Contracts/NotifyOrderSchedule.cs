namespace Shared.MassTransits.Contracts;

public class NotifyOrderSchedule
{
    public string Title { get; set; }
    public string Content { get; set; }
    public List<string> Receivers { get; set; }
}