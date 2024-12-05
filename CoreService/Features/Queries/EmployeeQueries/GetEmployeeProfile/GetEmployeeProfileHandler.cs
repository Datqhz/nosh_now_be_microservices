using CoreService.Models.Responses;
using CoreService.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Extensions;
using Shared.HttpContextAccessor;
using Shared.Responses;

namespace CoreService.Features.Queries.EmployeeQueries.GetEmployeeProfile;

public class GetEmployeeProfileHandler : IRequestHandler<GetEmployeeProfileQuery, GetEmployeeProfileResponse>
{
    private readonly IUnitOfRepository _unitOfRepository;
    private readonly ILogger<GetEmployeeProfileHandler> _logger;
    private readonly ICustomHttpContextAccessor _httpContextAccessor;
    public GetEmployeeProfileHandler
    (
        IUnitOfRepository unitOfRepository,
        ILogger<GetEmployeeProfileHandler> logger,
        ICustomHttpContextAccessor httpContextAccessor
    )
    {
        _unitOfRepository = unitOfRepository;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }
    public async Task<GetEmployeeProfileResponse> Handle(GetEmployeeProfileQuery request, CancellationToken cancellationToken)
    {
        var currentUserId = _httpContextAccessor.GetCurrentUserId();
        var functionName = $"{nameof(GetEmployeeProfileHandler)} EmployeeId: {currentUserId} =>";
        _logger.LogInformation(functionName);
        var response = new GetEmployeeProfileResponse { StatusCode = (int)ResponseStatusCode.Ok };

        try
        {
            var employee = await _unitOfRepository.Employee
                .Where(x =>x.Id.ToString().Equals(currentUserId) && x.IsActive)
                .Select(x => new GetEmployeeProfileData
                {
                    Id = x.Id.ToString(),
                    DisplayName = x.DisplayName,
                    Avatar = x.Avatar,
                    Email = x.Email,
                    Phone = x.PhoneNumber,
                })
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);
            if (employee is null)
            {
                _logger.LogInformation($"{functionName} Employee not found");
                response.StatusCode = (int)ResponseStatusCode.NotFound;
                response.ErrorMessage = "Employee not found";
            }
            
            response.Data = employee;
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
