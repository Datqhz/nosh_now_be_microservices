using IdentityServer4.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthServer.Data.Models.Configurations;

public class ClientScopeConfiguration : IEntityTypeConfiguration<ClientScopes>
{
    public void Configure(EntityTypeBuilder<ClientScopes> builder)
    {
        builder.ToTable(nameof(ClientScopes));
        builder.HasData(
            new ClientScopes {Id = 1, Scope = "Admin", ClientId = 1},
            new ClientScopes {Id = 2, Scope = "Customer", ClientId = 1},
            new ClientScopes {Id = 3, Scope = "Restaurant", ClientId = 1},
            new ClientScopes {Id = 4, Scope = "Servicestaff", ClientId = 1},
            new ClientScopes {Id = 5, Scope = "Chef", ClientId = 1}
        );
    }
}