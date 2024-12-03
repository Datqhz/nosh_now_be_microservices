using MediatR.Pipeline;
using Microsoft.EntityFrameworkCore;
using OrderService.Enums;
using OrderService.Models.Requests;
using OrderService.Models.Responses;
using OrderService.Repositories;
using Shared.Enums;
using Shared.Extensions;
using Shared.MassTransits.Contracts;
using Shared.MassTransits.Core;
using Shared.MassTransits.Enums;
using Shared.Responses;

namespace OrderService.Features.Commands.OrderDetailCommands.UpdatePrepareStatus.PostProcessor;

public class AfterUpdatePrepareStatus : IRequestPostProcessor<UpdatePrepareStatusCommand, UpdatePrepareStatusResponse>
{
    private readonly IUnitOfRepository _unitOfRepository;
    private readonly ILogger<AfterUpdatePrepareStatus> _logger;
    private readonly ISendEndpointCustomProvider _sendEndpoint;
    public AfterUpdatePrepareStatus
    (
        IUnitOfRepository unitOfRepository,
        ILogger<AfterUpdatePrepareStatus> logger,
        ISendEndpointCustomProvider sendEndpoint
    )
    {
        _unitOfRepository = unitOfRepository;
        _logger = logger;
        _sendEndpoint = sendEndpoint;
    }
    public async Task Process(UpdatePrepareStatusCommand request, UpdatePrepareStatusResponse response, CancellationToken cancellationToken)
    {
        var functionName = $"{nameof(UpdatePrepareStatusHandler)} OrderId = {request.Payload.OrderId}";

        try
        {
            _logger.LogInformation(functionName);
            if (response.StatusCode == (int)ResponseStatusCode.Ok)
            {
                var orderDetails = await _unitOfRepository.OrderDetail
                    .Where(x => x.OrderId == request.Payload.OrderId && x.Status == PrepareStatus.Done)
                    .AsNoTracking()
                    .ToListAsync(cancellationToken);
                if (!orderDetails.Any())
                {
                    var order = await _unitOfRepository.Order.GetById(request.Payload.OrderId);
                    order.Status = OrderStatus.ReadyToPickup;
                    _unitOfRepository.Order.Update(order);
                    await _unitOfRepository.CompleteAsync();
                    
                    var restaurant = await _unitOfRepository.Restaurant.GetById(order.RestaurantId);
                    
                    /* Todo: Send message to notify for service staff */
                    var serviceStaffIds = await _unitOfRepository.Employee
                        .Where(x =>
                            x.Role == RestaurantRole.ServiceStaff
                            && x.RestaurantId.Equals(order.RestaurantId))
                        .AsNoTracking()
                        .Select(x => x.Id)
                        .ToListAsync(cancellationToken);
                
                    /* 1.2. Create message */
                    var message = new NotifyOrder
                    {
                        OrderId = order.Id.ToString(),
                        OrderStatus = OrderStatus.ReadyToPickup,
                        RestaurantName = "",
                        Receivers = serviceStaffIds
                    };
                    await _sendEndpoint.SendMessage<NotifyOrder>(message, ExchangeType.Direct, cancellationToken);
                    /* Todo: Send message to notify for shipper */
                    
                    
                    /* Todo: Send message to notify for customer */
                    var customerId = await _unitOfRepository.Customer
                        .Where(x =>
                            x.Id == order.CustomerId)
                        .AsNoTracking()
                        .Select(x => x.Id)
                        .ToListAsync(cancellationToken);

                    var customerMessage = new NotifyOrder
                    {
                        OrderId = order.Id.ToString(),
                        OrderStatus = OrderStatus.ReadyToPickup,
                        RestaurantName = restaurant.Name,
                        Receivers = customerId
                    };
                    await _sendEndpoint.SendMessage<NotifyOrder>(message, ExchangeType.Direct, cancellationToken);
                }
            }
        }
        catch (Exception ex)
        {
            ex.LogError(functionName, _logger);
        }
    }
}