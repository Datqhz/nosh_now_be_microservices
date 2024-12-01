using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderService.Models.Responses;
using OrderService.Repositories;
using Shared.Enums;
using Shared.Extensions;
using Shared.HttpContextAccessor;
using Shared.Responses;

namespace OrderService.Features.Queries.OrderQueries.GetOrderByStatusEmployee;

public class GetOrderByStatusEmployeeHandler : IRequestHandler<GetOrderByStatusEmployeeQuery, GetOrderByStatusEmployeeResponse>
{
    private readonly IUnitOfRepository _unitOfRepository;
    private readonly ILogger<GetOrderByStatusEmployeeHandler> _logger;
    private readonly ICustomHttpContextAccessor _httpContextAccessor;
    public GetOrderByStatusEmployeeHandler
    (
        IUnitOfRepository unitOfRepository,
        ILogger<GetOrderByStatusEmployeeHandler> logger,
        ICustomHttpContextAccessor httpContextAccessor
    )
    {
        _unitOfRepository = unitOfRepository;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<GetOrderByStatusEmployeeResponse> Handle(GetOrderByStatusEmployeeQuery request, CancellationToken cancellationToken)
    {
        var functionName = $"{nameof(GetOrderByStatusEmployeeHandler)} =>";
        var response = new GetOrderByStatusEmployeeResponse {StatusCode = (int)ResponseStatusCode.InternalServerError};

        try
        {
            var currentUserId = _httpContextAccessor.GetCurrentUserId();
            _logger.LogInformation(functionName);

            var employee = await _unitOfRepository.Employee
                .Where(x => x.Id == currentUserId)
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);
            
            var compareId = ""; // If current user is restaurant
            if (employee is null)
            {
                _logger.LogInformation(functionName + $"{currentUserId} is not found");
                compareId = currentUserId;
            }
            else
            {
                compareId = employee.RestaurantId;
            }
            
            var orders = await
                (
                    from o in _unitOfRepository.Order.GetAll()
                    join res in _unitOfRepository.Restaurant.GetAll()
                        on o.RestaurantId equals res.Id
                    where
                        o.RestaurantId == compareId
                        && o.Status == request.OrderStatus
                        && o.Status != OrderStatus.Init
                        && (o.Status != OrderStatus.CheckedOut
                            || DateTime.Now - o.OrderDate > TimeSpan.FromMinutes(1))
                    select new GetOrderByStatusEmployeeData
                    {
                        OrderId = o.Id,
                        CustomerName = res.Name,
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
            ex.LogError(functionName, _logger);
        }

        return response;
    }
}
