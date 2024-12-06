using OrderService.Data.DbContexts;
using OrderService.Data.Models;
using OrderService.Repositories.Interfaces;

namespace OrderService.Repositories.Implements;

public class VoucherWalletRepository : GenericRepository<VoucherWallet>, IVoucherWalletRepository
{
    public VoucherWalletRepository(OrderDbContext context) : base(context)
    {
    }
}
