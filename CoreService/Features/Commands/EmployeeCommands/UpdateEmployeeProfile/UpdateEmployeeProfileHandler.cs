using System.Text.Json;
using CoreService.Models.Responses;
using CoreService.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Enums;
using Shared.HttpContextAccessor;
using Shared.Responses;

namespace CoreService.Features.Commands.EmployeeCommands.UpdateEmployeeProfile;

public class UpdateEmployeeProfileHandler : IRequestHandler<UpdateEmployeeProfileCommand, UpdateEmployeeProfileResponse>
{
    private readonly IUnitOfRepository _unitOfRepository;
    private readonly ILogger<UpdateEmployeeProfileHandler> _logger;
    private readonly ICustomHttpContextAccessor _httpContextAccessor;
    public UpdateEmployeeProfileHandler
    (
        IUnitOfRepository unitOfRepository,
        ILogger<UpdateEmployeeProfileHandler> logger,
        ICustomHttpContextAccessor httpContextAccessor
    )
    {
        _unitOfRepository = unitOfRepository;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }
    public async Task<UpdateEmployeeProfileResponse> Handle(UpdateEmployeeProfileCommand request, CancellationToken cancellationToken)
    {
        var payload = request.Payload;
        var functionName = $"{nameof(UpdateEmployeeProfileHandler)} Payload = {JsonSerializer.Serialize(payload)} => ";
        var response = new UpdateEmployeeProfileResponse {StatusCode = (int)ResponseStatusCode.NotFound};
        
        try
        {
            var currentUserId = _httpContextAccessor.GetCurrentUserId();
            _logger.LogInformation(functionName);
            var employee = await
                (
                    from e in _unitOfRepository.Employee.GetAll()
                    where e.Id.ToString().Equals(payload.EmployeeId)
                          && e.IsActive
                    select e
                )
                .FirstOrDefaultAsync(cancellationToken);

            if (employee is null)
            {
                _logger.LogWarning($"{functionName} {payload.EmployeeId} not found");
                response.ErrorMessage = "Employee not found";
                return response;
            }

            var havePermission = employee.Id.ToString().Equals(payload.EmployeeId) ||
                                 employee.RestaurantId.ToString().Equals(payload.EmployeeId);
            if (!havePermission)
            {
                _logger.LogWarning($"{functionName} {payload.EmployeeId} No permission");
                response.ErrorMessage = "No permission";
                return response;
            }
            
            employee.Avatar = payload.Avatar;
            employee.PhoneNumber = payload.PhoneNumber;
            employee.DisplayName = payload.DisplayName;
            
            _unitOfRepository.Employee.Update(employee);
            await _unitOfRepository.CompleteAsync();
            
            response.StatusCode = (int)ResponseStatusCode.Ok;
            response.PostProcessorData = employee.Role == RestaurantRole.Chef? SystemRole.Chef : SystemRole.ServiceStaff;
            
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