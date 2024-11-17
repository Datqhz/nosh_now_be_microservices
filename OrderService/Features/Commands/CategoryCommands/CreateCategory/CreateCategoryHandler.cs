using System.Text.Json;
using MediatR;
using OrderService.Data.Models;
using OrderService.Models.Responses;
using OrderService.Repositories;
using Shared.Extensions;
using Shared.HttpContextAccessor;
using Shared.Responses;

namespace OrderService.Features.Commands.CategoryCommands.CreateCategory;

public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, CreateCategoryResponse>
{
    private readonly IUnitOfRepository _unitOfRepository;
    private readonly ILogger<CreateCategoryHandler> _logger;
    private readonly ICustomHttpContextAccessor _httpContextAccessor;
    public CreateCategoryHandler
    (
        IUnitOfRepository unitOfRepository,
        ILogger<CreateCategoryHandler> logger,
        ICustomHttpContextAccessor httpContextAccessor
    )
    {
        _unitOfRepository = unitOfRepository;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }
    
    public async Task<CreateCategoryResponse> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var payload = request.Payload;
        var functionName = $"{nameof(CreateCategoryHandler)} Payload = {JsonSerializer.Serialize(payload)} =>";
        var response = new CreateCategoryResponse {StatusCode = (int)ResponseStatusCode.InternalServerError};

        try
        {
            // var currentUserId = _httpContextAccessor.GetCurrentUserId();
            var category = new Category
            {
                Name = payload.CategoryName,
                Image = payload.Image
            };
            await _unitOfRepository.Category.Add(category);
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