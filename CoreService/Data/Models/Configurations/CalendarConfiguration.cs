using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreService.Data.Models.Configurations;

public class CalendarConfiguration : IEntityTypeConfiguration<Calendar>
{
    public void Configure(EntityTypeBuilder<Calendar> builder)
    {
        builder.ToTable(nameof(Calendar));
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id)
            .ValueGeneratedOnAdd();
        builder.Property(c => c.WorkDate)
            .IsRequired()
            .HasColumnType("timestamp without time zone");
        builder.Property(x => x.OpeningTime)
            .IsRequired();
        builder.Property(x => x.ClosingTime)
            .IsRequired();
        builder.Property(x => x.RestaurantId)
            .IsRequired();
        builder.HasOne(x => x.Restaurant)
            .WithMany(x => x.Calendars)
            .HasForeignKey(x => x.RestaurantId);
    }
}