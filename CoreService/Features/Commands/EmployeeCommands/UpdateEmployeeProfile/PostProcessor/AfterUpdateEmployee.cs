using System.Windows.Input;
using CoreService.Models.Responses;
using MediatR.Pipeline;
using Shared.Enums;
using Shared.MassTransits.Contracts;
using Shared.MassTransits.Core;
using Shared.MassTransits.Enums;
using Shared.Responses;

namespace CoreService.Features.Commands.EmployeeCommands.UpdateEmployeeProfile.PostProcessor;

public class AfterUpdateEmployeePostProcessor : IRequestPostProcessor<UpdateEmployeeProfileCommand, UpdateEmployeeProfileResponse>
{
    private readonly ILogger<AfterUpdateEmployeePostProcessor> _logger;
    private readonly ISendEndpointCustomProvider _sendEndpoint;
    public AfterUpdateEmployeePostProcessor
    (
        ILogger<AfterUpdateEmployeePostProcessor> logger,
        ISendEndpointCustomProvider sendEndpoint
    )
    {
        _logger = logger;
        _sendEndpoint = sendEndpoint;
    }
    public async Task Process(UpdateEmployeeProfileCommand request, UpdateEmployeeProfileResponse response,
        CancellationToken cancellationToken)
    {
        const string functionName = $"{nameof(AfterUpdateEmployeePostProcessor)} => ";
        try
        {
            if (response.StatusCode == (int)ResponseStatusCode.Ok)
            {
                var updateSnapshotCustomerEvent = new UpdateSnapshotUser
                {
                    Id = request.Payload.EmployeeId,
                    Avatar = request.Payload.Avatar,
                    Role = response.PostProcessorData,
                    Name = request.Payload.DisplayName,
                    Phone = request.Payload.PhoneNumber,
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