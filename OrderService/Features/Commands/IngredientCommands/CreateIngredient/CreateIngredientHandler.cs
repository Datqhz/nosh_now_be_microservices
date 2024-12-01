using System.Text.Json;
using MediatR;
using OrderService.Data.Models;
using OrderService.Models.Responses;
using OrderService.Repositories;
using Shared.Extensions;
using Shared.HttpContextAccessor;
using Shared.Responses;

namespace OrderService.Features.Commands.IngredientCommands.CreateIngredient;

public class CreateIngredientHandler : IRequestHandler<CreateIngredientCommand, CreateIngredientResponse>
{
    private readonly IUnitOfRepository _unitOfRepository;
    private readonly ILogger<CreateIngredientHandler> _logger;
    private readonly ICustomHttpContextAccessor _httpContextAccessor;
    public CreateIngredientHandler
    (
        IUnitOfRepository unitOfRepository,
        ILogger<CreateIngredientHandler> logger,
        ICustomHttpContextAccessor httpContextAccessor
    )
    {
        _unitOfRepository = unitOfRepository;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }
    
    public async Task<CreateIngredientResponse> Handle(CreateIngredientCommand request, CancellationToken cancellationToken)
    {
        var payload = request.Payload;
        var functionName = $"{nameof(CreateIngredientHandler)} Payload : {JsonSerializer.Serialize(payload)} =>";
        var response = new CreateIngredientResponse {StatusCode = (int)ResponseStatusCode.BadRequest};

        try
        {
            _logger.LogInformation(functionName);
            var currentUserId = _httpContextAccessor.GetCurrentUserId();

            var ingredient = new Ingredient
            {
                Name = payload.IngredientName,
                Quantity = payload.Quantity,
                Unit = payload.Unit,
                RestaurantId = currentUserId,
                Image = payload.Image,
            };

            var newIngredient = await _unitOfRepository.Ingredient.Add(ingredient);
            await _unitOfRepository.CompleteAsync();

            response.StatusCode = (int)ResponseStatusCode.Ok;
            response.Data = newIngredient.Id;
        }
        catch (Exception ex)
        {
            ex.LogError(functionName, _logger);
            response.StatusCode = (int)ResponseStatusCode.InternalServerError;
        }

        return response;
    }
}