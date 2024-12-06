using CoreService.Models.Requests;
using CoreService.Models.Responses;
using MediatR;

namespace CoreService.Features.Commands.CalendarCommands.DeleteCalendars;

public class DeleteCalendarsCommand : IRequest<DeleteCalendarsResponse>
{
   public DeleteCalendarsRequest Payload { get; set; }
   public DeleteCalendarsCommand(DeleteCalendarsRequest payload)
   {
      Payload = payload;
   }
}
