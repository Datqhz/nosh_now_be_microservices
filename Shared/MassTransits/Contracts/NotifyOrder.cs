namespace Shared.MassTransits.Contracts;

public class NotifyOrder
{
    public string Title { get; set; }
    public string Content { get; set; }
    public List<string> Receivers { get; set; }
}