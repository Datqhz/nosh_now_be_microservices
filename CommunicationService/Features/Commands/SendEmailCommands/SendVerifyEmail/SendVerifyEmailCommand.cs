using CommunicationService.Models.Requests;
using MediatR;

namespace CommunicationService.Features.Commands.SendEmailCommands.SendVerifyEmail;

public class SendVerifyEmailCommand : IRequest
{
    public SendVerifyEmailRequest Payload { get; set; }

    public SendVerifyEmailCommand(SendVerifyEmailRequest payload)
    {
        Payload = payload;
    }
}