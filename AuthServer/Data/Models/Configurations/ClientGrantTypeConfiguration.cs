using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthServer.Data.Models.Configurations;

public class ClientGrantTypeConfiguration : IEntityTypeConfiguration<ClientGrantTypes>
{
    public void Configure(EntityTypeBuilder<ClientGrantTypes> builder)
    {
        builder.ToTable(nameof(ClientGrantTypes));
        builder.HasData(
            new ClientGrantTypes { Id = 1, GrantType = "password", ClientId = 1 },
            new ClientGrantTypes { Id = 2, GrantType = "code", ClientId = 1 },
            new ClientGrantTypes { Id = 3, GrantType = "client_credentials", ClientId = 1 });
    }
}