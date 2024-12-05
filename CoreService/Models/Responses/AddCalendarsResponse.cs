using Shared.Responses;

namespace CoreService.Models.Responses;

public class AddCalendarsResponse : BaseResponse
{
    public List<AddCalendarsData> Data { get; set; }
}

public class AddCalendarsData
{
    public long CalendarId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
