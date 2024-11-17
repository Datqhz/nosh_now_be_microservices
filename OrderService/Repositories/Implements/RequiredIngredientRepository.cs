using OrderService.Data.DbContexts;
using OrderService.Data.Models;
using OrderService.Repositories.Interfaces;

namespace OrderService.Repositories.Implements;

public class RequiredIngredientRepository : GenericRepository<RequiredIngredient>, IRequiredIngredientRepository
{
    public RequiredIngredientRepository(OrderDbContext context) : base(context)
    {
    }
}