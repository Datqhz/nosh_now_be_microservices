using CoreService.Models.Requests;
using CoreService.Models.Responses;
using MediatR;

namespace CoreService.Features.Commands.CalendarCommands.AddCalendars;

public class AddCalendarsCommand : IRequest<AddCalendarsResponse>
{
    public AddCalendarsRequest Payload { get; set; }
    public AddCalendarsCommand(AddCalendarsRequest payload)
    {
        Payload = payload;
    }
}
