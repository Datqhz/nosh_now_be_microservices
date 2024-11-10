using AuthServer.Models.Responses;
using AuthServer.Repositories;
using MassTransit;
using MediatR.Pipeline;
using Shared.MassTransits.Contracts;
using Shared.MassTransits.Core;
using Shared.MassTransits.Enums;
using Shared.Responses;

namespace AuthServer.Features.Commands.AccountCommands.ConfirmVerificationEmail.ConfirmEmailPostProcessor;

public class AfterConfirmEmailPostProcessor : IRequestPostProcessor<ConfirmVerificationEmailCommand, ConfirmVerificationEmailResponse>
{
    private readonly ILogger<AfterConfirmEmailPostProcessor> _logger;
    private readonly ISendEndpointCustomProvider _sendEndpoint;

    public AfterConfirmEmailPostProcessor
    (
        ILogger<AfterConfirmEmailPostProcessor> logger,
        ISendEndpointCustomProvider sendEndpoint
    )
    {
        _logger = logger;
        _sendEndpoint = sendEndpoint;
    }
    public async Task Process(ConfirmVerificationEmailCommand request, ConfirmVerificationEmailResponse response,
        CancellationToken cancellationToken)
    {
        var functionName = $"{nameof(AfterConfirmEmailPostProcessor)} AccountId = {response.AccountId} => ";
        _logger.LogInformation(functionName);
        try
        {
            if (response.StatusCode == (int)ResponseStatusCode.Ok)
            {
                var message = new UpdateUser
                {
                    AccountId = response.AccountId,
                    IsActive = true
                };
                //await _sendEndpoint.SendMessage<UpdateUser>(message, ExchangeType.Topic, cancellationToken);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"{functionName} Has error: {ex.Message}");
        }
    }
}