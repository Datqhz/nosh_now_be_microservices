using System.Text.Json;
using CoreService.Models.Responses;
using CoreService.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Helpers;
using Shared.Responses;

namespace CoreService.Features.Queries.RestaurantQueries.GetRestaurantByCategory;

public class GetRestaurantByCategoryHandler : IRequestHandler<GetRestaurantByCategoryQuery, GetRestaurantByCategoryResponse>
{
    private readonly IUnitOfRepository _unitOfRepository;
    private readonly ILogger<GetRestaurantByCategoryHandler> _logger;
    public GetRestaurantByCategoryHandler
    (
        IUnitOfRepository unitOfRepository,
        ILogger<GetRestaurantByCategoryHandler> logger
    )
    {
        _unitOfRepository = unitOfRepository;
        _logger = logger;
    }
    public async Task<GetRestaurantByCategoryResponse> Handle(GetRestaurantByCategoryQuery request, CancellationToken cancellationToken)
    {
        var payload = request.Payload;
        var functionName = $"{nameof(GetRestaurantByCategoryHandler)} Payload = {JsonSerializer.Serialize(payload)} => ";
        var response = new GetRestaurantByCategoryResponse {StatusCode = (int)ResponseStatusCode.InternalServerError};
        
        try
        {
            _logger.LogInformation(functionName);
            var restaurants = await
                (
                    from res in _unitOfRepository.Restaurant.GetAll()
                    join food in _unitOfRepository.Food.GetAll()
                        on res.Id equals food.RestaurantId
                    join category in _unitOfRepository.Category.GetAll()
                        on food.CategoryId equals category.Id
                    let distance = LocationHelper.GetDistance(payload.Coordinate, res.Coordinate)
                    where category.Id.ToString().Equals(payload.CategoryId)
                    select new GetRestaurantByCategoryData
                    {
                        RestaurantId = res.Id.ToString(),
                        Avatar = res.Avatar,
                        Distance = distance,
                        RestaurantName = res.DisplayName
                    }
                )
                .AsNoTracking()
                .ToListAsync(cancellationToken);
                // .ToListAsPageAsync(payload.PageNumber, payload.MaxPerPage, cancellationToken);

            response.Data = restaurants;
            response.StatusCode = (int)ResponseStatusCode.Ok;
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