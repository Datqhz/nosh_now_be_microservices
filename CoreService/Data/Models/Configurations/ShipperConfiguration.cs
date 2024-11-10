using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreService.Data.Models.Configurations;

public class ShipperConfiguration : IEntityTypeConfiguration<Shipper>
{
    public void Configure(EntityTypeBuilder<Shipper> builder)
    {
        builder.ToTable(nameof(Shipper));
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .IsRequired();
        builder.Property(x => x.DisplayName)
            .IsRequired()
            .HasColumnType("varchar(100)");
        builder.Property(x => x.Phone)
            .IsRequired()
            .HasColumnType("varchar(10)");
        builder.Property(x => x.Coordinate)
            .IsRequired();
        builder.Property(x => x.Avatar)
            .IsRequired();
        builder.Property(x => x.Status)
            .IsRequired();
    }
}