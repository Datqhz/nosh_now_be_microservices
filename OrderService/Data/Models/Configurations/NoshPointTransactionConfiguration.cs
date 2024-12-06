using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OrderService.Data.Models.Configurations;

public class NoshPointTransactionConfiguration : IEntityTypeConfiguration<NoshPointTransaction>
{
    public void Configure(EntityTypeBuilder<NoshPointTransaction> builder)
    {
        builder.ToTable(nameof(NoshPointTransaction));
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();
        builder.Property(x => x.Amount)
            .IsRequired()
            .HasColumnType("decimal");
        builder.Property(x => x.CreatedDate)
            .IsRequired()
            .HasColumnType("timestamp without time zone");
        builder.Property(x => x.CustomerId)
            .IsRequired();
        builder.Property(x => x.OrderId)
            .IsRequired();
        builder.Property(x => x.VoucherId);
        builder.HasOne(x => x.Customer)
            .WithMany(x => x.Transactions)
            .HasForeignKey(x => x.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => x.Voucher)
            .WithMany(x => x.Transaction)
            .HasForeignKey(x => x.VoucherId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
