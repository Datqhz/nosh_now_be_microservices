using CommunicationService.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace CommunicationService.Repositories;

public interface IUnitOfRepository
{
    public INotificationRepository Notification { get; }
    public ISignalRConnectionRepository SignalRConnection { get; }

    Task CompleteAsync();
    Task<IDbContextTransaction> OpenTransactionAsync();
    Task CommitAsync();
    Task RollbackAsync();
}