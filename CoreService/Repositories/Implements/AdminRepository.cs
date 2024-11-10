using CoreService.Data.DbContexts;
using CoreService.Data.Models;
using CoreService.Repositories.Interfaces;

namespace CoreService.Repositories.Implements;

public class AdminRepository : GenericRepository<Admin>, IAdminRepository
{
    public AdminRepository(CoreDbContext context) : base(context)
    {
    }
}