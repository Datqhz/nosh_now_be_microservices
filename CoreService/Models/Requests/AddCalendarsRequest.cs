namespace CoreService.Models.Requests;

public class AddCalendarsRequest
{
    public List<CalendarInput> Inputs { get; set; }
}

public class CalendarInput
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
