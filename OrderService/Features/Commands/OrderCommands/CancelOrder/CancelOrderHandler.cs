using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderService.Enums;
using OrderService.Models.Responses;
using OrderService.Repositories;
using Shared.Enums;
using Shared.Extensions;
using Shared.HttpContextAccessor;
using Shared.Responses;

namespace OrderService.Features.Commands.OrderCommands.CancelOrder;

public class CancelOrderHandler : IRequestHandler<CancelOrderCommand, CancelOrderResponse>
{
    private readonly IUnitOfRepository _unitOfRepository;
    private readonly ILogger<CancelOrderHandler> _logger;
    private readonly ICustomHttpContextAccessor _httpContextAccessor;
    public CancelOrderHandler
    (
        IUnitOfRepository unitOfRepository,
        ILogger<CancelOrderHandler> logger,
        ICustomHttpContextAccessor httpContextAccessor
    )
    {
        _unitOfRepository = unitOfRepository;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }
    
    public async Task<CancelOrderResponse> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
    {
        var functionName = $"{nameof(CancelOrderHandler)} =>";
        var response = new CancelOrderResponse {StatusCode = (int)ResponseStatusCode.BadRequest};

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

            if (order.CustomerId != currentUserId)
            {
                response.StatusCode = (int)ResponseStatusCode.Forbidden;
                return response;
            }

            var clock = DateTime.Now - order.OrderDate;
            if (clock > TimeSpan.FromMinutes(1))
            {
                return response;
            }
            
            order.Status = OrderStatus.Canceled;
            _unitOfRepository.Order.Update(order);
            await _unitOfRepository.CompleteAsync();
            response.StatusCode = (int)ResponseStatusCode.Ok;
        }
        catch (Exception exception)
        {
            exception.LogError(functionName, _logger);
        }

        return response;
    }
}