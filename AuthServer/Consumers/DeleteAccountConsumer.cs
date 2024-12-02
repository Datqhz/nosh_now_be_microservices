using AuthServer.Features.Commands.AccountCommands.DeleteAccount;
using AuthServer.Repositories;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Extensions;
using Shared.MassTransits.Contracts;

namespace AuthServer.Consumers;

public class DeleteAccountConsumer : IConsumer<DeleteAccount>
{
    private readonly IMediator _mediator;
    private readonly ILogger<DeleteAccountConsumer> _logger;
    public DeleteAccountConsumer
        (
            IMediator mediator,
            ILogger<DeleteAccountConsumer> logger
        )
    {
        _mediator = mediator;
        _logger = logger;
    }
    public async Task Consume(ConsumeContext<DeleteAccount> context)
    {
        var message = context.Message;
        var functionName = $"{nameof(DeleteAccountConsumer)} AccountId = {message.AccountId} =>";
        _logger.LogInformation(functionName);

        try
        {
            await _mediator.Send(new DeleteAccountCommand(message.AccountId), context.CancellationToken);
        }
        catch (Exception ex)
        {
            ex.LogError($"{functionName} Has error: {ex.Message}", _logger);
        }
    }
}