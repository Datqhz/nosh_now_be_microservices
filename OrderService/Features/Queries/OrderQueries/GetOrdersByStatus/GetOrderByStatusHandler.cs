using MediatR;
using Microsoft.EntityFrameworkCore;
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
        var functionName = $"{nameof(GetOrderByStatusHandler)} =>";
        var response = new GetOrderByStatusResponse {StatusCode = (int)ResponseStatusCode.InternalServerError};
        
        try
        {
            var currentUserId = _httpContextAccessor.GetCurrentUserId();
            _logger.LogInformation(functionName);
            var orders =  await 
                (
                    from o in _unitOfRepository.Order.GetAll()
                    join res in _unitOfRepository.Restaurant.GetAll()
                        on o.RestaurantId equals res.Id
                    where 
                        o.CustomerId == currentUserId
                        && o.Status == request.OrderStatus
                    select new GetOrderByStatusData
                    {
                        OrderId = o.Id,
                        RestaurantName = res.Name,
                        OrderDate = o.OrderDate,
                        OrderStatus = o.Status,
                    }
                )
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            var orderIds = orders.Select(x => x.OrderId).ToList();
            var totalPays = await
                (
                    from od in _unitOfRepository.OrderDetail.GetAll()
                    where orderIds.Contains(od.OrderId)
                    group od by od.OrderId
                    into grouped
                    select new
                    {
                        OrderId = grouped.Key,
                        TotalPay = grouped.Sum(x => x.Amount * x.Price)
                    }
                )
                .AsNoTracking()
                .ToListAsync(cancellationToken);
            foreach (var order in orders)
            {
                order.TotalPay = totalPays
                    .FirstOrDefault(x => x.OrderId == order.OrderId)?.TotalPay ?? 0;
            }
            response.StatusCode = (int)ResponseStatusCode.Ok;
            response.Data = orders;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"{functionName} Has error: {ex.Message}");
            // response.ErrorMessage = CoreServiceTranslation.EXH_ERR_01.ToString();
            // response.MessageCode = CoreServiceTranslation.EXH_ERR_01.ToString();
        }

        return response;
    }
}