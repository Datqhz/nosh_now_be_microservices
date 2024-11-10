using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreService.Data.Models.Configurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable(nameof(Employee));
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
        builder.Property(x => x.IsActive)
            .IsRequired()
            .HasDefaultValue(true);
        builder.Property(x => x.Avatar)
            .IsRequired();
        builder.Property(x => x.Role)
            .IsRequired();
        builder.Property(x => x.AccountId)
            .IsRequired();
        builder.Property(x => x.RestaurantId)
            .IsRequired();
        builder.HasOne(x => x.Restaurant)
            .WithMany(x => x.Employees)
            .HasForeignKey(x => x.RestaurantId);
    }
}