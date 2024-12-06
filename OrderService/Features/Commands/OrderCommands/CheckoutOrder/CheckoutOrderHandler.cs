using System.Text.Json;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderService.Data.Models;
using OrderService.Models.Responses;
using OrderService.Repositories;
using Shared.Enums;
using Shared.Extensions;
using Shared.HttpContextAccessor;
using Shared.Responses;

namespace OrderService.Features.Commands.OrderCommands.CheckoutOrder;

public class CheckoutOrderHandler : IRequestHandler<CheckoutOrderCommand, CheckoutOrderResponse>
{
    private readonly IUnitOfRepository _unitOfRepository;
    private readonly ILogger<CheckoutOrderHandler> _logger;
    private readonly ICustomHttpContextAccessor _httpContextAccessor;
    public CheckoutOrderHandler
    (
        IUnitOfRepository unitOfRepository,
        ILogger<CheckoutOrderHandler> logger,
        ICustomHttpContextAccessor httpContextAccessor
    )
    {
        _unitOfRepository = unitOfRepository;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }
    public async Task<CheckoutOrderResponse> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
    {
        var payload = request.Payload;
        var functionName = $"{nameof(CheckoutOrderHandler)} OrderId : {JsonSerializer.Serialize(payload)} =>";
        var response = new CheckoutOrderResponse {StatusCode = (int)ResponseStatusCode.BadRequest};
        await using var transaction = await _unitOfRepository.OpenTransactionAsync();
        
        try
        {
            _logger.LogInformation(functionName);
            var currentUserId = _httpContextAccessor.GetCurrentUserId();
            var order = await _unitOfRepository.Order
                .Where(x => x.Id == payload.OrderId)
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);
            if (order is null)
            {
                response.StatusCode = (int)ResponseStatusCode.NotFound;
                return response;
            }

            if (order.CustomerId != currentUserId)
            {
                response.StatusCode = (int)ResponseStatusCode.Forbidden;
                return response;
            }

            var orderDetails = await
                (
                    from od in _unitOfRepository.OrderDetail.GetAll()
                    join f in _unitOfRepository.Food.GetAll()
                        on od.FoodId equals f.Id
                    where od.OrderId == order.Id
                    select new OrderDetail
                    {
                        OrderId = order.Id,
                        FoodId = od.FoodId,
                        Amount = od.Amount,
                        Status = od.Status,
                        Id = od.Id,
                        Price = f.Price,
                    }
                )
                .ToListAsync(cancellationToken);
            _unitOfRepository.OrderDetail.UpdateRange(orderDetails);
            
            order.DeliveryInfo = payload.DeliveryInfo;
            order.ShippingFee = payload.ShippingFee;
            order.PaymentMethodId = new Guid(payload.PaymentMethod);
            order.Status = OrderStatus.CheckedOut;
            order.OrderDate = DateTime.Now;
            _unitOfRepository.Order.Update(order);
            await _unitOfRepository.CompleteAsync();
            await _unitOfRepository.CommitAsync();
            
            response.PostProcessorData = new CheckoutOrderPostProcessorData
            {
                Total = orderDetails.Sum(x => x.Amount * x.Price),
                RestaurantId = order.RestaurantId,
            };
            response.StatusCode = (int)ResponseStatusCode.Ok;
        }
        catch (Exception exception)
        {
            exception.LogError(functionName, _logger);
            response.StatusCode = (int)ResponseStatusCode.InternalServerError;
            await _unitOfRepository.RollbackAsync();
        }

        return response;
    }
}