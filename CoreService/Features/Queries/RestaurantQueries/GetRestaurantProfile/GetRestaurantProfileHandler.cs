using CoreService.Features.Queries.RestaurantQueries.GetRestaurantProfile;
using CoreService.Models.Responses;
using CoreService.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.HttpContextAccessor;
using Shared.Responses;

namespace CoreService.Features.Queries.CustomerQueries;

public class GetRestaurantProfileHandler : IRequestHandler<GetRestaurantProfileQuery, GetRestaurantProfileResponse>
{
    private readonly IUnitOfRepository _unitOfRepository;
    private readonly ILogger<GetRestaurantProfileHandler> _logger;
    private readonly ICustomHttpContextAccessor _httpContextAccessor;
    public GetRestaurantProfileHandler
    (
        IUnitOfRepository unitOfRepository,
        ILogger<GetRestaurantProfileHandler> logger,
        ICustomHttpContextAccessor httpContextAccessor
    )
    {
        _unitOfRepository = unitOfRepository;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }
    
    public async Task<GetRestaurantProfileResponse> Handle(GetRestaurantProfileQuery request, CancellationToken cancellationToken)
    {
        var functionName = $"{nameof(GetRestaurantProfileHandler)} => ";
        var response = new GetRestaurantProfileResponse {StatusCode = (int)ResponseStatusCode.InternalServerError};
        
        try
        {
            var currentUserId = _httpContextAccessor.GetCurrentUserId();
            _logger.LogInformation(functionName);
            var restaurant = await
                (
                    from res in _unitOfRepository.Restaurant.GetAll()
                    where res.Id.ToString().Equals(currentUserId)
                          && res.IsActive
                    select new GetRestaurantProfileData
                    {
                        Id = res.Id.ToString(),
                        DisplayName = res.DisplayName,
                        Avatar = res.Avatar,
                        Email = res.Email,
                        Phone = res.PhoneNumber,
                        Coordinate = res.Coordinate
                    }
                )
                .FirstOrDefaultAsync(cancellationToken);

            if (restaurant == null)
            {
                response.ErrorMessage = CoreServiceTranslation.CUS_ERR_01.ToString();
                response.MessageCode = CoreServiceTranslation.CUS_ERR_01.ToString();
                return response;
            }
            
            response.StatusCode = (int)ResponseStatusCode.Ok;
            response.Data = restaurant;
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