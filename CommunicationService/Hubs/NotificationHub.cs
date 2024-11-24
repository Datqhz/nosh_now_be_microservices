using CommunicationService.Constants;
using CommunicationService.Hubs.Models;
using Microsoft.AspNetCore.SignalR;
using Shared.HttpContextAccessor;

namespace CommunicationService.Hubs;

public class NotificationHub :Hub<INotificationHub>
{
    private readonly ICustomHttpContextAccessor _httpContextAccessor;
    public NotificationHub
    (
        ICustomHttpContextAccessor httpContextAccessor
    )
    {
        _httpContextAccessor = httpContextAccessor;
    }
        
    public override Task OnConnectedAsync()
    {
        //var currentAccount = _httpContextAccessor.GetCurrentUserId();
        Console.WriteLine(Context.ConnectionId);
        var storage = new ConnectionStorage();
        //storage.connections.Add(currentAccount, Context.ConnectionId);
        Clients.Client(Context.ConnectionId).SendMessageConnection($"{Context.ConnectionId} - Connected");
        return base.OnConnectedAsync();
    }
    
}

public interface INotificationHub
{
    Task SendMessageConnection(string message);
    Task NotifyOrderStatusChange(OrderStatusChangeNotification notification);
}
