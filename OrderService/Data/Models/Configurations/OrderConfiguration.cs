using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using Shared.Enums;

namespace OrderService.Data.Models.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable(nameof(Order));
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();
        builder.Property(x => x.OrderDate)
            .IsRequired()
            .HasColumnType("timestamp without time zone");
        builder.Property(x => x.Status)
            .IsRequired()
            .HasDefaultValue(OrderStatus.Init);
        builder.Property(x => x.ShippingFee)
            .HasDefaultValue(0);
        builder.Property(x => x.DeliveryInfo)
            .HasColumnType("jsonb")
            .HasConversion(
                v => JsonConvert.SerializeObject(v), 
                v => JsonConvert.DeserializeObject<DeliveryInfo>(v));
        builder.Property(x => x.CustomerId)
            .IsRequired();
        builder.Property(x => x.RestaurantId)
            .IsRequired();
        builder.HasOne(x => x.Customer)
            .WithMany(x => x.Orders)
            .HasForeignKey(x => x.CustomerId);
        builder.HasOne(x => x.Restaurant)
            .WithMany(x => x.Orders)
            .HasForeignKey(x => x.RestaurantId);
        builder.HasOne(x => x.Shipper)
            .WithMany(x => x.Orders)
            .HasForeignKey(x => x.ShipperId);
        builder.HasOne(x => x.PaymentMethod)
            .WithMany(x => x.Orders)
            .HasForeignKey(x => x.PaymentMethodId);
    }
}