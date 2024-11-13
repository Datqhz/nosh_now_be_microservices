using CoreService.Models.Requests;
using CoreService.Models.Responses;
using MediatR;

namespace CoreService.Features.Commands.LocationCommands.CreateLocation;

public class CreateLocationCommand : IRequest<CreateLocationResponse>
{
    public CreateLocationRequest Payload { get; set; }
    public CreateLocationCommand(CreateLocationRequest payload)
    {
        Payload = payload;
    }
}