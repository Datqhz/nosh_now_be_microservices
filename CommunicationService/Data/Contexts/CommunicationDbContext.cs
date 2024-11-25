using CommunicationService.Data.Models;
using CommunicationService.Data.Models.Configurations;
using Microsoft.EntityFrameworkCore;

namespace CommunicationService.Data.Contexts;

public class CommunicationDbContext : DbContext
{
    public DbSet<Notification> Notification { get; set; }
    public DbSet<SignalRConnection> SignalRConnection { get; set; }
    public CommunicationDbContext(){}
    public CommunicationDbContext(DbContextOptions<CommunicationDbContext> options) : base(options){}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("auth");
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CommunicationDbContext).Assembly);
        modelBuilder.ApplyConfiguration(new NotificationConfiguration());
        modelBuilder.ApplyConfiguration(new SignalRConnectionConfiguration());
    }
}