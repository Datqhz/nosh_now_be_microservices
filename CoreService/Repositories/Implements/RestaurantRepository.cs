using CoreService.Data.DbContexts;
using CoreService.Data.Models;
using CoreService.Repositories.Interfaces;

namespace CoreService.Repositories.Implements;

public class RestaurantRepository : GenericRepository<Restaurant>, IRestaurantRepository
{
    public RestaurantRepository(CoreDbContext context) : base(context)
    {
    }
}