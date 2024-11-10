using OrderService.Data.DbContexts;
using OrderService.Data.Models;
using OrderService.Repositories.Interfaces;

namespace OrderService.Repositories.Implements;

public class ShipperRepository : GenericRepository<Shipper>, IShipperRepository
{
    public ShipperRepository(OrderDbContext context) : base(context)
    {
    }
}