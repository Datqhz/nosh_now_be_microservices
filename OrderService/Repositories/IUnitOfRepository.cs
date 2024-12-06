using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using OrderService.Repositories.Interfaces;

namespace OrderService.Repositories;

public interface IUnitOfRepository
{
    IEmployeeRepository Employee { get; }
    public ICategoryRepository Category { get; }
    public IFoodRepository Food { get; }
    public IIngredientRepository Ingredient { get; }
    public IRequiredIngredientRepository RequiredIngredient { get; }
    public IRestaurantRepository Restaurant { get; }
    public IShipperRepository Shipper { get; }
    public ICustomerRepository Customer { get; }
    public IOrderRepository Order { get; }
    public IOrderDetailRepository OrderDetail { get; }
    public IPaymentMethodRepository PaymentMethod { get; }
    public INoshPointTransactionRepository NoshPointTransaction { get; }
    public IVoucherRepository Voucher { get; }
    public IVoucherWalletRepository VoucherWallet { get; }

    Task CompleteAsync();
    Task<IDbContextTransaction> OpenTransactionAsync();
    Task CommitAsync();
    Task RollbackAsync();
}