using CoreService.Data.DbContexts;
using CoreService.Data.Models;
using CoreService.Repositories.Interfaces;

namespace CoreService.Repositories.Implements;

public class ShipperRepository : GenericRepository<Shipper>, IShipperRepository
{
    public ShipperRepository(CoreDbContext context) : base(context)
    {
    }
}