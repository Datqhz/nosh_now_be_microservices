using OrderService.Data.DbContexts;
using OrderService.Data.Models;
using OrderService.Repositories.Interfaces;

namespace OrderService.Repositories.Implements;

public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
{
    public CategoryRepository(OrderDbContext context) : base(context)
    {
    }
}