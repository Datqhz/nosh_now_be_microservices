using MassTransit.Scheduling;
using MediatR;
using MediatR.Pipeline;
using Microsoft.EntityFrameworkCore;
using OrderService.Models.Responses;
using OrderService.Repositories;
using Shared.Enums;
using Shared.Extensions;
using Shared.MassTransits.Contracts;
using Shared.MassTransits.Core;
using Shared.MassTransits.Enums;
using Shared.Responses;

namespace OrderService.Features.Commands.OrderCommands.CheckoutOrder.PostProcessor;

public class AfterCheckoutOrder : IRequestPostProcessor<CheckoutOrderCommand, CheckoutOrderResponse>
{
    private readonly IUnitOfRepository _unitOfRepository;
    public readonly ILogger<AfterCheckoutOrder> _logger;
    public readonly ISendEndpointCustomProvider _SendEndpoint;
    public AfterCheckoutOrder
    (
        IUnitOfRepository unitOfRepository,
        ILogger<AfterCheckoutOrder> logger,
        ISendEndpointCustomProvider sendEndpoint
    )
    {
        _unitOfRepository = unitOfRepository;
        _logger = logger;
        _SendEndpoint = sendEndpoint;
    }
    
    public async Task Process(CheckoutOrderCommand request, CheckoutOrderResponse response, CancellationToken cancellationToken)
    {
        var functionName = $"{nameof(AfterCheckoutOrder)} OrderId = {request.Payload.OrderId} =>";
        _logger.LogInformation(functionName);

        try
        {
            if(response.StatusCode == (int)ResponseStatusCode.Ok)
            {
                /* 1. Send notification to chef */
                /* 1.1. Find all chef in restaurant */
                var serviceStaffIds = await _unitOfRepository.Employee
                    .Where(x =>
                        x.Role == RestaurantRole.ServiceStaff
                        && x.RestaurantId.Equals(response.PostProcessorData.RestaurantId))
                    .AsNoTracking()
                    .Select(x => x.Id)
                    .ToListAsync(cancellationToken);
                
                /* 1.2. Create message */
                var restaurant = await _unitOfRepository.Restaurant.GetById(response.PostProcessorData.RestaurantId);
                var restaurantName = "Default";
                if (restaurant is not null)
                {
                    restaurantName = restaurant.Name;
                }
                var message = new NotifyOrder
                {
                    OrderId = request.Payload.OrderId.ToString(),
                    OrderStatus = OrderStatus.CheckedOut,
                    RestaurantName = restaurantName,
                    Receivers = serviceStaffIds,
                    ReceiverType = ReceiverType.ServiceStaff
                };
                await _SendEndpoint.SendMessage<NotifyOrderSchedule>(new NotifyOrderSchedule
                {
                    DeliveryTime = TimeSpan.FromMinutes(1),
                    Notification = message
                }, ExchangeType.Direct, cancellationToken);
            }
        }
        catch (Exception ex)
        {
            ex.LogError($"{functionName} Has error: {ex.Message}", _logger);
        }
    }
}