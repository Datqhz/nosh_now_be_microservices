using OrderService.Data.DbContexts;
using OrderService.Data.Models;
using OrderService.Repositories.Interfaces;

namespace OrderService.Repositories.Implements;

public class IngredientRepository : GenericRepository<Ingredient>, IIngredientRepository
{
    public IngredientRepository(OrderDbContext context) : base(context)
    {
    }
}