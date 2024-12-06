using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreService.Data.Models.Configurations;

public class LocationConfiguration : IEntityTypeConfiguration<Location>
{
    public void Configure(EntityTypeBuilder<Location> builder)
    {
        builder.ToTable(nameof(Location));
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();
        builder.Property(x => x.Name)
            .IsRequired();
        builder.Property(x => x.Phone)
            .IsRequired()
            .HasMaxLength(10)
            .HasColumnType("varchar");
        builder.Property(x => x.Coordinate)
            .IsRequired();
        builder.Property(x => x.CustomerId)
            .IsRequired();
        builder.HasOne(x => x.Customer)
            .WithMany(u => u.Locations)
            .HasForeignKey(x => x.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}