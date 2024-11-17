using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CommunicationService.Data.Models.Configurations;

public class SignalRConnectionConfiguration : IEntityTypeConfiguration<SignalRConnection>
{
    public void Configure(EntityTypeBuilder<SignalRConnection> builder)
    {
        builder.ToTable(nameof(SignalRConnection));
        builder.HasKey(x => x.Id);
        builder.Property(x => x.ConnectionId)
            .IsRequired();
        builder.Property(x => x.UserId)
            .IsRequired();
        builder.Property(x => x.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();

    }
}