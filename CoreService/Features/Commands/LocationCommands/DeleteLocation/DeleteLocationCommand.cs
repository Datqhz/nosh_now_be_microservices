using CoreService.Models.Responses;
using MediatR;

namespace CoreService.Features.Commands.LocationCommands.DeleteLocation;

public class DeleteLocationCommand : IRequest<DeleteLocationResponse>
{
    public int LocationId { get; set; }
    public DeleteLocationCommand(int locationId)
    {
        LocationId = locationId;
    }
}