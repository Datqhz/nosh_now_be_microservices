using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthServer.Data.Models.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        List<string> guids = Enumerable.Range(1, 5)
            .Select(_ => Guid.NewGuid().ToString())
            .ToList();
        builder.HasData(
            new IdentityRole{Id = guids[0], Name = "Customer", NormalizedName = "CUSTOMER" }, 
            new IdentityRole{Id = guids[1], Name = "Restaurant", NormalizedName = "RESTAURANT"},
            new IdentityRole{Id = guids[2], Name = "Admin", NormalizedName = "ADMIN"},
            new IdentityRole{Id = guids[3], Name = "ServiceStaff", NormalizedName = "SERVICESTAFF"},
            new IdentityRole{Id = guids[4], Name = "Chef", NormalizedName = "CHEF"}
        );
    }
}