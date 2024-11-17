using System.Text.Json;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderService.Data.Models;
using OrderService.Enums;
using OrderService.Models.Responses;
using OrderService.Repositories;
using Shared.Extensions;
using Shared.HttpContextAccessor;
using Shared.Responses;

namespace OrderService.Features.Commands.FoodCommands.UpdateFood;

public class UpdateFoodHandler : IRequestHandler<UpdateFoodCommand, UpdateFoodResponse>
{
    private readonly IUnitOfRepository _unitOfRepository;
    private readonly ILogger<UpdateFoodHandler> _logger;
    private readonly ICustomHttpContextAccessor _httpContextAccessor;
    public UpdateFoodHandler
    (
        IUnitOfRepository unitOfRepository,
        ILogger<UpdateFoodHandler> logger,
        ICustomHttpContextAccessor httpContextAccessor
    )
    {
        _unitOfRepository = unitOfRepository;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }
    
    public async Task<UpdateFoodResponse> Handle(UpdateFoodCommand request, CancellationToken cancellationToken)
    {
        var payload = request.Payload;
        var functionName = $"{nameof(UpdateFoodHandler)} Payload = {JsonSerializer.Serialize(payload)} =>";
        var response = new UpdateFoodResponse {StatusCode = (int)ResponseStatusCode.BadRequest};

        try
        {
            var currentUserId = _httpContextAccessor.GetCurrentUserId();
            var food = await _unitOfRepository.Food
                .Where(x => x.Id == payload.FoodId && !x.IsDeleted)
                .FirstOrDefaultAsync(cancellationToken);
            if (food is null)
            {
                _logger.LogWarning($"{functionName} Food not found");
                response.StatusCode = (int)ResponseStatusCode.NotFound;
                return response;
            }

            if (!food.RestaurantId.Equals(currentUserId))
            {
                _logger.LogWarning($"{functionName} Permission denied");
                response.StatusCode = (int)ResponseStatusCode.Forbidden;
                return response;
            }
            /* 1. Update food information */
            await using var transaction = await _unitOfRepository.OpenTransactionAsync();
            food.Name = payload.FoodName;
            food.Price = payload.FoodPrice;
            food.Image = payload.FoodImage;
            food.CategoryId = new Guid(payload.CategoryId);
            food.Description = payload.FoodDescription;
            _unitOfRepository.Food.Update(food);
            
            var requiredIngredients = await _unitOfRepository.RequiredIngredient
                .Where(x => x.FoodId == food.Id)
                .ToListAsync(cancellationToken);
            
            /* 2. Update required ingredient */
            foreach (var ingredient in payload.Ingredients)
            {
                var record = requiredIngredients
                    .FirstOrDefault(x => x.Id == ingredient.RequiredIngredientId);
                if (record is null)
                {
                    continue;
                }

                if (ingredient.ModifyOption == ModifyOption.Update)
                {
                    record.Quantity = ingredient.Quantity;
                    _unitOfRepository.RequiredIngredient.Update(record);
                    continue;
                }

                if (ingredient.ModifyOption == ModifyOption.Delete)
                {
                    _unitOfRepository.RequiredIngredient.Delete(record);
                }
            }

            await _unitOfRepository.CompleteAsync();
            await _unitOfRepository.CommitAsync();
            response.StatusCode = (int)ResponseStatusCode.Ok;
        }
        catch (Exception ex)
        {
            ex.LogError(functionName, _logger);
            response.StatusCode = (int)ResponseStatusCode.InternalServerError;
        }

        return response;
    }
}