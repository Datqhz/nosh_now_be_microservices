using CoreService.Data.DbContexts;
using CoreService.Data.Models;
using CoreService.Repositories.Interfaces;

namespace CoreService.Repositories.Implements;

public class CalendarRepository : GenericRepository<Calendar>, ICalendarRepository
{
    public CalendarRepository(CoreDbContext context) : base(context)
    {
    }
}