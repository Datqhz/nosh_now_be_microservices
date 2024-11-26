using CommunicationService.Data.Contexts;
using CommunicationService.Data.Models;
using CommunicationService.Repositories.Interfaces;

namespace CommunicationService.Repositories.Implements;

public class NotificationRepository : GenericRepository<Notification>, INotificationRepository
{
    public NotificationRepository(CommunicationDbContext context) : base(context)
    {
    }
}