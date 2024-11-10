using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthServer.Data.Models.Configurations;

public class RolePermissionConfiguration  : IEntityTypeConfiguration<RolePermission>
{
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        builder.ToTable(nameof(RolePermission));
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.Permission).IsRequired();
        builder.Property(x => x.Role).IsRequired();
        builder.HasData(
            new RolePermission {Id = 1, Permission = "Admin", Role = "Admin"},
            new RolePermission {Id = 2, Permission = "Customer", Role = "Customer"},
            new RolePermission {Id = 3, Permission = "Restaurant", Role = "Restaurant"},
            new RolePermission {Id = 4, Permission = "ServiceStaff", Role = "ServiceStaff"},
            new RolePermission {Id = 5, Permission = "Chef", Role = "Chef"}
        );
    }
}