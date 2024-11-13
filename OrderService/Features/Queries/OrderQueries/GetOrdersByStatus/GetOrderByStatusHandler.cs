using MediatR;
using OrderService.Data.Models;
using OrderService.Models.Responses;
using OrderService.Repositories;
using Shared.HttpContextAccessor;
using Shared.Responses;

namespace OrderService.Features.Queries.OrderQueries.GetOrdersByStatus;

public class GetOrderByStatusHandler : IRequestHandler<GetOrderByStatusQuery, GetOrderByStatusResponse>
{
    private readonly IUnitOfRepository _unitOfRepository;
    private readonly ILogger<GetOrderByStatusHandler> _logger;
    private readonly ICustomHttpContextAccessor _httpContextAccessor;
    public GetOrderByStatusHandler
    (
        IUnitOfRepository unitOfRepository,
        ILogger<GetOrderByStatusHandler> logger,
        ICustomHttpContextAccessor httpContextAccessor
    )
    {
        _unitOfRepository = unitOfRepository;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }
    
    public async Task<GetOrderByStatusResponse> Handle(GetOrderByStatusQuery request, CancellationToken cancellationToken)
    {
        var functionName = $"{nameof(GetOrderByStatusHandler)} => ";
        var response = new GetOrderByStatusResponse {StatusCode = (int)ResponseStatusCode.InternalServerError};
        
        // try
        // {
        //     var currentUserId = _httpContextAccessor.GetCurrentUserId();
        //     _logger.LogInformation(functionName);
        //     var orders = 
        //         (
        //             from o in _unitOfRepository.Order.GetAll()
        //             join 
        //             where cus.AccountId == currentUserId
        //                   && cus.IsActive
        //             select new GetCustomerProfileData
        //             {
        //                 Id = cus.Id.ToString(),
        //                 DisplayName = cus.DisplayName,
        //                 Avatar = cus.Avatar,
        //                 Email = cus.Email,
        //                 Phone = cus.PhoneNumber,
        //             }
        //         )
        //         .FirstOrDefaultAsync(cancellationToken);
        //
        //     if (customer == null)
        //     {
        //         response.ErrorMessage = CoreServiceTranslation.CUS_ERR_01.ToString();
        //         response.MessageCode = CoreServiceTranslation.CUS_ERR_01.ToString();
        //         return response;
        //     }
        //     
        //     response.StatusCode = (int)ResponseStatusCode.Ok;
        //     response.Data = customer;
        // }
        // catch (Exception ex)
        // {
        //     _logger.LogError(ex, $"{functionName} Has error: {ex.Message}");
        //     response.ErrorMessage = CoreServiceTranslation.EXH_ERR_01.ToString();
        //     response.MessageCode = CoreServiceTranslation.EXH_ERR_01.ToString();
        // }

        return response;
    }
}