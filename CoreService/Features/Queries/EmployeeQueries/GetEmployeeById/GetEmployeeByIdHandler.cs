using CoreService.Models.Responses;
using CoreService.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Extensions;
using Shared.HttpContextAccessor;
using Shared.Responses;

namespace CoreService.Features.Queries.EmployeeQueries.GetEmployeeById;

public class GetEmployeeByIdHandler : IRequestHandler<GetEmployeeByIdQuery, GetEmployeeByIdResponse>
{
    private readonly IUnitOfRepository _unitOfRepository;
    private readonly ILogger<GetEmployeeByIdHandler> _logger;
    private readonly ICustomHttpContextAccessor _httpContextAccessor;
    public GetEmployeeByIdHandler
    (
        IUnitOfRepository unitOfRepository,
        ILogger<GetEmployeeByIdHandler> logger,
        ICustomHttpContextAccessor httpContextAccessor
    )
    {
        _unitOfRepository = unitOfRepository;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }
    public async Task<GetEmployeeByIdResponse> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
    {
        var employeeId = request.EmployeeId;
        var currentUserId = _httpContextAccessor.GetCurrentUserId();
        var functionName = $"{nameof(GetEmployeeByIdHandler)} EmployeeId: {employeeId} =>";
        _logger.LogInformation(functionName);
        var response = new GetEmployeeByIdResponse { StatusCode = (int)ResponseStatusCode.Ok };

        try
        {
            var employee = await _unitOfRepository.Employee
                .Where(x =>x.Id.ToString().Equals(employeeId))
                .Select(x => new GetEmployeeByIdData
                {
                    Id = x.Id.ToString(),
                    DisplayName = x.DisplayName,
                    Avatar = x.Avatar,
                    Email = x.Email,
                    Role = x.Role,
                    PhoneNumber = x.PhoneNumber,
                    IsActive = x.IsActive
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
