using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthServer.Data.Models.Configurations;

public class ClientSecretConfiguration : IEntityTypeConfiguration<ClientSecrets>
{
    public void Configure(EntityTypeBuilder<ClientSecrets> builder)
    {
        builder.ToTable(nameof(ClientSecrets));
        builder.HasData(
            new ClientSecrets{Id = 1, ClientId = 1, SecretName = "mobile"}
        );
    }
}