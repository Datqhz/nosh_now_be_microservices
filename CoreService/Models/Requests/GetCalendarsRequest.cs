namespace CoreService.Models.Requests;

public class GetCalendarsRequest
{
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
}