using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderService.Models.Responses;
using OrderService.Repositories;
using Shared.Enums;
using Shared.Extensions;
using Shared.HttpContextAccessor;
using Shared.Responses;

namespace OrderService.Features.Commands.OrderCommands.AcceptOrder;

public class AcceptOrderHandler : IRequestHandler<AcceptOrderCommand, AcceptOrderResponse>
{
    private readonly IUnitOfRepository _unitOfRepository;
    private readonly ILogger<AcceptOrderHandler> _logger;
    private readonly ICustomHttpContextAccessor _httpContextAccessor;
    public AcceptOrderHandler
    (
        IUnitOfRepository unitOfRepository,
        ILogger<AcceptOrderHandler> logger,
        ICustomHttpContextAccessor httpContextAccessor
    )
    {
        _unitOfRepository = unitOfRepository;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }
    
    public async Task<AcceptOrderResponse> Handle(AcceptOrderCommand request, CancellationToken cancellationToken)
    {
        var functionName = $"{nameof(AcceptOrderHandler)} =>";
        var response = new AcceptOrderResponse {StatusCode = (int)ResponseStatusCode.BadRequest};

        try
        {
            _logger.LogInformation(functionName);
            var currentUserId = _httpContextAccessor.GetCurrentUserId();
            var order = await _unitOfRepository.Order
                .Where(x => x.Id == request.OrderId)
                .FirstOrDefaultAsync(cancellationToken);
            if (order is null)
            {
                response.StatusCode = (int)ResponseStatusCode.NotFound;
                return response;
            }
            
            var employee = await _unitOfRepository.Employee
                .Where(x => x.Id.Equals(currentUserId))
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);

            if (employee is null)
            {
                response.StatusCode = (int)ResponseStatusCode.NotFound;
                return response;
            }
            
            if (!employee.RestaurantId.Equals(order.RestaurantId))
            {
                response.StatusCode = (int)ResponseStatusCode.Forbidden;
                return response;
            }

            var clock = DateTime.Now - order.OrderDate;
            if (clock < TimeSpan.FromMinutes(1))
            {
                return response;
            }
            
            order.Status = OrderStatus.Preparing;
            _unitOfRepository.Order.Update(order);
            await _unitOfRepository.CompleteAsync();
            
            var restaurant = await _unitOfRepository.Restaurant
                .Where(x => x.Id.Equals(order.RestaurantId) && x.IsActive)
                .AsNoTracking()
                .Select(x => new
                {
                    x.Id,
                    x.Name
                })
                .FirstOrDefaultAsync(cancellationToken);
            
            response.StatusCode = (int)ResponseStatusCode.Ok;
            response.PostProcessorData = new AcceptOrderPostProcessorData
            {
                Order = order,
                RestaurantName = restaurant.Name,
                RestaurantId = restaurant.Id,
            };
        }
        catch (Exception exception)
        {
            exception.LogError(functionName, _logger);
        }

        return response;
    }
}