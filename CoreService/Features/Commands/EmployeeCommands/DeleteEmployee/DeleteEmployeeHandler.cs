using CoreService.Models.Responses;
using CoreService.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Extensions;
using Shared.HttpContextAccessor;
using Shared.Responses;

namespace CoreService.Features.Commands.EmployeeCommands.DeleteEmployee;

public class DeleteEmployeeHandler : IRequestHandler<DeleteEmployeeCommand, DeleteEmployeeResponse>
{
    private readonly IUnitOfRepository _unitOfRepository;
    private readonly ILogger<DeleteEmployeeHandler> _logger;
    private readonly ICustomHttpContextAccessor _httpContextAccessor;
    public DeleteEmployeeHandler
    (
        IUnitOfRepository unitOfRepository,
        ILogger<DeleteEmployeeHandler> logger,
        ICustomHttpContextAccessor httpContextAccessor
    )
    {
        _unitOfRepository = unitOfRepository;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }
    public async Task<DeleteEmployeeResponse> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
    {
        var restaurantId = _httpContextAccessor.GetCurrentUserId();
        var employeeId = request.EmployeeId;
        var functionName = $"{nameof(DeleteEmployeeHandler)} RestaurantId = {restaurantId} EmployeeId = {employeeId} =>";
        var response = new DeleteEmployeeResponse(){StatusCode = (int)ResponseStatusCode.Ok};

        try
        {
            _logger.LogInformation(functionName);
            
            var employee = await
                (
                    from e in _unitOfRepository.Employee.GetAll()
                    where e.Id.ToString().Equals(employeeId)
                          && e.IsActive
                          && e.RestaurantId.ToString().Equals(restaurantId)
                    select e
                )
                .FirstOrDefaultAsync(cancellationToken);

            if (employee is null)
            {
                _logger.LogWarning($"{functionName} {employeeId} not found or no permission");
                response.ErrorMessage = "Employee not found or no permission";
                response.StatusCode = (int)ResponseStatusCode.NotFound;
                return response;
            }
            
            employee.IsActive = false;
            
            _unitOfRepository.Employee.Update(employee);
            await _unitOfRepository.CompleteAsync();
        }
        catch (Exception ex)
        {
            ex.LogError($"{functionName} Has error: {ex.Message}", _logger);
            response.StatusCode = (int)ResponseStatusCode.InternalServerError;
        }

        return response;
    }
}
