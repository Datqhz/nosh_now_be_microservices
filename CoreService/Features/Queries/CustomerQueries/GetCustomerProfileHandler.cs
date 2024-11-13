using CoreService.Models.Responses;
using CoreService.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.HttpContextAccessor;
using Shared.Responses;

namespace CoreService.Features.Queries.CustomerQueries;

public class GetCustomerProfileHandler : IRequestHandler<GetCustomerProfileQuery, GetCustomerProfileResponse>
{
    private readonly IUnitOfRepository _unitOfRepository;
    private readonly ILogger<GetCustomerProfileHandler> _logger;
    private readonly ICustomHttpContextAccessor _httpContextAccessor;
    public GetCustomerProfileHandler
    (
        IUnitOfRepository unitOfRepository,
        ILogger<GetCustomerProfileHandler> logger,
        ICustomHttpContextAccessor httpContextAccessor
    )
    {
        _unitOfRepository = unitOfRepository;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }
    
    public async Task<GetCustomerProfileResponse> Handle(GetCustomerProfileQuery request, CancellationToken cancellationToken)
    {
        var functionName = $"{nameof(GetCustomerProfileHandler)} => ";
        var response = new GetCustomerProfileResponse {StatusCode = (int)ResponseStatusCode.InternalServerError};
        
        try
        {
            var currentUserId = _httpContextAccessor.GetCurrentUserId();
            _logger.LogInformation(functionName);
            var customer = await
                (
                    from cus in _unitOfRepository.Customer.GetAll()
                    where cus.AccountId == currentUserId
                          && cus.IsActive
                    select new GetCustomerProfileData
                    {
                        Id = cus.Id.ToString(),
                        DisplayName = cus.DisplayName,
                        Avatar = cus.Avatar,
                        Email = cus.Email,
                        Phone = cus.PhoneNumber,
                    }
                )
                .FirstOrDefaultAsync(cancellationToken);

            if (customer == null)
            {
                response.ErrorMessage = CoreServiceTranslation.CUS_ERR_01.ToString();
                response.MessageCode = CoreServiceTranslation.CUS_ERR_01.ToString();
                return response;
            }
            
            response.StatusCode = (int)ResponseStatusCode.Ok;
            response.Data = customer;
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