using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderService.Models.Responses;
using OrderService.Repositories;
using Shared.Extensions;
using Shared.HttpContextAccessor;
using Shared.Responses;

namespace OrderService.Features.Commands.FoodCommands.DeleteFood;

public class DeleteFoodHandler : IRequestHandler<DeleteFoodCommand, DeleteFoodResponse>
{
    private readonly IUnitOfRepository _unitOfRepository;
    private readonly ILogger<DeleteFoodHandler> _logger;
    private readonly ICustomHttpContextAccessor _httpContextAccessor;
    public DeleteFoodHandler
    (
        IUnitOfRepository unitOfRepository,
        ILogger<DeleteFoodHandler> logger,
        ICustomHttpContextAccessor httpContextAccessor
    )
    {
        _unitOfRepository = unitOfRepository;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }
    
    public async Task<DeleteFoodResponse> Handle(DeleteFoodCommand request, CancellationToken cancellationToken)
    {
        var functionName = $"{nameof(DeleteFoodHandler)} FoodId {request.FoodId} =>";
        var response = new DeleteFoodResponse {StatusCode = (int)ResponseStatusCode.BadRequest};

        try
        {
            _logger.LogInformation(functionName);
            var currentUserId = _httpContextAccessor.GetCurrentUserId();
            var food = await _unitOfRepository.Food
                .Where(x => x.Id == request.FoodId && !x.IsDeleted)
                .FirstOrDefaultAsync(cancellationToken);
            if (food is null)
            {
                _logger.LogWarning($"{functionName} Food not found or already deleted");
                return response;
            }

            if (food.RestaurantId != currentUserId)
            {
                _logger.LogWarning($"{functionName} Permission denied");
                response.StatusCode = (int)ResponseStatusCode.Forbidden;
                return response;
            }

            food.IsDeleted = true;
            _unitOfRepository.Food.Update(food);
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