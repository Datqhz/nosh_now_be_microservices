using Microsoft.EntityFrameworkCore;
using OrderService.Data.Models;
using OrderService.Data.Models.Configurations;

namespace OrderService.Data.DbContexts;

public class OrderDbContext : DbContext
{
    public DbSet<Customer> User { get; set; }
    public DbSet<Employee> Employee { get; set; }
    public DbSet<Food> Food { get; set; }
    public DbSet<Restaurant> Restaurant { get; set; }
    public DbSet<Shipper> Shipper { get; set; }
    public DbSet<OrderDetail> OrderDetail { get; set; }
    public DbSet<Order> Order { get; set; }
    public DbSet<PaymentMethod> PaymentMethod { get; set; }
    public OrderDbContext(){}
    public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema("order");
        modelBuilder.ApplyConfiguration(new CustomerConfiguration());
        modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
        modelBuilder.ApplyConfiguration(new FoodConfiguration());
        modelBuilder.ApplyConfiguration(new OrderConfiguration());
        modelBuilder.ApplyConfiguration(new OrderDetailConfiguration());
        modelBuilder.ApplyConfiguration(new PaymentMethodConfiguration());
        modelBuilder.ApplyConfiguration(new RestaurantConfiguration());
        modelBuilder.ApplyConfiguration(new ShipperConfiguration());
    }
}