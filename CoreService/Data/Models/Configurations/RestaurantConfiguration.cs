﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreService.Data.Models.Configurations;

public class RestaurantConfiguration : IEntityTypeConfiguration<Restaurant>
{
    public void Configure(EntityTypeBuilder<Restaurant> builder)
    {
        builder.ToTable(nameof(Restaurant));
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .IsRequired();
        builder.Property(x => x.DisplayName)
            .IsRequired()
            .HasColumnType("varchar(100)");
        builder.Property(x => x.Email)
            .IsRequired()
            .HasColumnType("varchar");
        builder.Property(x => x.PhoneNumber)
            .IsRequired()
            .HasColumnType("varchar(10)");
        builder.Property(x => x.Coordinate)
            .IsRequired();
        builder.Property(x => x.IsActive)
            .IsRequired()
            .HasDefaultValue(false);
        builder.Property(x => x.Avatar)
            .IsRequired();
    }
}