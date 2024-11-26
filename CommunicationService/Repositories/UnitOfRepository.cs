using CommunicationService.Data.Contexts;
using CommunicationService.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace CommunicationService.Repositories;

public class UnitOfRepository : IUnitOfRepository
{
    private readonly CommunicationDbContext _context;
    private IDbContextTransaction _transaction;
    public INotificationRepository Notification { get; }
    public ISignalRConnectionRepository SignalRConnection { get; }

    public UnitOfRepository(CommunicationDbContext context)
    {
        _context = context;
        
    }
    
    
    public async Task CompleteAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<IDbContextTransaction> OpenTransactionAsync()
    {
        _transaction = await _context.Database.BeginTransactionAsync();
        return _transaction;
    }

    public async Task CommitAsync()
    {
        await _transaction.CommitAsync();
    }

    public async Task RollbackAsync()
    {
        await _transaction.RollbackAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}