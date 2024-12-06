using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OrderService.Data.Models.Configurations;

public class VoucherWalletConfiguration : IEntityTypeConfiguration<VoucherWallet>
{
    public void Configure(EntityTypeBuilder<VoucherWallet> builder)
    { 
        builder.ToTable(nameof(VoucherWallet));
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();
        builder.Property(x => x.Amount)
            .IsRequired()
            .HasColumnType("decimal");
        builder.Property(x => x.StartDate)
            .IsRequired()
            .HasColumnType("timestamp without time zone");
        builder.Property(x => x.ExpiredDate)
            .IsRequired()
            .HasColumnType("timestamp without time zone");
        builder.Property(x => x.CustomerId)
            .IsRequired();
        builder.HasOne(x => x.Customer)
            .WithMany(x => x.VoucherWallets)
            .HasForeignKey(x => x.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
