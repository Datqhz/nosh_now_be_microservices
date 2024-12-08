using CoreService.Models.Requests;
using CoreService.Models.Responses;
using MediatR;

namespace CoreService.Features.Queries.CalendarQueries.GetCalendars;

public class GetCalendarsQuery : IRequest<GetCalendarsResponse>
{
    public GetCalendarsRequest Payload { get; set; }
    public GetCalendarsQuery(GetCalendarsRequest payload)
    {
        Payload = payload;
    }
}