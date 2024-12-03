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

namespace OrderService.Features.Commands.OrderCommands.AcceptOrder.PostProcessor;

public class AfterAcceptOrder : IRequestPostProcessor<AcceptOrderCommand, AcceptOrderResponse>
{
    private readonly IUnitOfRepository _unitOfRepository;
    public readonly ILogger<AfterAcceptOrder> _logger;
    public readonly ISendEndpointCustomProvider _SendEndpoint;
    public AfterAcceptOrder
    (
        IUnitOfRepository unitOfRepository,
        ILogger<AfterAcceptOrder> logger,
        ISendEndpointCustomProvider sendEndpoint
    )
    {
        _unitOfRepository = unitOfRepository;
        _logger = logger;
        _SendEndpoint = sendEndpoint;
    }
    
    public async Task Process(AcceptOrderCommand request, AcceptOrderResponse response, CancellationToken cancellationToken)
    {
        var functionName = $"{nameof(AfterAcceptOrder)} OrderId = {request.OrderId} =>";
        _logger.LogInformation(functionName);

        try
        {
            if(response.StatusCode == (int)ResponseStatusCode.Ok)
            {
                /* 1. Send notification to chef */
                /* 1.1. Find all chef in restaurant */
                var chefIds = await _unitOfRepository.Employee
                    .Where(x =>
                        x.Role == RestaurantRole.Chef
                        && x.RestaurantId.Equals(response.PostProcessorData.RestaurantId))
                    .AsNoTracking()
                    .Select(x => x.Id)
                    .ToListAsync(cancellationToken);
                
                /* 1.2. Create message */
                var message = new NotifyOrder
                {
                    OrderId = request.OrderId.ToString(),
                    OrderStatus = OrderStatus.Preparing,
                    RestaurantName = response.PostProcessorData.RestaurantName,
                    Receivers = chefIds,
                    ReceiverType = ReceiverType.Chef
                };
                await _SendEndpoint.SendMessage<NotifyOrder>(message, ExchangeType.Direct, cancellationToken);

                /* 2. Send notification to customer */
                /* 2.1. Find customer */
                var customerId = await _unitOfRepository.Customer
                    .Where(x =>
                        x.Id == response.PostProcessorData.Order.CustomerId)
                    .AsNoTracking()
                    .Select(x => x.Id)
                    .ToListAsync(cancellationToken);
                
                /* 2.2. Create message */
                var customerMessage = new NotifyOrder
                {
                    OrderId = request.OrderId.ToString(),
                    OrderStatus = OrderStatus.Preparing,
                    RestaurantName = response.PostProcessorData.RestaurantName,
                    Receivers = customerId,
                    ReceiverType = ReceiverType.Customer
                };
                await _SendEndpoint.SendMessage<NotifyOrder>(message, ExchangeType.Direct, cancellationToken);
            }
        }
        catch (Exception ex)
        {
            ex.LogError($"{functionName} Has error: {ex.Message}", _logger);
        }
    }
}