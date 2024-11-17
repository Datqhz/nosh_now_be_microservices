using System.Text.Json;
using MediatR;
using OrderService.Models.Responses;
using OrderService.Repositories;
using Shared.Extensions;
using Shared.HttpContextAccessor;
using Shared.Responses;

namespace OrderService.Features.Commands.IngredientCommands.DeleteIngredient;

public class DeleteIngredientHandler : IRequestHandler<DeleteIngredientCommand, DeleteIngredientResponse>
{
    private readonly IUnitOfRepository _unitOfRepository;
    private readonly ILogger<DeleteIngredientHandler> _logger;
    private readonly ICustomHttpContextAccessor _httpContextAccessor;
    public DeleteIngredientHandler
    (
        IUnitOfRepository unitOfRepository,
        ILogger<DeleteIngredientHandler> logger,
        ICustomHttpContextAccessor httpContextAccessor
    )
    {
        _unitOfRepository = unitOfRepository;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }
    public async Task<DeleteIngredientResponse> Handle(DeleteIngredientCommand request, CancellationToken cancellationToken)
    {
        var functionName = $"{nameof(DeleteIngredientHandler)} =>";
        var response = new DeleteIngredientResponse {StatusCode = (int)ResponseStatusCode.BadRequest};

        try
        {
            _logger.LogInformation(functionName);
            var currentUserId = _httpContextAccessor.GetCurrentUserId();
            var ingredient = await _unitOfRepository.Ingredient.GetById(request.IngredientId);
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

            _unitOfRepository.Ingredient.Delete(ingredient);
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