using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ngn_DbModels.Models;

namespace ngn_data.Configurations
{
    public class SeverityConfiguration : IEntityTypeConfiguration<SeverityMaster>
    {
        public void Configure(EntityTypeBuilder<SeverityMaster> builder)
        {
            builder.HasKey(x => x.SeverityId);
            builder.Property(x => x.SeverityCode).HasMaxLength(10).IsRequired();
            builder.Property(x => x.SeverityName).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Level).HasMaxLength(30);
            builder.Property(x => x.ResponseSLA).HasPrecision(10, 2);
            builder.Property(x => x.ResolutionSLA).HasPrecision(10, 2);
            builder.Property(x => x.Description).HasMaxLength(100);
            //builder.HasIndex(x => x.SeverityCode).IsUnique();
        }
    }
}