using CoreService.Models.Requests;
using CoreService.Models.Responses;
using MediatR;

namespace CoreService.Features.Commands.EmployeeCommands.UpdateEmployeeProfile;

public class UpdateEmployeeProfileCommand : IRequest<UpdateEmployeeProfileResponse>
{
    public UpdateEmployeeProfileRequest Payload { get; set; }
    public UpdateEmployeeProfileCommand(UpdateEmployeeProfileRequest payload)
    {
        Payload = payload;
    }
}