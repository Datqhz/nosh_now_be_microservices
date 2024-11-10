using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthServer.Data.Models.Configurations;

public class ApiResourceScopesConfiguration : IEntityTypeConfiguration<ApiResourceScopes>
{
    public void Configure(EntityTypeBuilder<ApiResourceScopes> builder)
    {
        builder.ToTable(nameof(ApiResourceScopes));
        builder.HasData(
            new ApiResourceScopes { Id = 1, Scope = "Admin", ApiResourceId = 1 },
            new ApiResourceScopes { Id = 2, Scope = "Customer", ApiResourceId = 1 },
            new ApiResourceScopes { Id = 3, Scope = "Restaurant", ApiResourceId = 1 },
            new ApiResourceScopes { Id = 4, Scope = "ServiceStaff", ApiResourceId = 1 },
            new ApiResourceScopes { Id = 5, Scope = "Chef", ApiResourceId = 1 }
            );
    }
}