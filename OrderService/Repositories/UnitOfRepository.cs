using Microsoft.EntityFrameworkCore.Storage;
using OrderService.Data.DbContexts;
using OrderService.Repositories.Implements;
using OrderService.Repositories.Interfaces;

namespace OrderService.Repositories;

public class UnitOfRepository : IUnitOfRepository
{
    private readonly OrderDbContext _context;
    private IDbContextTransaction _transaction;
    public IEmployeeRepository Employee { get; }
    public ICategoryRepository Category { get; }
    public IRequiredIngredientRepository RequiredIngredient { get; }
    public IIngredientRepository Ingredient { get; }
    public IFoodRepository Food { get; }
    public IRestaurantRepository Restaurant { get; }
    public IShipperRepository Shipper { get; }
    public ICustomerRepository Customer { get; }
    public IOrderRepository Order { get; }
    public IOrderDetailRepository OrderDetail { get; }
    public IPaymentMethodRepository PaymentMethod { get; }
    public UnitOfRepository(OrderDbContext context)
    {
        _context = context;
        Customer = new CustomerRepository(context);
        Employee = new EmployeeRepository(context);
        Food = new FoodRepository(context);
        Restaurant = new RestaurantRepository(context);
        Shipper = new ShipperRepository(context);
        OrderDetail = new OrderDetailRepository(context);
        Order = new OrderRepository(context);
        PaymentMethod = new PaymentMethodRepository(context);
        Category = new CategoryRepository(context);
        Food = new FoodRepository(context);
        Ingredient = new IngredientRepository(context);
        RequiredIngredient = new RequiredIngredientRepository(context);
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