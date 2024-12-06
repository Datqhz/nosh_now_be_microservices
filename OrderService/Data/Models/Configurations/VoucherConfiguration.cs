using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OrderService.Data.Models.Configurations;

public class VoucherConfiguration : IEntityTypeConfiguration<Voucher>
{
    public void Configure(EntityTypeBuilder<Voucher> builder)
    {
        builder.ToTable(nameof(NoshPointTransaction));
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();
        builder.Property(x => x.Amount)
            .IsRequired()
            .HasColumnType("decimal");
        builder.Property(x => x.StartDate)
            .IsRequired()
            .HasColumnType("timestamp without time zone");
        builder.Property(x => x.EndDate)
            .IsRequired()
            .HasColumnType("timestamp without time zone");
        builder.Property(x => x.Expired)
            .IsRequired();
        builder.Property(x => x.Quantity)
            .IsRequired();
        builder.Property(x => x.VoucherName)
            .IsRequired()
            .HasColumnType("varchar(100)");
        builder.Property(x => x.IsDeleted)
            .IsRequired()
            .HasDefaultValue(false);
    }
}
