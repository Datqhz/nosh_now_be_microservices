using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderService.Models.Responses;
using OrderService.Repositories;
using Shared.Enums;
using Shared.Extensions;
using Shared.HttpContextAccessor;
using Shared.Responses;

namespace OrderService.Features.Commands.OrderCommands.RejectOrder;

public class RejectOrderHandler : IRequestHandler<RejectOrderCommand, RejectOrderResponse>
{
    private readonly IUnitOfRepository _unitOfRepository;
    private readonly ILogger<RejectOrderHandler> _logger;
    private readonly ICustomHttpContextAccessor _httpContextAccessor;
    public RejectOrderHandler
    (
        IUnitOfRepository unitOfRepository,
        ILogger<RejectOrderHandler> logger,
        ICustomHttpContextAccessor httpContextAccessor
    )
    {
        _unitOfRepository = unitOfRepository;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }
    public async Task<RejectOrderResponse> Handle(RejectOrderCommand request, CancellationToken cancellationToken)
    {
        var functionName = $"{nameof(RejectOrderHandler)} OrderId = {request.OrderId}=>";
        var response = new RejectOrderResponse {StatusCode = (int)ResponseStatusCode.BadRequest};

        try
        {
            _logger.LogInformation(functionName);
            var currentUserId = _httpContextAccessor.GetCurrentUserId();
            var serviceStaff = await _unitOfRepository.Employee
                .Where(x => x.Id.Equals(currentUserId) && x.Role == RestaurantRole.ServiceStaff)
                .FirstOrDefaultAsync(cancellationToken);
            var order = await _unitOfRepository.Order
                .Where(x => x.Id == request.OrderId)
                .FirstOrDefaultAsync(cancellationToken);
            if (order is null)
            {
                response.StatusCode = (int)ResponseStatusCode.NotFound;
                return response;
            }

            if (order.RestaurantId != serviceStaff!.RestaurantId)
            {
                response.StatusCode = (int)ResponseStatusCode.Forbidden;
                return response;
            }

            var clock = DateTime.Now - order.OrderDate;
            if (clock < TimeSpan.FromMinutes(1))
            {
                return response;
            }

            order.Status = OrderStatus.Rejected;
            _unitOfRepository.Order.Update(order);
            await _unitOfRepository.CompleteAsync();
            
            response.StatusCode = (int)ResponseStatusCode.Ok;
        }
        catch(Exception ex)
        {
            ex.LogError(functionName, _logger);
            response.StatusCode = (int)ResponseStatusCode.InternalServerError;
        }

        return response;
    }
}