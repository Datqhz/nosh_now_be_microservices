using OrderService.Models.Responses;
using OrderService.Repositories;
using MassTransit.Initializers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Extensions;
using Shared.Helpers;
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
            var food = await
                (
                    from f in _unitOfRepository.Food.GetAll()
                    join c in _unitOfRepository.Category.GetAll()
                        on f.CategoryId equals c.Id
                    where f.Id == foodId
                    select new GetFoodByIdData
                    {
                        FoodId = f.Id,
                        FoodPrice = f.Price,
                        FoodName = f.Name,
                        FoodImage = f.Image,
                        FoodDescription = f.Description,
                        Category = new FoodCategoryData
                        {
                            CategoryId = c.Id.ToString(),
                            CategoryName = c.Name
                        }
                    }
                )
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);
            var requiredIngredient = await
                (
                    from ri in _unitOfRepository.RequiredIngredient.GetAll()
                    join i in _unitOfRepository.Ingredient.GetAll()
                        on ri.IngredientId equals i.Id
                    where ri.FoodId == foodId
                    select new FoodIngredientData
                    {
                        RequiredIngredientId = ri.Id,
                        IngredientName = i.Name,
                        IngredientImage = i.Image,
                        RequiredAmount = ri.Quantity,
                        Unit = i.Unit.GetUnitName()
                    }
                )
                .AsNoTracking()
                .ToListAsync(cancellationToken);
            
            food.FoodIngredients = requiredIngredient;
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