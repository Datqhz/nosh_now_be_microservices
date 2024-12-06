using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OrderService.Data.Models.Configurations;

public class RequiredIngredientConfiguration : IEntityTypeConfiguration<RequiredIngredient>
{
    public void Configure(EntityTypeBuilder<RequiredIngredient> builder)
    {
        builder.ToTable(nameof(RequiredIngredient));
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();
        builder.Property(x => x.FoodId)
            .IsRequired();
        builder.Property(x => x.IngredientId)
            .IsRequired();
        builder.Property(x => x.Quantity)
            .IsRequired();
        builder.HasOne(x => x.Food)
            .WithMany(x => x.RequiredIngredients)
            .HasForeignKey(x => x.FoodId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => x.Ingredient)
            .WithMany(x => x.RequiredIngredients)
            .HasForeignKey(x => x.IngredientId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}