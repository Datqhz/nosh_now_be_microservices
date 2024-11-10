namespace CoreService.Data.Models;

public class Calendar
{
    public long Id { get; set; }
    public DateTime WorkDate { get; set; }
    public string OpeningTime { get; set; }
    public string ClosingTime { get; set; }
    public Guid RestaurantId { get; set; }
    public virtual Restaurant Restaurant { get; set; }
}