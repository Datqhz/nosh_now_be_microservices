using System.Text.Json;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderService.Data.Models;
using OrderService.Models.Responses;
using OrderService.Repositories;
using Shared.Extensions;
using Shared.HttpContextAccessor;
using Shared.Responses;

namespace OrderService.Features.Commands.CategoryCommands.UpdateCategory;

public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryCommand, UpdateCategoryResponse>
{
    private readonly IUnitOfRepository _unitOfRepository;
    private readonly ILogger<UpdateCategoryHandler> _logger;
    private readonly ICustomHttpContextAccessor _httpContextAccessor;
    public UpdateCategoryHandler
    (
        IUnitOfRepository unitOfRepository,
        ILogger<UpdateCategoryHandler> logger,
        ICustomHttpContextAccessor httpContextAccessor
    )
    {
        _unitOfRepository = unitOfRepository;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }
    
    public async Task<UpdateCategoryResponse> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var payload = request.Payload;
        var functionName = $"{nameof(UpdateCategoryHandler)} Payload = {JsonSerializer.Serialize(payload)} =>";
        var response = new UpdateCategoryResponse {StatusCode = (int)ResponseStatusCode.InternalServerError};

        try
        {
            // var currentUserId = _httpContextAccessor.GetCurrentUserId();
            var category = await _unitOfRepository.Category
                .Where(x => x.Id.ToString().Equals(payload.CategoryId))
                .FirstOrDefaultAsync(cancellationToken);

            if (category is null)
            {
                _logger.LogWarning($"{functionName} Category not found");
            }
            
            _unitOfRepository.Category.Update(category);
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