using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ngn_DbModels.Models;

namespace ngn_data.Configurations
{
    public class CircleConfiguration : IEntityTypeConfiguration<CircleMaster>
    {
        public void Configure(EntityTypeBuilder<CircleMaster> builder)
        {
            builder.HasKey(x => x.CircleCityId);
            builder.Property(x => x.CircleCode).HasMaxLength(5).IsRequired();
            builder.Property(x => x.CircleName).HasMaxLength(100).IsRequired();
            builder.Property(x => x.CityCode).HasMaxLength(5).IsRequired();
            builder.Property(x => x.CityName).HasMaxLength(100).IsRequired();

            builder.HasIndex(x => new { x.CircleCode, x.CityCode }).IsUnique();
            builder.HasIndex(x => x.CircleName);
            builder.HasIndex(x => x.CityName);
        }
    }
}