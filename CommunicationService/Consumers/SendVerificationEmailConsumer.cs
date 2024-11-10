using System.Text.Json;
using CommunicationService.Services;
using MassTransit;
using Newtonsoft.Json;
using Shared.MassTransits.Contracts;

namespace CommunicationService.Consumers;

public class SendVerificationEmailConsumer : IConsumer<SendVerificationEmail>
{
    private readonly IEmailService _emailService;
    private readonly ILogger<SendVerificationEmailConsumer> _logger;

    public SendVerificationEmailConsumer
    (
        IEmailService emailService,
        ILogger<SendVerificationEmailConsumer> logger
    )
    {
        _emailService = emailService;
        _logger = logger;
    }
    public async Task Consume(ConsumeContext<SendVerificationEmail> context)
    {
        var message = context.Message;
        var functionName = $"{nameof(SendVerificationEmailConsumer)} Message = {JsonConvert.SerializeObject(message)}";
        _logger.LogInformation(functionName);
        try
        {
            Console.WriteLine(functionName);
            await _emailService.SendVerifyEmail(message.Email, message.VerificationToken, message.DisplayName);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"{functionName} Has error: {ex.Message}");
        }
    }
}