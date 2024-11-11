using CoreService.Models.Responses;
using CoreService.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Extensions;
using Shared.Responses;

namespace CoreService.Features.Queries.FoodQueries.GetRandomFood;

public class GetFoodsHandler : IRequestHandler<GetFoodsQuery, GetFoodsResponse>
{
    private readonly IUnitOfRepository _unitOfRepository;
    private readonly ILogger<GetFoodsHandler> _logger;
    public GetFoodsHandler
    (
        IUnitOfRepository unitOfRepository,
        ILogger<GetFoodsHandler> logger
    )
    {
        _unitOfRepository = unitOfRepository;
        _logger = logger;
    }
    
    public async Task<GetFoodsResponse> Handle(GetFoodsQuery request, CancellationToken cancellationToken)
    {
        var payload = request.Payload;
        var functionName = $"{nameof(GetFoodsHandler)} Payload = {payload} => ";
        var response = new GetFoodsResponse {StatusCode = (int)ResponseStatusCode.InternalServerError};
        
        try
        {
            _logger.LogInformation(functionName);
            /* Todo: Join with Ingredient and Required-Ingredient to calculate amount of available food */
            var pagination = await _unitOfRepository.Food.GetAll()
                .AsNoTracking()
                .Select(x => new GetRandomFoodData
                {
                    FoodId = x.Id,
                    Price = x.Price,
                    FoodName = x.Name,
                    FoodImage = x.Image,
                    RestaurantId = x.RestaurantId.ToString(),
                })
                .ToListAsPageAsync(payload.PageNumber, payload.MaxPerPage, cancellationToken);

            response.Data = pagination.Data;
            response.Paging = pagination.Paging;
            response.StatusCode = (int)ResponseStatusCode.Ok;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"{functionName} Has error: {ex.Message}");
            response.ErrorMessage = CoreServiceTranslation.EXH_ERR_01.ToString();
            response.MessageCode = CoreServiceTranslation.EXH_ERR_01.ToString();
        }

        return response;
    }
}