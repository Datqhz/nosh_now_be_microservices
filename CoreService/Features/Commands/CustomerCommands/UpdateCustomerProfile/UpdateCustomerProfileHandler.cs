using System.Text.Json;
using CoreService.Models.Responses;
using CoreService.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.HttpContextAccessor;
using Shared.Responses;

namespace CoreService.Features.Commands.CustomerCommands.UpdateCustomerProfile;

public class UpdateCustomerProfileHandler : IRequestHandler<UpdateCustomerProfileCommand, UpdateCustomerProfileResponse>
{
    private readonly IUnitOfRepository _unitOfRepository;
    private readonly ILogger<UpdateCustomerProfileHandler> _logger;
    private readonly ICustomHttpContextAccessor _httpContextAccessor;
    public UpdateCustomerProfileHandler
    (
        IUnitOfRepository unitOfRepository,
        ILogger<UpdateCustomerProfileHandler> logger,
        ICustomHttpContextAccessor httpContextAccessor
    )
    {
        _unitOfRepository = unitOfRepository;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }
    public async Task<UpdateCustomerProfileResponse> Handle(UpdateCustomerProfileCommand request, CancellationToken cancellationToken)
    {
        var payload = request.Payload;
        var functionName = $"{nameof(UpdateCustomerProfileHandler)} Payload = {JsonSerializer.Serialize(payload)} => ";
        var response = new UpdateCustomerProfileResponse {StatusCode = (int)ResponseStatusCode.BadRequest};
        
        try
        {
            var currentUserId = _httpContextAccessor.GetCurrentUserId();
            _logger.LogInformation(functionName);
            var customer = await
                (
                    from cus in _unitOfRepository.Customer.GetAll()
                    where cus.AccountId == currentUserId
                          && cus.IsActive
                    select cus
                )
                .FirstOrDefaultAsync(cancellationToken);

            if (customer == null)
            {
                response.ErrorMessage = CoreServiceTranslation.CUS_ERR_01.ToString();
                response.MessageCode = CoreServiceTranslation.CUS_ERR_01.ToString();
                return response;
            }
            
            customer.Avatar = payload.Avatar;
            customer.PhoneNumber = payload.PhoneNumber;
            customer.DisplayName = payload.DisplayName;
            
            _unitOfRepository.Customer.Update(customer);
            await _unitOfRepository.CompleteAsync();
            
            response.StatusCode = (int)ResponseStatusCode.Ok;
            response.PostProcessorData = new UpdateCustomerProfilePostProcessorData
            {
                Avatar = payload.Avatar,
                DisplayName = payload.DisplayName,
                CustomerId = customer.Id.ToString(),
            };
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