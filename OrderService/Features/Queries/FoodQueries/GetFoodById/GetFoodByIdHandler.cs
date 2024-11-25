using OrderService.Models.Responses;
using OrderService.Repositories;
using MassTransit.Initializers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Extensions;
using Shared.Responses;

namespace OrderService.Features.Queries.FoodQueries.GetFoodById;

public class GetFoodByIdHandler : IRequestHandler<GetFoodByIdQuery, GetFoodByIdResponse>
{
    private readonly IUnitOfRepository _unitOfRepository;
    private readonly ILogger<GetFoodByIdHandler> _logger;
    public GetFoodByIdHandler
    (
        IUnitOfRepository unitOfRepository,
        ILogger<GetFoodByIdHandler> logger
    )
    {
        _unitOfRepository = unitOfRepository;
        _logger = logger;
    }
    
    public async Task<GetFoodByIdResponse> Handle(GetFoodByIdQuery request, CancellationToken cancellationToken)
    {
        var foodId = request.FoodId;
        var functionName = $"{nameof(GetFoodByIdHandler)} FoodId = {foodId} => ";
        var response = new GetFoodByIdResponse {StatusCode = (int)ResponseStatusCode.InternalServerError};
        
        try
        {
            _logger.LogInformation(functionName);
            /* Todo: Join with Ingredient and Required-Ingredient to calculate amount of available food */
            var food = await _unitOfRepository.Food.GetAll()
                .Where(x => x.Id == foodId)
                .Select(x => new GetFoodByIdData
                {
                    FoodId = x.Id,
                    FoodPrice = x.Price,
                    FoodName = x.Name,
                    FoodImage = x.Image,
                    FoodDescription = x.Description
                })
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);

            response.Data = food;
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