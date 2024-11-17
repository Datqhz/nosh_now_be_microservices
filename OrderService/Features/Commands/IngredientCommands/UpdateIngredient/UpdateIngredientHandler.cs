using System.Text.Json;
using MediatR;
using OrderService.Data.Models;
using OrderService.Models.Responses;
using OrderService.Repositories;
using Shared.Extensions;
using Shared.HttpContextAccessor;
using Shared.Responses;

namespace OrderService.Features.Commands.IngredientCommands.UpdateIngredient;

public class UpdateIngredientHandler : IRequestHandler<UpdateIngredientCommand, UpdateIngredientResponse>
{
    private readonly IUnitOfRepository _unitOfRepository;
    private readonly ILogger<UpdateIngredientHandler> _logger;
    private readonly ICustomHttpContextAccessor _httpContextAccessor;
    public UpdateIngredientHandler
    (
        IUnitOfRepository unitOfRepository,
        ILogger<UpdateIngredientHandler> logger,
        ICustomHttpContextAccessor httpContextAccessor
    )
    {
        _unitOfRepository = unitOfRepository;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }
    
    public async Task<UpdateIngredientResponse> Handle(UpdateIngredientCommand request, CancellationToken cancellationToken)
    {
        var payload = request.Payload;
        var functionName = $"{nameof(UpdateIngredientHandler)} Payload : {JsonSerializer.Serialize(payload)} =>";
        var response = new UpdateIngredientResponse {StatusCode = (int)ResponseStatusCode.BadRequest};

        try
        {
            _logger.LogInformation(functionName);
            var currentUserId = _httpContextAccessor.GetCurrentUserId();
            var ingredient = await _unitOfRepository.Ingredient.GetById(payload.IngredientId);
            if (ingredient is null)
            {
                _logger.LogWarning($"{functionName} Ingredient not found");
                response.StatusCode = (int)ResponseStatusCode.NotFound;
                return response;
            }

            if (ingredient.RestaurantId != currentUserId)
            {
                _logger.LogWarning($"{functionName} Permission denied");
                response.StatusCode = (int)ResponseStatusCode.Forbidden;
                return response;
            }

            ingredient.Name = payload.IngredientName;
            ingredient.Quantity = payload.Quantity;
            ingredient.Unit = payload.Unit;
            ingredient.Image = payload.Image;

            _unitOfRepository.Ingredient.Update(ingredient);
            await _unitOfRepository.CompleteAsync();

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