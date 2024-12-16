using System.Text.Json;
using MassTransit;
using MediatR;
using OrderService.Features.Commands.OrderCommands.CancelPlaceOrderProcess;
using Shared.Extensions;
using Shared.MassTransits.Contracts;

namespace OrderService.Consumers;

public class ReCalculateIngredientConsumer : IConsumer<ReCalculateIngredient>
{
    private readonly IMediator _mediator;
    private readonly ILogger<ReCalculateIngredientConsumer> _logger;
    public ReCalculateIngredientConsumer(IMediator mediator, ILogger<ReCalculateIngredientConsumer> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }
    public async Task Consume(ConsumeContext<ReCalculateIngredient> context)
    {
        var message = context.Message;
        var functionName = $"{nameof(ReCalculateIngredientConsumer)} Message = ${JsonSerializer.Serialize(message)}";

        try
        {
            _logger.LogInformation(functionName);

            await _mediator.Send(new CancelPlaceOrderProcessCommand(message.OrderId));

        }
        catch (Exception ex)
        {
            ex.LogError($"{functionName} Has error: {ex.Message}", _logger);
        }
    }
}