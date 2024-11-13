using CoreService.Models.Requests;
using CoreService.Models.Responses;
using MediatR;

namespace CoreService.Features.Commands.LocationCommands.UpdateLocation;

public class UpdateLocationCommand : IRequest<UpdateLocationResponse>
{
    public UpdateLocationRequest Payload { get; set; }
    public UpdateLocationCommand(UpdateLocationRequest payload)
    {
        Payload = payload;
    }
}