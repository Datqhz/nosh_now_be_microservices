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
                .Where(x => x.RestaurantId.Equals(restaurantId) && !x.IsDeleted)
                .AsNoTracking()
                .Select(x => new GetFoodsByRestaurantData
                {
                    FoodId = x.Id,
                    Price = x.Price,
                    FoodName = x.Name,
                    FoodImage = x.Image
                })
                .AsNoTracking()
                .ToListAsync(cancellationToken);
            var foodIds = foods.Select(x => x.FoodId).ToList();
            var foodAmounts = await
                (
                    from ri in _unitOfRepository.RequiredIngredient
                        .Where(x => foodIds.Contains(x.FoodId))
                    join i in _unitOfRepository.Ingredient.GetAll()
                        on ri.IngredientId equals i.Id
                    let quantity = i.Quantity / ri.Quantity
                    select new
                    {
                        ri.FoodId,
                        quantity
                    }
                )
                .AsNoTracking()
                .ToListAsync(cancellationToken);
            
            foreach (var food in foods)
            {
                food.Available = foodAmounts
                    .Where(x => x.FoodId == food.FoodId)
                    .Min(x => (int)Math.Floor(x.quantity));
            }
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