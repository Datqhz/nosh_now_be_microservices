using CoreService.Data.DbContexts;
using CoreService.Data.Models;
using CoreService.Repositories.Interfaces;

namespace CoreService.Repositories.Implements;

public class RequiredIngredientRepository : GenericRepository<RequiredIngredient>, IRequiredIngredientRepository
{
    public RequiredIngredientRepository(CoreDbContext context) : base(context)
    {
    }
}