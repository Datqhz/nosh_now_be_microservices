using OrderService.Models.Responses;
using OrderService.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Responses;

namespace OrderService.Features.Queries.CategoryQueries.GetCategories;

public class GetCategoriesHandler : IRequestHandler<GetCategoriesQuery, GetCategoriesResponse>
{
    private readonly IUnitOfRepository _unitOfRepository;
    private readonly ILogger<GetCategoriesHandler> _logger;
    public GetCategoriesHandler
    (
        IUnitOfRepository unitOfRepository,
        ILogger<GetCategoriesHandler> logger
    )
    {
        _unitOfRepository = unitOfRepository;
        _logger = logger;
    }
    public async Task<GetCategoriesResponse> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        var functionName = $"{nameof(GetCategoriesHandler)} => ";
        var response = new GetCategoriesResponse {StatusCode = (int)ResponseStatusCode.InternalServerError};
        try
        {
            _logger.LogInformation(functionName);
            var categories = await _unitOfRepository.Category.GetAll()
                .AsNoTracking()
                .Select(x => new GetCategoriesData
                {
                    CategoryId = x.Id.ToString(),
                    CategoryName = x.Name,
                    CategoryImage = x.Image
                })
                .ToListAsync(cancellationToken);

            response.Data = categories;
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