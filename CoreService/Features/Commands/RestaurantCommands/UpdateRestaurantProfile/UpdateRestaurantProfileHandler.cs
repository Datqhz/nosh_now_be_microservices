using System.Text.Json;
using CoreService.Models.Responses;
using CoreService.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.HttpContextAccessor;
using Shared.Responses;

namespace CoreService.Features.Commands.RestaurantCommands.UpdateRestaurantProfile;

public class UpdateRestaurantProfileHandler : IRequestHandler<UpdateRestaurantProfileCommand, UpdateRestaurantProfileResponse>
{
    private readonly IUnitOfRepository _unitOfRepository;
    private readonly ILogger<UpdateRestaurantProfileHandler> _logger;
    private readonly ICustomHttpContextAccessor _httpContextAccessor;
    public UpdateRestaurantProfileHandler
    (
        IUnitOfRepository unitOfRepository,
        ILogger<UpdateRestaurantProfileHandler> logger,
        ICustomHttpContextAccessor httpContextAccessor
    )
    {
        _unitOfRepository = unitOfRepository;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }
    public async Task<UpdateRestaurantProfileResponse> Handle(UpdateRestaurantProfileCommand request, CancellationToken cancellationToken)
    {
        var payload = request.Payload;
        var functionName = $"{nameof(UpdateRestaurantProfileHandler)} Payload = {JsonSerializer.Serialize(payload)} => ";
        var response = new UpdateRestaurantProfileResponse {StatusCode = (int)ResponseStatusCode.BadRequest};
        
        try
        {
            var currentUserId = _httpContextAccessor.GetCurrentUserId();
            _logger.LogInformation(functionName);
            var restaurant = await
                (
                    from cus in _unitOfRepository.Restaurant.GetAll()
                    where cus.Id.ToString().Equals(currentUserId)
                          && cus.IsActive
                    select cus
                )
                .FirstOrDefaultAsync(cancellationToken);

            if (restaurant == null)
            {
                response.ErrorMessage = CoreServiceTranslation.CUS_ERR_01.ToString();
                response.MessageCode = CoreServiceTranslation.CUS_ERR_01.ToString();
                return response;
            }
            
            restaurant.Avatar = payload.Avatar;
            restaurant.PhoneNumber = payload.PhoneNumber;
            restaurant.DisplayName = payload.DisplayName;
            restaurant.Coordinate = payload.Coordinate;
            
            _unitOfRepository.Restaurant.Update(restaurant);
            await _unitOfRepository.CompleteAsync();
            
            response.StatusCode = (int)ResponseStatusCode.Ok;
            response.PostProcessorData = new UpdateRestaurantProfilePostProcessorData
            {
                Avatar = payload.Avatar,
                DisplayName = payload.DisplayName,
                Id = restaurant.Id.ToString(),
                Coordinate = restaurant.Coordinate,
                Phone = payload.PhoneNumber,
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"{functionName} Has error: {ex.Message}");
            response.ErrorMessage = CoreServiceTranslation.EXH_ERR_01.ToString();
            response.MessageCode = CoreServiceTranslation.EXH_ERR_01.ToString();
        }

        return response;
    }
}