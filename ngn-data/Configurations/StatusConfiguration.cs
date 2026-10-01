using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ngn_DbModels.Models;

namespace ngn_data.Configurations
{
    public class StatusConfiguration : IEntityTypeConfiguration<StatusMaster>
    {
        public void Configure(EntityTypeBuilder<StatusMaster> builder)
        {
            builder.HasKey(x => x.StatusMasterId);
            builder.Property(x => x.StatusCode).HasMaxLength(50).IsRequired();
            builder.Property(x => x.StatusName).HasMaxLength(100).IsRequired();
            builder.Property(x => x.ManagementType).HasMaxLength(100);
            builder.Property(x => x.BadgeVisualStyle).HasMaxLength(100);
            builder.Property(x => x.Remarks).HasMaxLength(100);
            builder.HasIndex(x => x.StatusCode).IsUnique();
            builder.HasIndex(x => x.IsActive);
        }
    }
}