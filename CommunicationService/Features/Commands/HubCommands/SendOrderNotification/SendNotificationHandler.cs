using System.Text.Json;
using CommunicationService.Constants;
using CommunicationService.Hubs;
using CommunicationService.Hubs.Models;
using CommunicationService.Repositories;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Shared.Enums;
using Shared.MassTransits.Contracts;

namespace CommunicationService.Features.Commands.HubCommands.SendOrderNotification;

public class SendNotificationHandler : IRequestHandler<SendNotificationCommand>
{
    private readonly IUnitOfRepository _unitOfRepository;
    private readonly IHubContext<NotificationHub, INotificationHub> _hubContext;
    private readonly ILogger<SendNotificationHandler> _logger;
    public SendNotificationHandler
    (
        IHubContext<NotificationHub, INotificationHub> hubContext,
        IUnitOfRepository unitOfRepository,
        ILogger<SendNotificationHandler> logger
    )
    {
        _hubContext = hubContext;
        _unitOfRepository = unitOfRepository;
        _logger = logger;
    }
    public async Task Handle(SendNotificationCommand request, CancellationToken cancellationToken)
    {
        var functionName = $"{nameof(SendNotificationHandler)} Payload = {JsonSerializer.Serialize(request.Event)} => ";

        try
        {
            _logger.LogInformation(functionName);
            await SendNotification(request.Event);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"{functionName} Has Error: {e.Message}");
        }
    }
    
    #region Private Methods

    private async Task SendNotification(NotifyOrder eventData)
    {
        var receivers = await _unitOfRepository.SignalRConnection
            .Where(x => eventData.Receivers.Contains(x.UserId))
            .AsNoTracking()
            .Select(x => x.ConnectionId)
            .ToListAsync();
        var message = new OrderStatusChangeNotification();
        switch (eventData.OrderStatus)
        {
            case OrderStatus.Preparing:
            {
                if (eventData.ReceiverType == ReceiverType.Customer)
                {
                    message.Content = string.Format(OrderNotificationCustomerContent.OrderPreparing, eventData.OrderId, eventData.RestaurantName);
                    message.Title = OrderNotificationCustomerTitle.OrderPreparing;
                }
                var content = string.Format(OrderNotificationChefContent.OrderPreparing, eventData.OrderId);
                message.Title = OrderNotificationChefTitle.OrderPreparing;
                message.Content = content;
                break;
            }
            case OrderStatus.Rejected:
            {
                message.Content = string.Format(OrderNotificationCustomerContent.OrderRejected, eventData.OrderId, eventData.RestaurantName);
                message.Title = OrderNotificationCustomerTitle.OrderRejected;
                break;
            }
            // case OrderStatus.ReadyToPickup:
            // {
            //     var content = OrderNotificationContent.OrderReadyToPickup
            //         .Replace("{{orderId}}", eventData.OrderId)
            //         .Replace("{{restaurant}}",eventData.RestaurantName);
            //     message.Title = OrderNotificationTitle.OrderReadyToPickup;
            //     message.Content = content;
            //     break;
            // }
            // case OrderStatus.Delivering:
            // {
            //     var content = OrderNotificationContent.OrderDelivering
            //         .Replace("{{orderId}}", eventData.OrderId)
            //         .Replace("{{restaurant}}",eventData.RestaurantName);
            //     message.Title = OrderNotificationTitle.OrderDelivering;
            //     message.Content = content;
            //     break;
            // }
            // case OrderStatus.Arrived:
            // {
            //     var content = OrderNotificationContent.OrderArrived
            //         .Replace("{{orderId}}", eventData.OrderId)
            //         .Replace("{{restaurant}}",eventData.RestaurantName);
            //     message.Title = OrderNotificationTitle.OrderArrived;
            //     message.Content = content;
            //     break;
            // }
            // case OrderStatus.Success:
            // {
            //     var content = OrderNotificationContent.DeliverSuccess
            //         .Replace("{{orderId}}", eventData.OrderId)
            //         .Replace("{{restaurant}}",eventData.RestaurantName);
            //     message.Title = OrderNotificationTitle.DeliverSuccess;
            //     message.Content = content;
            //     break;
            // }
            // case OrderStatus.Failed:
            // {
            //     var content = OrderNotificationContent.OrderFailed
            //         .Replace("{{orderId}}", eventData.OrderId);
            //     message.Title = OrderNotificationTitle.OrderFailed;
            //     message.Content = content;
            //     break;
            // }
            default:
            {
                break;
            }
        }
        await _hubContext.Clients.Clients(receivers).NotifyOrderStatusChange(message);
    }
    
    #endregion
}