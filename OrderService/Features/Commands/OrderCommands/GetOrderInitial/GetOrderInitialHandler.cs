using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderService.Data.Models;
using OrderService.Models.Responses;
using OrderService.Repositories;
using Shared.Enums;
using Shared.HttpContextAccessor;
using Shared.Responses;

namespace OrderService.Features.Commands.OrderCommands.GetOrderInitial;

public class GetOrderInitialHandler : IRequestHandler<GetOrderInitialCommand, GetOrderInitialResponse>
{
    private readonly IUnitOfRepository _unitOfRepository;
    private readonly ILogger<GetOrderInitialHandler> _logger;
    private readonly ICustomHttpContextAccessor _httpContextAccessor;
    public GetOrderInitialHandler
    (
        IUnitOfRepository unitOfRepository,
        ILogger<GetOrderInitialHandler> logger,
        ICustomHttpContextAccessor httpContextAccessor
    )
    {
        _unitOfRepository = unitOfRepository;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }
    public async Task<GetOrderInitialResponse> Handle(GetOrderInitialCommand request, CancellationToken cancellationToken)
    {
        var restaurantId = request.RestaurantId;
        var functionName = $"{nameof(GetOrderInitialHandler)}=> ";
        var response = new GetOrderInitialResponse {StatusCode = (int)ResponseStatusCode.InternalServerError};

        try
        {
            var currentUserId = _httpContextAccessor.GetCurrentUserId();
            _logger.LogInformation(functionName);

            var order = await _unitOfRepository.Order
                .Where(x =>
                    x.RestaurantId.Equals(restaurantId) 
                    && x.CustomerId.Equals(currentUserId)
                    && x.Status == OrderStatus.Init)
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);

            if (order is null)
            {
                order = new Order
                {
                    RestaurantId = restaurantId,
                    CustomerId = currentUserId,
                    OrderDate = DateTime.Now
                };
                order = await _unitOfRepository.Order.Add(order);
                await _unitOfRepository.CompleteAsync();
                response.Data = new GetOrderInitialData
                {
                    OrderId = order.Id,
                };
                response.StatusCode = (int)ResponseStatusCode.Ok;
                return response;
            }
            var orderDetails = await _unitOfRepository.OrderDetail.Where(x => x.OrderId == order.Id)
                .AsNoTracking()
                .Select(x => new GetOrderInitialOrderDetailData
                {
                    Id = x.Id,
                    FoodId = x.FoodId,
                    Amount = x.Amount,
                })
                .ToListAsync(cancellationToken);
            
            response.Data = new GetOrderInitialData
            {
                OrderId = order.Id,
                OrderDetails = orderDetails
            };
            
            response.StatusCode = (int)ResponseStatusCode.Ok;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"{functionName} Has error: {ex.Message}");
        }

        return response;
    }
}