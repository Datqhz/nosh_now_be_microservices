using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderService.Models.Responses;
using OrderService.Repositories;
using Shared.Enums;
using Shared.HttpContextAccessor;
using Shared.Responses;

namespace OrderService.Features.Queries.OrderQueries.PrepareOrder;

public class PrepareOrderHandler : IRequestHandler<PrepareOrderQuery, PrepareOrderResponse>
{
    private readonly IUnitOfRepository _unitOfRepository;
    private readonly ILogger<PrepareOrderHandler> _logger;
    private readonly ICustomHttpContextAccessor _httpContextAccessor;
    public PrepareOrderHandler
    (
        IUnitOfRepository unitOfRepository,
        ILogger<PrepareOrderHandler> logger,
        ICustomHttpContextAccessor httpContextAccessor
    )
    {
        _unitOfRepository = unitOfRepository;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }
    
    public async Task<PrepareOrderResponse> Handle(PrepareOrderQuery request, CancellationToken cancellationToken)
    {
        var orderId = request.OrderId;
        var functionName = $"{nameof(PrepareOrderHandler)} OrderId = {orderId} =>";
        var response = new PrepareOrderResponse {StatusCode = (int)ResponseStatusCode.InternalServerError};
        
        try
        {
            var currentUserId = _httpContextAccessor.GetCurrentUserId();
            _logger.LogInformation(functionName);
            var order = await
                (
                    from o in _unitOfRepository.Order.GetAll()
                    join res in _unitOfRepository.Restaurant.GetAll()
                        on o.RestaurantId equals res.Id
                    where
                        o.Id == orderId
                        && o.Status == OrderStatus.Init
                    select new PrepareOrderData
                    {
                        OrderId = o.Id,
                        RestaurantName = res.Name,
                        RestaurantCoordinate = res.Coordinate
                    }
                )
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);

            if (order is null)
            {
                _logger.LogWarning($"{functionName} Order could be found");
            }
            var orderDetails = await 
                (
                    from od in _unitOfRepository.OrderDetail.GetAll()
                    join food in _unitOfRepository.Food.GetAll()
                        on od.FoodId equals food.Id
                    where od.OrderId == orderId
                    select new PrepareOrderOrderDetailData
                    {
                        OrderDetailId = od.Id,
                        FoodName = food.Name,
                        FoodPrice = food.Price,
                        FoodImage = food.Image,
                        Amount = od.Amount
                    }
                )
                .AsNoTracking()
                .ToListAsync(cancellationToken);
            
            var totalPay = orderDetails.Sum(x => x.Amount * x.FoodPrice);
            order.OrderDetails = orderDetails;
            order.Substantial = totalPay;
            response.StatusCode = (int)ResponseStatusCode.Ok;
            response.Data = order;
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