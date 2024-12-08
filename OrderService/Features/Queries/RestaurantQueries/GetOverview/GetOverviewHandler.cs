using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderService.Models.Responses;
using OrderService.Repositories;
using Shared.Enums;
using Shared.Extensions;
using Shared.HttpContextAccessor;
using Shared.Responses;

namespace OrderService.Features.Queries.RestaurantQueries.GetOverview;

public class GetOverviewHandler : IRequestHandler<GetOverviewQuery, GetRestaurantOverviewResponse>
{
    private readonly IUnitOfRepository _unitOfRepository;
    private readonly ILogger<GetOverviewHandler> _logger;
    private readonly ICustomHttpContextAccessor _httpContextAccessor;
    public GetOverviewHandler
    (
        IUnitOfRepository unitOfRepository,
        ILogger<GetOverviewHandler> logger,
        ICustomHttpContextAccessor httpContextAccessor
    )
    {
        _unitOfRepository = unitOfRepository;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }
    
    public async Task<GetRestaurantOverviewResponse> Handle(GetOverviewQuery request, CancellationToken cancellationToken)
    {
        var currentDate = request.CurrentDate;
        var functionName = $"{nameof(GetOverviewHandler)} =>";
        var response = new GetRestaurantOverviewResponse {StatusCode = (int)ResponseStatusCode.InternalServerError};

        try
        {
            var currentUserId = _httpContextAccessor.GetCurrentUserId();
            _logger.LogInformation(functionName);
            var orderIds = await _unitOfRepository.Order
                .Where(x => 
                    x.RestaurantId.Equals(currentUserId) 
                    && x.Status == OrderStatus.Success 
                    && x.OrderDate.Date == currentDate.Date
                )
                .AsNoTracking()
                .Select(x => x.Id)
                .ToListAsync(cancellationToken);
            
            var amountData = await 
            (
                 from order in _unitOfRepository.Order.GetAll() 
                 join orderDetail in _unitOfRepository.OrderDetail.GetAll() 
                     on  order.Id equals orderDetail.OrderId 
                 where 
                    order.RestaurantId.Equals(currentUserId) 
                    && order.OrderDate.Month == currentDate.Month 
                    && order.Status == OrderStatus.Success 
                 select new 
                 {
                    orderDetail.Price,
                    orderDetail.Amount 
                 }
            )
            .AsNoTracking()
            .ToListAsync(cancellationToken);
            
            var monthlyRevenue = amountData.Sum(x => x.Amount * x.Price);
            
            var totalRevenue = await _unitOfRepository.OrderDetail
                .Where(x => orderIds.Contains(x.OrderId))
                .AsNoTracking()
                .SumAsync(x => x.Amount * x.Price, cancellationToken);

            response.Data = new GetRestaurantOverviewData
            {
                TotalSuccessOrder = orderIds.Count(),
                TotalRevenueInMonth = monthlyRevenue,
                TotalRevenueInDay = totalRevenue,
            };
            response.StatusCode = (int)ResponseStatusCode.Ok;
        }
        catch (Exception ex)
        {
            ex.LogError(functionName, _logger);
        }

        return response;
    }
}