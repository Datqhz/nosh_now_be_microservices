using System.IdentityModel.Tokens.Jwt;
using CommunicationService.Data.Contexts;
using CommunicationService.Data.Models;
using CommunicationService.Hubs.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Shared.HttpContextAccessor;

namespace CommunicationService.Hubs;

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
    
    
    public async Task<string> SetupConnection(string accessToken)
        {
            try
            {
                var jwt = new JwtSecurityTokenHandler().ReadJwtToken(accessToken);
                var accountId = jwt.Claims.First(c => c.Type == Shared.Constants.Constants.CustomClaimTypes.AccountId).Value;

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
                return Context.ConnectionId;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        public override Task OnConnectedAsync()
        {
            Clients.Client(Context.ConnectionId).SendMessageConnection($"{Context.ConnectionId} - Connected");
            return base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var funcName = $"{nameof(NotificationHub)} {nameof(OnDisconnectedAsync)} ConnectionId = {Context.ConnectionId}";
            try
            {
                var connection = _communicationDbContext.SignalRConnection
                    .Where(i => i.ConnectionId == Context.ConnectionId);
                
                _communicationDbContext.SignalRConnection.RemoveRange(connection);
               
                var result = await _communicationDbContext.SaveChangesAsync();
                if (result > 0)
                {
                    Context.Abort();
                }
            }
            catch(Exception ex)
            {
                
            }
            await base.OnDisconnectedAsync(exception);
        }
    
}

public interface INotificationHub
{
    Task SendMessageConnection(string message);
    Task NotifyOrderStatusChange(OrderStatusChangeNotification notification);
}