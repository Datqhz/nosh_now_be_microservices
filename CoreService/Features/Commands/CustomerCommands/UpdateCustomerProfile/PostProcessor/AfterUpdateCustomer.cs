using System.Windows.Input;
using CoreService.Models.Responses;
using MediatR.Pipeline;
using Shared.Enums;
using Shared.MassTransits.Contracts;
using Shared.MassTransits.Core;
using Shared.MassTransits.Enums;
using Shared.Responses;

namespace CoreService.Features.Commands.CustomerCommands.UpdateCustomerProfile.PostProcessor;

public class AfterUpdateCustomerPostProcessor : IRequestPostProcessor<UpdateCustomerProfileCommand, UpdateCustomerProfileResponse>
{
    private readonly ILogger<AfterUpdateCustomerPostProcessor> _logger;
    private readonly ISendEndpointCustomProvider _sendEndpoint;
    public AfterUpdateCustomerPostProcessor
    (
        ILogger<AfterUpdateCustomerPostProcessor> logger,
        ISendEndpointCustomProvider sendEndpoint
    )
    {
        _logger = logger;
        _sendEndpoint = sendEndpoint;
    }
    public async Task Process(UpdateCustomerProfileCommand request, UpdateCustomerProfileResponse response,
        CancellationToken cancellationToken)
    {
        const string functionName = $"{nameof(AfterUpdateCustomerPostProcessor)} => ";
        try
        {
            if (response.StatusCode == (int)ResponseStatusCode.Ok)
            {
                var updateSnapshotCustomerEvent = new UpdateSnapshotUser
                {
                    Id = response.PostProcessorData.CustomerId,
                    Avatar = response.PostProcessorData.Avatar,
                    Role = SystemRole.Customer
                };
                //send
                await _sendEndpoint.SendMessage<UpdateSnapshotUser>(updateSnapshotCustomerEvent, ExchangeType.Direct, cancellationToken);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"{functionName} Has error: {ex.Message}");
        }
    }
}