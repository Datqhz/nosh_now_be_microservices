using CoreService.Models.Requests;
using CoreService.Models.Responses;
using MediatR;

namespace CoreService.Features.Commands.RestaurantCommands.UpdateRestaurantProfile;

public class UpdateRestaurantProfileCommand : IRequest<UpdateRestaurantProfileResponse>
{
    public UpdateRestaurantProfileRequest Payload { get; set; }
    public UpdateRestaurantProfileCommand(UpdateRestaurantProfileRequest payload)
    {
        Payload = payload;
    }
}