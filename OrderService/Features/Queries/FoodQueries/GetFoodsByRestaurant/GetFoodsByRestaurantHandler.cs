using OrderService.Models.Responses;
using OrderService.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Extensions;
using Shared.Responses;

namespace OrderService.Features.Queries.FoodQueries.GetFoodsByRestaurant;

public class GetFoodsByRestaurantHandler : IRequestHandler<GetFoodsByRestaurantQuery, GetFoodsByRestaurantResponse>
{
    private readonly IUnitOfRepository _unitOfRepository;
    private readonly ILogger<GetFoodsByRestaurantHandler> _logger;
    public GetFoodsByRestaurantHandler
    (
        IUnitOfRepository unitOfRepository,
        ILogger<GetFoodsByRestaurantHandler> logger
    )
    {
        _unitOfRepository = unitOfRepository;
        _logger = logger;
    }
    public async Task<GetFoodsByRestaurantResponse> Handle(GetFoodsByRestaurantQuery request, CancellationToken cancellationToken)
    {        
        var restaurantId = request.RestaurantId;
        var functionName = $"{nameof(GetFoodsByRestaurantHandler)} RestaurantId = {restaurantId} => ";
        var response = new GetFoodsByRestaurantResponse {StatusCode = (int)ResponseStatusCode.InternalServerError};
        try
        {
            _logger.LogInformation(functionName);
            var foods = await _unitOfRepository.Food
                .Where(x => x.RestaurantId.ToString().Equals(restaurantId))
                .AsNoTracking()
                .Select(x => new GetFoodsByRestaurantData
                {
                    FoodId = x.Id,
                    Price = x.Price,
                    FoodName = x.Name,
                    FoodImage = x.Image,
                })
                .ToListAsync(cancellationToken);

            response.Data = foods;
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