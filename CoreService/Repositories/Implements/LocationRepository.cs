using CoreService.Data.DbContexts;
using CoreService.Data.Models;
using CoreService.Repositories.Interfaces;

namespace CoreService.Repositories.Implements;

public class LocationRepository : GenericRepository<Location>, ILocationRepository
{
    public LocationRepository(CoreDbContext context) : base(context)
    {
    }
}