using CoreService.Data.DbContexts;
using CoreService.Data.Models;
using CoreService.Repositories.Interfaces;

namespace CoreService.Repositories.Implements;

public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
{
    public CategoryRepository(CoreDbContext context) : base(context)
    {
    }
}