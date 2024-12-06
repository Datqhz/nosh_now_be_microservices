using CoreService.Data.DbContexts;
using CoreService.Repositories.Implements;
using CoreService.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace CoreService.Repositories;

public class UnitOfRepository : IUnitOfRepository
{
    private readonly CoreDbContext _context;
    private IDbContextTransaction _transaction;
    public IAdminRepository Admin { get; }
    public ICalendarRepository Calendar { get; }
    public IEmployeeRepository Employee { get; }
    
    public ILocationRepository Location { get; }

    public IRestaurantRepository Restaurant { get; }
    public IShipperRepository Shipper { get; }
    public ICustomerRepository Customer{ get; }
    public UnitOfRepository(CoreDbContext context)
    {
        _context = context;
        Customer = new CustomerRepository(context);
        Admin = new AdminRepository(context);
        Calendar = new CalendarRepository(context);
        Employee = new EmployeeRepository(context);
        Location = new LocationRepository(context);
        Restaurant = new RestaurantRepository(context);
        Shipper = new ShipperRepository(context);
        
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