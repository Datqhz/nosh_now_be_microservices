using System.Text.Json;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderService.Enums;
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
        var functionName = $"{nameof(CheckoutOrderHandler)} Payload : {JsonSerializer.Serialize(payload)} =>";
        var response = new CheckoutOrderResponse {StatusCode = (int)ResponseStatusCode.BadRequest};
        await using var transaction = await _unitOfRepository.OpenTransactionAsync();
        
        try
        {
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
            
            var orderDetails = await _unitOfRepository.OrderDetail
                .Where(x => x.OrderId == payload.OrderId)
                .ToListAsync(cancellationToken);
            var foodIds = payload.OrderDetails.Select(x => x.FoodId).ToList();
            
            foreach (var record in payload.OrderDetails)
            {
                var orderDetail = orderDetails.FirstOrDefault(x => x.FoodId == record.FoodId);
                if (orderDetail is null)
                {
                    continue;
                }
                
                var foods = _unitOfRepository.Food.Where(x => foodIds.Contains(x.Id));
                if (record.Option == ModifyOption.Update)
                {
                    var food = foods.FirstOrDefault(x => x.Id == record.FoodId);
                    orderDetail.Amount = record.Amount;
                    orderDetail.Price = food.Price;
                    _unitOfRepository.OrderDetail.Update(orderDetail);
                    continue;
                }

                if (record.Option == ModifyOption.Delete)
                {
                    _unitOfRepository.OrderDetail.Delete(orderDetail);
                }
            }
            
            order.DeliveryInfo = payload.DeliveryInfo;
            order.ShippingFee = payload.ShippingFee;
            order.PaymentMethodId = new Guid(payload.PaymentMethod);
            order.Status = OrderStatus.CheckedOut;
            order.OrderDate = DateTime.Now;
            _unitOfRepository.Order.Update(order);
            await _unitOfRepository.CompleteAsync();
            await _unitOfRepository.CommitAsync();
            
            // if (orderDetail is null)
            // {
            //     response.StatusCode = (int)ResponseStatusCode.NotFound;
            //     return response;
            // }
            // _unitOfRepository.OrderDetail.Update(orderDetail);
            response.PostProcessorData = new CheckoutOrderPostProcessorData
            {
                Payload = payload,
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