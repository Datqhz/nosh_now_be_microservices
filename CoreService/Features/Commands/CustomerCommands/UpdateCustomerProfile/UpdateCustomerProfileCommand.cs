using CoreService.Models.Requests;
using CoreService.Models.Responses;
using MediatR;

namespace CoreService.Features.Commands.CustomerCommands.UpdateCustomerProfile;

public class UpdateCustomerProfileCommand : IRequest<UpdateCustomerProfileResponse>
{
    public UpdateCustomerProfileRequest Payload { get; set; }
    public UpdateCustomerProfileCommand(UpdateCustomerProfileRequest payload)
    {
        Payload = payload;
    }
}