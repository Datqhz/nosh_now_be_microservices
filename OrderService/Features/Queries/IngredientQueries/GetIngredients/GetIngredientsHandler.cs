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
        var currentUserId = _httpContextAccessor.GetCurrentUserId();
        var functionName = $"{nameof(GetIngredientsHandler)} Restaurant {currentUserId} =>";
        var response = new GetIngredientsResponse {StatusCode = (int)ResponseStatusCode.InternalServerError};

        try
        {
            _logger.LogInformation(functionName);

            var restaurantId = currentUserId;
            
            var employee = await _unitOfRepository.Employee
                .Where(x => x.Id.Equals(currentUserId))
                .FirstOrDefaultAsync(cancellationToken);

            if (employee is not null)
            {
                restaurantId = employee.RestaurantId;
            }

            var ingredients = await _unitOfRepository.Ingredient
                .Where(x => x.RestaurantId.Equals(restaurantId))
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