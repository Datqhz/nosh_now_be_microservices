﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OrderService.Data.Models.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable(nameof(Customer));
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .IsRequired();
        builder.Property(x => x.Name)
            .IsRequired();
        builder.Property(x => x.Avatar)
            .IsRequired();
    }
}