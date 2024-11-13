using System.Text.Json;
using CommunicationService.Constants;
using CommunicationService.Hubs;
using CommunicationService.Hubs.Models;
using CommunicationService.Models.Requests;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Shared.Enums;

namespace CommunicationService.Features.Commands.NotifyCommands.NotifyOrderStatusChange;

public class NotifyOrderStatusChangeHandler : IRequestHandler<NotifyOrderStatusChangeCommand>
{
    private readonly IHubContext<NotificationHub, INotificationHub> _hubContext;
    private readonly ILogger<NotifyOrderStatusChangeHandler> _logger;
    public NotifyOrderStatusChangeHandler
    (
        IHubContext<NotificationHub, INotificationHub> hubContext,
        ILogger<NotifyOrderStatusChangeHandler> logger
    )
    {
        _hubContext = hubContext;
        _logger = logger;
    }
    public async Task Handle(NotifyOrderStatusChangeCommand request, CancellationToken cancellationToken)
    {
        var functionName = $"{nameof(NotifyOrderStatusChangeHandler)} Payload = {JsonSerializer.Serialize(request.Payload)} => ";

        try
        {
            _logger.LogInformation(functionName);
            await SendNotification(request.Payload);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"{functionName} Has Error: {e.Message}");
        }
    }
    
    #region Private Methods

    private async Task SendNotification(NotifyOrderStatusChangeRequest request)
    {
        var storage = new ConnectionStorage();
        var message = new OrderStatusChangeNotification();
        switch (request.OrderStatus)
        {
            case OrderStatus.Preparing:
            {
                var content = OrderNotificationContent.OrderPreparing
                    .Replace("{{orderId}}", request.OrderId)
                    .Replace("{{restaurant}}", request.RestaurantName);
                message.Title = OrderNotificationTitle.OrderPreparing;
                message.Content = content;
                break;
            }
            default:
            {
                break;
            }
        }
        await _hubContext.Clients.Client(storage.connections[request.AccountId]).NotifyOrderStatusChange(message);
    }
    
    #endregion
}