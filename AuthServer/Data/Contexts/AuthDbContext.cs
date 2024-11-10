using AuthServer.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuthServer.Data.DbContext;

public class AuthDbContext : IdentityDbContext
{
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Clients> Clients { get; set; }
    public DbSet<ClientGrantTypes> ClientGrantTypes { get; set; }
    public DbSet<ClientSecrets> ClientSecrets { get; set; }
    public DbSet<ClientScopes> ClientScopes { get; set; }
    public DbSet<ApiResources> ApiResources { get; set; }
    public DbSet<ApiResourceScopes> ApiResourceScope { get; set; }
    public AuthDbContext(){}
    public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options){}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("auth");
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AuthDbContext).Assembly);
    }
}