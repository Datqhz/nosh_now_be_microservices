using CoreService.Data.Models;
using CoreService.Data.Models.Configurations;
using Microsoft.EntityFrameworkCore;

namespace CoreService.Data.DbContexts;

public class CoreDbContext : DbContext
{
    public DbSet<Customer> User { get; set; }
    public DbSet<Admin> Admin { get; set; }
    public DbSet<Calendar> Calendar { get; set; }
    public DbSet<Employee> Employee { get; set; }
    public DbSet<Location> Location { get; set; }
    public DbSet<Restaurant> Restaurant { get; set; }
    public DbSet<Shipper> Shipper { get; set; }
    public CoreDbContext(){}
    public CoreDbContext(DbContextOptions<CoreDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema("core");
        modelBuilder.ApplyConfiguration(new CustomerConfiguration());
        modelBuilder.ApplyConfiguration(new AdminConfiguration());
        modelBuilder.ApplyConfiguration(new CalendarConfiguration());
        modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
        modelBuilder.ApplyConfiguration(new LocationConfiguration());
        modelBuilder.ApplyConfiguration(new RestaurantConfiguration());
        modelBuilder.ApplyConfiguration(new ShipperConfiguration());
    }
}