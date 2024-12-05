using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderService.Models.Responses;
using OrderService.Repositories;
using Shared.Enums;
using Shared.HttpContextAccessor;
using Shared.Responses;

namespace OrderService.Features.Queries.OrderQueries.GetOrderById;

public class GetOrderByIdHandler : IRequestHandler<GetOrderByIdQuery, GetOrderByIdResponse>
{
    private readonly IUnitOfRepository _unitOfRepository;
    private readonly ILogger<GetOrderByIdHandler> _logger;
    private readonly ICustomHttpContextAccessor _httpContextAccessor;
    public GetOrderByIdHandler
    (
        IUnitOfRepository unitOfRepository,
        ILogger<GetOrderByIdHandler> logger,
        ICustomHttpContextAccessor httpContextAccessor
    )
    {
        _unitOfRepository = unitOfRepository;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }
    
    public async Task<GetOrderByIdResponse> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var orderId = request.OrderId;
        var functionName = $"{nameof(GetOrderByIdHandler)} OrderId = {orderId} =>";
        var response = new GetOrderByIdResponse {StatusCode = (int)ResponseStatusCode.InternalServerError};
        
        try
        {
            _logger.LogInformation(functionName);
            var order = await
                (
                    from o in _unitOfRepository.Order
                        .Where( o => o.Id == orderId
                               && o.Status != OrderStatus.Init)
                    join res in _unitOfRepository.Restaurant.GetAll()
                        on o.RestaurantId equals res.Id
                    join cus in _unitOfRepository.Customer.GetAll()
                        on o.CustomerId equals cus.Id
                    join s in _unitOfRepository.Shipper.GetAll()
                        on o.ShipperId equals s.Id into oi
                    from i in oi.DefaultIfEmpty()
                    select new GetOrderByIdData
                    {
                        OrderId = o.Id,
                        RestaurantName = res.Name,
                        RestaurantCoordinate = res.Coordinate,
                        ShippingFee = o.ShippingFee,
                        OrderDate = o.OrderDate,
                        DeliveryInfo = o.DeliveryInfo,
                        ShipperImage = i.Avatar,
                        ShipperName = i.Name,
                        CustomerName = cus.Name,
                        OrderStatus = o.Status,
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
                    select new OrderDetailData
                    {
                        OrderDetailId = od.Id,
                        FoodName = food.Name,
                        FoodPrice = food.Price,
                        FoodImage = food.Image,
                        Amount = od.Amount,
                        Status = (int)od.Status
                    }
                )
                .AsNoTracking()
                .ToListAsync(cancellationToken);
            
            var totalPay = orderDetails.Sum(x => x.Amount * x.FoodPrice);
            order.OrderDetails = orderDetails;
            order.Substantial = totalPay;
            order.Total = order.Substantial + order.ShippingFee;
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