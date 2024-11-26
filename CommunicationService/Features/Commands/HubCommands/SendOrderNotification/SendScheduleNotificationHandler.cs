using MediatR;
using System.Text.Json;
using CommunicationService.Constants;
using CommunicationService.Hubs;
using CommunicationService.Hubs.Models;
using CommunicationService.Repositories;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Shared.Enums;
using Shared.MassTransits.Contracts;

namespace CommunicationService.Features.Commands.HubCommands.SendOrderNotification;

public class SendScheduleNotificationHandler : IRequestHandler<SendScheduleNotificationCommand>
{
    private readonly IUnitOfRepository _unitOfRepository;
    private readonly IHubContext<NotificationHub, INotificationHub> _hubContext;
    private readonly ILogger<SendScheduleNotificationHandler> _logger;
    public SendScheduleNotificationHandler
    (
        IHubContext<NotificationHub, INotificationHub> hubContext,
        IUnitOfRepository unitOfRepository,
        ILogger<SendScheduleNotificationHandler> logger
    )
    {
        _hubContext = hubContext;
        _unitOfRepository = unitOfRepository;
        _logger = logger;
    }
    public async Task Handle(SendScheduleNotificationCommand request, CancellationToken cancellationToken)
    {
        var functionName = $"{nameof(SendScheduleNotificationHandler)} Payload = {JsonSerializer.Serialize(request.Event)} => ";

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

    private async Task SendNotification(NotifyOrderSchedule eventData)
    {
        var receivers = await _unitOfRepository.SignalRConnection
            .Where(x => eventData.Receivers.Contains(x.UserId))
            .AsNoTracking()
            .Select(x => x.ConnectionId)
            .ToListAsync();
        var message = new OrderStatusChangeNotification();
        switch (eventData.OrderStatus)
        {
            case OrderStatus.CheckedOut:
            {
                var content = OrderNotificationContent.HaveANewOrder;
                message.Title = OrderNotificationTitle.HaveANewOrder;
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