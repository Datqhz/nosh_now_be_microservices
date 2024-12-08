using Shared.Responses;

namespace CoreService.Models.Responses;

public class GetCalendarsResponse : BaseResponse
{
    public List<GetCalendarsData> Data { get; set; }
}

public class GetCalendarsData
{
    public long Id { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}