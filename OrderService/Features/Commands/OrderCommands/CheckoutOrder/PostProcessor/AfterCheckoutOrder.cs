using MediatR.Pipeline;
using Microsoft.EntityFrameworkCore;
using OrderService.Enums;
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
    private readonly ILogger<AfterCheckoutOrder> _logger;
    private readonly ISendEndpointCustomProvider _sendEndpoint;
    public AfterCheckoutOrder
    (
        IUnitOfRepository unitOfRepository,
        ILogger<AfterCheckoutOrder> logger,
        ISendEndpointCustomProvider sendEndpoint
    )
    {
        _unitOfRepository = unitOfRepository;
        _logger = logger;
        _sendEndpoint = sendEndpoint;
    }
    public async Task Process(CheckoutOrderCommand request, CheckoutOrderResponse response, CancellationToken cancellationToken)
    {
        var payload = request.Payload;
        var functionName = $"{nameof(CheckoutOrderHandler)} =>";

        try
        {
            if (response.StatusCode == (int)ResponseStatusCode.Ok)
            {
                /* 1. Send a message to check status of order after 1 minute */
                var resName = await _unitOfRepository.Restaurant
                    .Where(x => x.Id.Equals(response.PostProcessorData.RestaurantId))
                    .AsNoTracking()
                    .Select(x => x.Name)
                    .FirstOrDefaultAsync(cancellationToken);
                var order = await  _unitOfRepository.Order.GetById(payload.OrderId);
                var receivers = await _unitOfRepository.Employee
                    .Where(x => x.RestaurantId.Equals(order.RestaurantId) && x.Role == RestaurantRole.ServiceStaff)
                    .AsNoTracking()
                    .Select(x => x.Id)
                    .ToListAsync(cancellationToken);
                
                // Send to service staff
                var message = new NotifyOrder
                {
                    OrderId = payload.OrderId.ToString(),
                    OrderStatus = OrderStatus.CheckedOut,
                    RestaurantName = resName,
                    Receivers = receivers
                };
                await _sendEndpoint.SendMessage<NotifyOrder>(message, ExchangeType.Direct, cancellationToken);
            }
        }
        catch (Exception ex)
        {
            ex.LogError(functionName, _logger);
        }
    }
}