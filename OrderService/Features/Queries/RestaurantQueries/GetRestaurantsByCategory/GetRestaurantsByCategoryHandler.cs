using System.Text.Json;
using OrderService.Models.Responses;
using OrderService.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderService.Data.Models;
using Shared.Extensions;
using Shared.Helpers;
using Shared.Responses;

namespace OrderService.Features.Queries.RestaurantQueries.GetRestaurants;

public class GetRestaurantsByCategoryHandler : IRequestHandler<GetRestaurantsByCategoryQuery, GetRestaurantsByCategoryResponse>
{
    private readonly IUnitOfRepository _unitOfRepository;
    private readonly ILogger<GetRestaurantsByCategoryHandler> _logger;
    public GetRestaurantsByCategoryHandler
    (
        IUnitOfRepository unitOfRepository,
        ILogger<GetRestaurantsByCategoryHandler> logger
    )
    {
        _unitOfRepository = unitOfRepository;
        _logger = logger;
    }
    public async  Task<GetRestaurantsByCategoryResponse> Handle(GetRestaurantsByCategoryQuery request, CancellationToken cancellationToken)
    {
        var payload = request.Payload;
        var functionName = $"{nameof(GetRestaurantsByCategoryHandler)} Payload = {JsonSerializer.Serialize(payload)} => ";
        var response = new GetRestaurantsByCategoryResponse {StatusCode = (int)ResponseStatusCode.InternalServerError};
        try
        {
            _logger.LogInformation(functionName);
            /* Todo: Join with calendar table to select restaurant status */
            var pagination = await 
                (
                    from res in _unitOfRepository.Restaurant.GetAll()
                    join food in _unitOfRepository.Food.GetAll() 
                        on res.Id equals food.RestaurantId
                    join category in _unitOfRepository.Category.GetAll()
                        on food.CategoryId equals category.Id
                    where category.Id.ToString().Equals(payload.CategoryId)
                    select new GetRestaurantsByCategoryData
                    {
                        RestaurantId = res.Id,
                        RestaurantName = res.Name,
                        Coordinate = res.Coordinate,
                        Avatar = res.Avatar
                    }
                )
                .AsNoTracking()
                .ToListAsPageAsync(payload.PageNumber, payload.MaxPerPage, cancellationToken);

            if (pagination.Data.Any())
            {
                foreach (var restaurant in pagination.Data)
                {
                    restaurant.Distance = LocationHelper.GetDistance(restaurant.Coordinate, payload.Coordinate);
                }
            }
            response.Data = pagination.Data;
            response.Paging = pagination.Paging;
            response.StatusCode = (int)ResponseStatusCode.Ok;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"{functionName} Has error: {ex.Message}");
            response.ErrorMessage = OrderServiceTranslation.EXH_ERR_01.ToString();
            response.MessageCode = OrderServiceTranslation.EXH_ERR_01.ToString();
        }

        return response;
    }
}