using AuthServer.Models.Requests;
using AuthServer.Models.Responses;
using MediatR;

namespace AuthServer.Features.Commands.AccountCommands.ChangePassword;

public class ChangePasswordCommand : IRequest<ChangePasswordResponse>
{
    public ChangePasswordRequest Payload { get; set; }
    public ChangePasswordCommand(ChangePasswordRequest payload)
    {
        Payload = payload;
    }
}
