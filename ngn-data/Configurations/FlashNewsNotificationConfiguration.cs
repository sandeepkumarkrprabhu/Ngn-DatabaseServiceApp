using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ngn_DbModels.Models;

namespace ngn_data.Configurations
{
    public class FlashNewsNotificationConfiguration : IEntityTypeConfiguration<FlashNewsNotification>
    {
        public void Configure(EntityTypeBuilder<FlashNewsNotification> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Code).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Title).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Message).HasMaxLength(200).IsRequired();
            builder.Property(x => x.Priority).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Category).HasMaxLength(100).IsRequired();
            builder.Property(x => x.TargetAudience).HasMaxLength(200).IsRequired();
            builder.Property(x => x.TargetCircle).HasMaxLength(100);
            builder.Property(x => x.CreatedBy).IsRequired();
            builder.HasIndex(x => new { x.Status, x.StartDate, x.EndDate });
        }
    }
}