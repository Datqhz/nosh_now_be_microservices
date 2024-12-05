using System;
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
        if (!receivers.Any())
        {
            return;
        }
        var message = new OrderStatusChangeNotification();
        switch (eventData.OrderStatus)
        {
            case OrderStatus.CheckedOut:
            {
                var content = string.Format(OrderNotificationServiceStaffContent.OrderCheckedOut, eventData.OrderId);
                message.Title = OrderNotificationServiceStaffContent.OrderCheckedOut;
                message.Content = content;
                break;
            }
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
            case OrderStatus.ReadyToPickup:
            {
                if (eventData.ReceiverType == ReceiverType.Customer)
                {
                    message.Content = string.Format(OrderNotificationCustomerContent.OrderReadyToPickup, eventData.OrderId, eventData.RestaurantName);
                    message.Title = OrderNotificationCustomerTitle.OrderReadyToPickup;
                }
                
                var content = string.Format(OrderNotificationServiceStaffContent.OrderReadyToPickup, eventData.OrderId);
                message.Title = string.Format(OrderNotificationServiceStaffTitle.OrderReadyToPickup, eventData.OrderId);
                message.Content = content;
                break;
            }
            case OrderStatus.Delivering:
            {
                message.Content = string.Format(OrderNotificationCustomerContent.OrderDelivering, eventData.OrderId, eventData.RestaurantName);
                message.Title = OrderNotificationCustomerTitle.OrderDelivering;
                break;
            }
            case OrderStatus.Arrived:
            {
                if (eventData.ReceiverType == ReceiverType.Customer)
                {
                    message.Content = string.Format(OrderNotificationCustomerContent.OrderArrived, eventData.OrderId, eventData.RestaurantName);
                    message.Title = OrderNotificationCustomerTitle.OrderArrived;
                }
                
                var content = string.Format(OrderNotificationServiceStaffContent.OrderArrived, eventData.OrderId);
                message.Title = OrderNotificationServiceStaffTitle.OrderArrived;
                message.Content = content;
                break;
            }
            case OrderStatus.Success:
            {
                if (eventData.ReceiverType == ReceiverType.Customer)
                {
                    message.Content = string.Format(OrderNotificationCustomerContent.DeliverSuccess, eventData.OrderId);
                    message.Title = OrderNotificationCustomerTitle.DeliverSuccess;
                }
                
                var content = string.Format(OrderNotificationServiceStaffContent.DeliverSuccess, eventData.OrderId);
                message.Title = OrderNotificationServiceStaffTitle.DeliverSuccess;
                message.Content = content;
                break;
            }
            case OrderStatus.Failed:
            {
                var content = string.Format(OrderNotificationServiceStaffContent.OrderFailed, eventData.OrderId);
                message.Title = OrderNotificationServiceStaffTitle.OrderFailed;
                message.Content = content;
                break;
            }
            default:
            {
                break;
            }
        }
        await _hubContext.Clients.Clients(receivers).NotifyOrderStatusChange(message);
    }
    
    #endregion
}