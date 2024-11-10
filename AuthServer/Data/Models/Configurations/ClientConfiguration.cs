using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthServer.Data.Models.Configurations;

public class ClientConfiguration : IEntityTypeConfiguration<Clients>
{
    public void Configure(EntityTypeBuilder<Clients> builder)
    {
        builder.ToTable(nameof(Clients));
        builder.HasData(
            new Clients { Id = 1, ClientId = "Mobile", ClientName = "mobile" }
            );
    }
}