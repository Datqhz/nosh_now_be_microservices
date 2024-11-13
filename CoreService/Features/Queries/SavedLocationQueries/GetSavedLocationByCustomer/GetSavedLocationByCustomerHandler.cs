using System.Text.Json;
using CoreService.Models.Responses;
using CoreService.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Helpers;
using Shared.HttpContextAccessor;
using Shared.Responses;

namespace CoreService.Features.Queries.SavedLocationQueries.GetSavedLocationByCustomer;

public class GetSavedLocationByCustomerHandler: IRequestHandler<GetSavedLocationByCustomerQuery, GetSavedLocationByCustomerResponse>    
{
    private readonly IUnitOfRepository _unitOfRepository;
    private readonly ILogger<GetSavedLocationByCustomerHandler> _logger;
    private readonly ICustomHttpContextAccessor _httpContextAccessor;
    public GetSavedLocationByCustomerHandler
    (
        IUnitOfRepository unitOfRepository,
        ILogger<GetSavedLocationByCustomerHandler> logger,
        ICustomHttpContextAccessor httpContextAccessor
    )
    {
        _unitOfRepository = unitOfRepository;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }
    public async Task<GetSavedLocationByCustomerResponse> Handle(GetSavedLocationByCustomerQuery request, CancellationToken cancellationToken)
    {
        
        var functionName = $"{nameof(GetSavedLocationByCustomerHandler)} CustomerId = {{id}} => ";
        var response = new GetSavedLocationByCustomerResponse {StatusCode = (int)ResponseStatusCode.InternalServerError};
        try
        {
            var currentUserId = _httpContextAccessor.GetCurrentUserId();
            functionName = functionName.Replace("id", currentUserId);
            _logger.LogInformation(functionName);
            var locations = await _unitOfRepository.Location
                .Where(x => x.CustomerId.ToString().Equals(currentUserId))
                .AsNoTracking()
                .Select(x => new GetSavedLocationByCustomerData
                {
                    Id = x.Id,
                    Coordinate = x.Coordinate,
                    Name = x.Name,
                    Phone = x.Phone
                })
                .ToListAsync(cancellationToken);
                //.ToListAsPageAsync(payload.PageNumber, payload.MaxPerPage, cancellationToken);

            response.Data =locations;
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