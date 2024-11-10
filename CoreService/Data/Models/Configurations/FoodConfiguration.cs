using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreService.Data.Models.Configurations;

public class FoodConfiguration : IEntityTypeConfiguration<Food>
{
    public void Configure(EntityTypeBuilder<Food> builder)
    {
        builder.ToTable(nameof(Food));
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnType("varchar");
        builder.Property(x => x.Description);
        builder.Property(x => x.Image)
            .IsRequired();
        builder.Property(x => x.Price)
            .IsRequired();
        builder.Property(x => x.CategoryId)
            .IsRequired();
        builder.Property(x => x.RestaurantId)
            .IsRequired();
        builder.HasOne(x => x.Category)
            .WithMany(x => x.Foods)
            .HasForeignKey(x => x.CategoryId);
        builder.HasOne(x => x.Restaurant)
            .WithMany(x => x.Foods)
            .HasForeignKey(x => x.RestaurantId);
    }
}