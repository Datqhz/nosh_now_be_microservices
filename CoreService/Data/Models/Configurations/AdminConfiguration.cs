using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreService.Data.Models.Configurations;

public class AdminConfiguration : IEntityTypeConfiguration<Admin>
{
    public void Configure(EntityTypeBuilder<Admin> builder)
    {
        builder.ToTable(nameof(Admin));
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .IsRequired();
        builder.Property(x => x.DisplayName)
            .IsRequired()
            .HasColumnType("varchar(100)");
        builder.Property(x => x.Email)
            .IsRequired()
            .HasColumnType("varchar");
        builder.Property(x => x.IsActive)
            .IsRequired()
            .HasDefaultValue(true);
        builder.Property(x => x.Avatar)
            .IsRequired();
        builder.Property(x => x.AccountId)
            .IsRequired();
    }
}