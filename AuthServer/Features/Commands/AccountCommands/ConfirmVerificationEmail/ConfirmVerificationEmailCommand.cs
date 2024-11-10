using AuthServer.Models.Requests;
using AuthServer.Models.Responses;
using MediatR;

namespace AuthServer.Features.Commands.AccountCommands.ConfirmVerificationEmail;

public class ConfirmVerificationEmailCommand : IRequest<ConfirmVerificationEmailResponse>
{
    public ConfirmVerificationEmailRequest Payload { get; set; }

    public ConfirmVerificationEmailCommand(ConfirmVerificationEmailRequest payload)
    {
        Payload = payload;
    }
}