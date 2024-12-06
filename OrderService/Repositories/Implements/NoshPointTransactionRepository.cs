using OrderService.Data.DbContexts;
using OrderService.Data.Models;
using OrderService.Repositories.Interfaces;

namespace OrderService.Repositories.Implements;

public class NoshPointTransactionRepository : GenericRepository<NoshPointTransaction>, INoshPointTransactionRepository
{
    public NoshPointTransactionRepository(OrderDbContext context) : base(context)
    {
    }
}
