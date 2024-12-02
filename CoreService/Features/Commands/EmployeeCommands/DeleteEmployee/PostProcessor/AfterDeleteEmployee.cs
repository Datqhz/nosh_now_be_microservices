using CoreService.Models.Responses;
using MediatR.Pipeline;
using Shared.Extensions;
using Shared.MassTransits.Core;
using Shared.MassTransits.Enums;
using Shared.Responses;

namespace CoreService.Features.Commands.EmployeeCommands.DeleteEmployee.PostProcessor;

public class AfterDeleteEmployee : IRequestPostProcessor<DeleteEmployeeCommand, DeleteEmployeeResponse>
{
    private readonly ISendEndpointCustomProvider _sendEndpointCustomProvider;
    private readonly ILogger<AfterDeleteEmployee> _logger;

    public AfterDeleteEmployee
    (
        ISendEndpointCustomProvider sendEndpointCustomProvider,
        ILogger<AfterDeleteEmployee> logger
    )
    {
        _sendEndpointCustomProvider = sendEndpointCustomProvider;
        _logger = logger;
    }
    public async Task Process(DeleteEmployeeCommand request, DeleteEmployeeResponse response, CancellationToken cancellationToken)
    {
        var functionName = $"{nameof(AfterDeleteEmployee)} EmployeeId = {request.EmployeeId} =>";

        try
        {
            if (response.StatusCode == (int)ResponseStatusCode.Ok)
            {
                var message = new Shared.MassTransits.Contracts.DeleteAccount()
                {
                    AccountId = request.EmployeeId,
                };
            
                await _sendEndpointCustomProvider.SendMessage<Shared.MassTransits.Contracts.DeleteAccount>(message, ExchangeType.Direct, cancellationToken);
            }
        }
        catch (Exception ex)
        {
            ex.LogError($"{functionName} Has error: {ex.Message}", _logger);
        }
    }
}
