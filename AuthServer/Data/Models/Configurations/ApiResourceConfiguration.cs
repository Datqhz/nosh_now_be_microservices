using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthServer.Data.Models.Configurations;

public class ApiResourceConfiguration : IEntityTypeConfiguration<ApiResources>
{
    public void Configure(EntityTypeBuilder<ApiResources> builder)
    {
        builder.HasData(
            new ApiResources {Id = 1, Name = "Mobile", DisplayName = "Mobile"}
        );
    }
}