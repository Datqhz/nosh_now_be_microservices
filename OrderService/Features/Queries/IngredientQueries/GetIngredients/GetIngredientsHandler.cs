using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderService.Models.Responses;
using OrderService.Repositories;
using Shared.Extensions;
using Shared.HttpContextAccessor;
using Shared.Responses;

namespace OrderService.Features.Queries.IngredientQueries.GetIngredients;

public class GetIngredientsHandler : IRequestHandler<GetIngredientsQuery, GetIngredientsResponse>
{
    private readonly IUnitOfRepository _unitOfRepository;
    private readonly ILogger<GetIngredientsHandler> _logger;
    private readonly ICustomHttpContextAccessor _httpContextAccessor;
    public GetIngredientsHandler
    (
        IUnitOfRepository unitOfRepository,
        ILogger<GetIngredientsHandler> logger,
        ICustomHttpContextAccessor httpContextAccessor
    )
    {
        _unitOfRepository = unitOfRepository;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }
    public async Task<GetIngredientsResponse> Handle(GetIngredientsQuery request, CancellationToken cancellationToken)
    {
        var functionName = $"{nameof(GetIngredientsHandler)}";
        var response = new GetIngredientsResponse {StatusCode = (int)ResponseStatusCode.InternalServerError};

        try
        {
            var currentUserId = _httpContextAccessor.GetCurrentUserId();
            functionName = $"{nameof(GetIngredientsHandler)} Restaurant {currentUserId}";
            _logger.LogInformation(functionName);

            var ingredients = await _unitOfRepository.Ingredient
                .Where(x => x.RestaurantId == currentUserId)
                .Select(x => new GetIngredientsData
                {
                    Id = x.Id,
                    Image = x.Image,
                    Name = x.Name,
                    Quantity = x.Quantity,
                    Unit = x.Unit,
                })
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            response.Data = ingredients;
            response.StatusCode = (int)ResponseStatusCode.Ok;
        }
        catch (Exception ex)
        {
            ex.LogError(functionName, _logger);
        }

        return response;
    }
}