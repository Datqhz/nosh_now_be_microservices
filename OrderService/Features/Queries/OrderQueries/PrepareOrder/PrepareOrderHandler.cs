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
        var response = new PrepareOrderResponse {StatusCode = (int)ResponseStatusCode.BadRequest};
        
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
                        && o.CustomerId == currentUserId
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
                response.ErrorMessage = "Order not found";
                return response;
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
                        Amount = od.Amount,
                        FoodId = food.Id
                    }
                )
                .AsNoTracking()
                .ToListAsync(cancellationToken);
            
            var foodIds = orderDetails.Select(x => x.FoodId).ToList();
            var foodAmounts = await
                (
                    from ri in _unitOfRepository.RequiredIngredient
                        .Where(x => foodIds.Contains(x.FoodId))
                    join i in _unitOfRepository.Ingredient.GetAll()
                        on ri.IngredientId equals i.Id
                    let quantity = i.Quantity / ri.Quantity
                    select new
                    {
                        ri.FoodId,
                        quantity
                    }
                )
                .AsNoTracking()
                .ToListAsync(cancellationToken);
            
            var foodsAvailable = foodAmounts.GroupBy(x => x.FoodId)
                .Select(x => new
                {
                    FoodId = x.Key,
                    Quantity = x.Min(x => x.quantity)
                })
                .ToList();
            foreach (var orderDetail in orderDetails)
            {
                var foodAvailable = foodsAvailable
                    .FirstOrDefault(x => 
                        x.FoodId == orderDetail.FoodId 
                        && Math.Floor(x.Quantity) >= orderDetail.Amount
                    );
                if (foodAvailable is null)
                {
                    _logger.LogWarning($"{functionName} Order can't serve");
                    response.ErrorMessage = "Ingredient isn't sufficient to serve this order";
                    return response;
                }
            }
            var totalPay = orderDetails.Sum(x => x.Amount * x.FoodPrice);
            order.OrderDetails = orderDetails;
            order.Substantial = totalPay;
            response.StatusCode = (int)ResponseStatusCode.Ok;
            response.Data = order;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"{functionName} Has error: {ex.Message}");
            response.ErrorMessage = "Internal Server Error";
            response.StatusCode = (int)ResponseStatusCode.InternalServerError;
            // response.ErrorMessage = CoreServiceTranslation.EXH_ERR_01.ToString();
            // response.MessageCode = CoreServiceTranslation.EXH_ERR_01.ToString();
        }

        return response;
    }
}