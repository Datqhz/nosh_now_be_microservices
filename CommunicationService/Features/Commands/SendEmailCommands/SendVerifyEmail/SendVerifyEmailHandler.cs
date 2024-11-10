using CommunicationService.Services;
using MediatR;

namespace CommunicationService.Features.Commands.SendEmailCommands.SendVerifyEmail;

public class SendVerifyEmailHandler : IRequestHandler<SendVerifyEmailCommand>
{
    private readonly IEmailService _emailService;

    public SendVerifyEmailHandler
    (
        IEmailService emailService
    )
    {
        _emailService = emailService;
    }
    public async Task Handle(SendVerifyEmailCommand request, CancellationToken cancellationToken)
    {
        
    }
}