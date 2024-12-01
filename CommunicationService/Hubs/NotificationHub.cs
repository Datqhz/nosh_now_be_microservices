using System.IdentityModel.Tokens.Jwt;
using CommunicationService.Data.Contexts;
using CommunicationService.Data.Models;
using CommunicationService.Hubs.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Shared.Extensions;
using Shared.HttpContextAccessor;

namespace CommunicationService.Hubs;

[Authorize]
public class NotificationHub :Hub<INotificationHub>
{
    private readonly ICustomHttpContextAccessor _httpContextAccessor;
    private readonly CommunicationDbContext _communicationDbContext;
    public NotificationHub
    (
        ICustomHttpContextAccessor httpContextAccessor,
        CommunicationDbContext communicationDbContext
    )
    {
        _httpContextAccessor = httpContextAccessor;
        _communicationDbContext = communicationDbContext;
    }

        public override async Task OnConnectedAsync()
        {
            try
            {
                var accountId = _httpContextAccessor.GetCurrentUserId();
                var connection = await _communicationDbContext.SignalRConnection
                    .Where(x => x.UserId == accountId)
                    .FirstOrDefaultAsync();

                if (connection == null)
                {
                    _communicationDbContext.SignalRConnection.Add(new SignalRConnection
                    {
                        UserId = accountId,
                        ConnectionId = Context.ConnectionId,
                    });
                }
                else
                {
                    connection.ConnectionId = Context.ConnectionId;
                }

                await _communicationDbContext.SaveChangesAsync();
                Clients.Client(Context.ConnectionId).SendMessageConnection($"{Context.ConnectionId} - Connected");
                await base.OnConnectedAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hub Has error: {ex.Message}");
            }
        }

        
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var funcName = $"{nameof(NotificationHub)} {nameof(OnDisconnectedAsync)} ConnectionId = {Context.ConnectionId}";
            try
            {
                Console.WriteLine(funcName);
                var connection =await _communicationDbContext.SignalRConnection
                    .Where(i => i.ConnectionId == Context.ConnectionId)
                    .FirstOrDefaultAsync();

                if (connection is null)
                {
                    _communicationDbContext.SignalRConnection.Remove(connection);
                    await _communicationDbContext.SaveChangesAsync(); 
                    Context.Abort();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"{funcName} Has error: {ex.Message}");
            }
            await base.OnDisconnectedAsync(exception);
        }
    
}

public interface INotificationHub
{
    Task SendMessageConnection(string message);
    Task NotifyOrderStatusChange(OrderStatusChangeNotification notification);
}