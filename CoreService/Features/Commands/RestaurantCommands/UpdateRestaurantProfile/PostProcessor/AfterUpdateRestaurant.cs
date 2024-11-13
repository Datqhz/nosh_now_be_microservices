using System.Windows.Input;
using CoreService.Features.Commands.CustomerCommands.UpdateCustomerProfile;
using CoreService.Models.Responses;
using MediatR.Pipeline;
using Shared.Enums;
using Shared.MassTransits.Contracts;
using Shared.MassTransits.Core;
using Shared.MassTransits.Enums;
using Shared.Responses;

namespace CoreService.Features.Commands.RestaurantCommands.UpdateRestaurantProfile.PostProcessor;

public class AfterUpdateRestaurantPostProcessor : IRequestPostProcessor<UpdateRestaurantProfileCommand, UpdateRestaurantProfileResponse>
{
    private readonly ILogger<AfterUpdateRestaurantPostProcessor> _logger;
    private readonly ISendEndpointCustomProvider _sendEndpoint;
    public AfterUpdateRestaurantPostProcessor
    (
        ILogger<AfterUpdateRestaurantPostProcessor> logger,
        ISendEndpointCustomProvider sendEndpoint
    )
    {
        _logger = logger;
        _sendEndpoint = sendEndpoint;
    }
    public async Task Process(UpdateRestaurantProfileCommand request, UpdateRestaurantProfileResponse response,
        CancellationToken cancellationToken)
    {
        const string functionName = $"{nameof(AfterUpdateRestaurantPostProcessor)} => ";
        try
        {
            if (response.StatusCode == (int)ResponseStatusCode.Ok)
            {
                var updateSnapshotRestaurantEvent = new UpdateSnapshotUser
                {
                    Id = response.PostProcessorData.Id,
                    Avatar = response.PostProcessorData.Avatar,
                    Role = SystemRole.Restaurant,
                    Coordinate = response.PostProcessorData.Coordinate,
                    Name = response.PostProcessorData.DisplayName,
                    Phone = response.PostProcessorData.Phone,
                };
                //send
                await _sendEndpoint.SendMessage<UpdateSnapshotUser>(updateSnapshotRestaurantEvent, ExchangeType.Direct, cancellationToken);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"{functionName} Has error: {ex.Message}");
        }
    }
}