using Shared.Responses;

namespace CoreService.Models.Responses;

public class AddCalendarsResponse : BaseResponse
{
    public List<AddCalendarsData> Data { get; set; }
}

public class AddCalendarsData
{
    public long Id { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}
