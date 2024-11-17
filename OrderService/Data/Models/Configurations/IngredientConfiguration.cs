using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OrderService.Data.Models.Configurations;

public class IngredientConfiguration : IEntityTypeConfiguration<Ingredient>
{
    public void Configure(EntityTypeBuilder<Ingredient> builder)
    {
        builder.ToTable(nameof(Ingredient));
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(200)
            .HasColumnType("varchar");
        builder.Property(x => x.Image)
            .IsRequired();
        builder.Property(x => x.Quantity)
            .IsRequired()
            .HasDefaultValue(0);
        builder.Property(x => x.Unit)
            .IsRequired();
        builder.Property(x => x.RestaurantId)
            .IsRequired();
        builder.HasOne(x => x.Restaurant)
            .WithMany(x => x.Ingredients)
            .HasForeignKey(x => x.RestaurantId);
    }
}