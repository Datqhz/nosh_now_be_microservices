using CoreService.Models.Responses;
using CoreService.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Extensions;
using Shared.HttpContextAccessor;
using Shared.Responses;

namespace CoreService.Features.Queries.EmployeeQueries.GetEmployees;

public class GetEmployeesHandler : IRequestHandler<GetEmployeesQuery, GetEmployeesResponse>
{
    private readonly IUnitOfRepository _unitOfRepository;
    private readonly ILogger<GetEmployeesHandler> _logger;
    private readonly ICustomHttpContextAccessor _httpContextAccessor;
    public GetEmployeesHandler
    (
        IUnitOfRepository unitOfRepository,
        ILogger<GetEmployeesHandler> logger,
        ICustomHttpContextAccessor httpContextAccessor
    )
    {
        _unitOfRepository = unitOfRepository;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }
    
    public async Task<GetEmployeesResponse> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
    {
        var payload = request.Payload;
        var currentUserId = _httpContextAccessor.GetCurrentUserId();
        var functionName = $"{nameof(GetEmployeesHandler)} RestaurantId: {currentUserId}, Payload: {payload} =>";
        _logger.LogInformation(functionName);
        var response = new GetEmployeesResponse { StatusCode = (int)ResponseStatusCode.Ok };

        try
        {
            var pagination = await
                (
                    from e in _unitOfRepository.Employee.GetAll()
                    where e.RestaurantId.ToString().Equals(currentUserId)
                          && e.IsActive
                          && e.Role == payload.Role
                    select new GetEmployeesData
                    {
                        Id = e.Id.ToString(),
                        DisplayName = e.DisplayName,
                        Avatar = e.Avatar,
                        Email = e.Email,
                        Role = e.Role,
                        PhoneNumber = e.PhoneNumber,
                    }
                )
                .AsNoTracking()
                .ToListAsPageAsync(payload.PageNumber, payload.MaxPerPage, cancellationToken);

            response.Paging = pagination.Paging;
            response.Data = pagination.Data;
        }
        catch (Exception ex)
        {
            ex.LogError($"{functionName} Has error: {ex.Message}", _logger);
            response.StatusCode = (int)ResponseStatusCode.InternalServerError;
            response.ErrorMessage = "An error has occurred.";
        }

        return response;
    }
}
