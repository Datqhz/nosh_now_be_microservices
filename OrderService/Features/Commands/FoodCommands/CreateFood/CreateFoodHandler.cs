using System.Text.Json;
using MediatR;
using OrderService.Data.Models;
using OrderService.Models.Responses;
using OrderService.Repositories;
using Shared.Extensions;
using Shared.HttpContextAccessor;
using Shared.Responses;

namespace OrderService.Features.Commands.FoodCommands.CreateFood;

public class CreateFoodHandler : IRequestHandler<CreateFoodCommand, CreateFoodResponse>
{
    private readonly IUnitOfRepository _unitOfRepository;
    private readonly ILogger<CreateFoodHandler> _logger;
    private readonly ICustomHttpContextAccessor _httpContextAccessor;
    public CreateFoodHandler
    (
        IUnitOfRepository unitOfRepository,
        ILogger<CreateFoodHandler> logger,
        ICustomHttpContextAccessor httpContextAccessor
    )
    {
        _unitOfRepository = unitOfRepository;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }
    
    public async Task<CreateFoodResponse> Handle(CreateFoodCommand request, CancellationToken cancellationToken)
    {
        var payload = request.Payload;
        var functionName = $"{nameof(CreateFoodHandler)} Payload = {JsonSerializer.Serialize(payload)} =>";
        var response = new CreateFoodResponse {StatusCode = (int)ResponseStatusCode.InternalServerError};

        try
        {
            var currentUserId = _httpContextAccessor.GetCurrentUserId();
            var food = new Food
            {
                Name = payload.FoodName,
                Price = payload.FoodPrice,
                Image = payload.FoodImage,
                RestaurantId = currentUserId,
                CategoryId = new Guid(payload.CategoryId),
                Description = payload.FoodDescription
            };
            var newFood = await _unitOfRepository.Food.Add(food);
            await _unitOfRepository.CompleteAsync();
            
            var requiredIngredients = new List<RequiredIngredient>();
            foreach (var ingredient in payload.Ingredients)
            {
                var requiredIngredient = new RequiredIngredient
                {
                    FoodId = newFood.Id,
                    IngredientId = ingredient.IngredientId,
                    Quantity = ingredient.Quantity,
                };
                requiredIngredients.Add(requiredIngredient);
            }
            await _unitOfRepository.RequiredIngredient.AddRange(requiredIngredients);
            await _unitOfRepository.CompleteAsync();
            
            response.StatusCode = (int)ResponseStatusCode.Ok;
            response.Data = newFood.Id;
        }
        catch (Exception ex)
        {
            ex.LogError(functionName, _logger);
            response.StatusCode = (int)ResponseStatusCode.InternalServerError;
        }

        return response;
    }
}