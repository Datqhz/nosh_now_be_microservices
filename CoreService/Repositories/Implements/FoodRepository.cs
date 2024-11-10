using CoreService.Data.DbContexts;
using CoreService.Data.Models;
using CoreService.Repositories.Interfaces;

namespace CoreService.Repositories.Implements;

public class FoodRepository : GenericRepository<Food> , IFoodRepository
{
    public FoodRepository(CoreDbContext context) : base(context)
    {
    }
}