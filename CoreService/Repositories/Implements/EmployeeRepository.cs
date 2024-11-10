using CoreService.Data.DbContexts;
using CoreService.Data.Models;
using CoreService.Repositories.Interfaces;

namespace CoreService.Repositories.Implements;

public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
{
    public EmployeeRepository(CoreDbContext context) : base(context)
    {
    }
}