using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ngn_DbModels.Models;

namespace ngn_data.Configurations
{
    public class CircleByZoneConfiguration : IEntityTypeConfiguration<CircleByZoneMaster>
    {
        public void Configure(EntityTypeBuilder<CircleByZoneMaster> builder)
        {
            builder.HasKey(x => x.CircleZoneId);
            builder.Property(x => x.Zone).HasMaxLength(50).IsRequired();
            builder.Property(x => x.CircleName).HasMaxLength(100).IsRequired();
            builder.HasIndex(x => new { x.Zone, x.CircleName }).IsUnique();
        }
    }
}