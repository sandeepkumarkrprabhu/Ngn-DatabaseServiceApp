using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ngn_DbModels.Models;

namespace ngn_data.Configurations
{
    public class MasterDataConfiguration : IEntityTypeConfiguration<MasterData>
    {
        public void Configure(EntityTypeBuilder<MasterData> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.SSA).HasMaxLength(100);
            builder.Property(x => x.CityType).HasMaxLength(50);
            builder.Property(x => x.SiteName).HasMaxLength(100);
            builder.Property(x => x.ExchangeName).HasMaxLength(100);
            builder.Property(x => x.SiteAddress).HasMaxLength(500);
            builder.Property(x => x.IPAddress).HasMaxLength(45);
            builder.Property(x => x.EquipmentName).HasMaxLength(100);
            builder.Property(x => x.Pincode).HasMaxLength(10);
            builder.HasOne(x => x.Project).WithMany(x => x.MasterData).HasForeignKey(x => x.ProjectId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.CircleCity).WithMany(x => x.MasterData).HasForeignKey(x => x.CircleCityId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.BSNLContact).WithMany().HasForeignKey(x => x.BSNLContactId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.HCLContact).WithMany().HasForeignKey(x => x.HCLContactId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.UTContact).WithMany().HasForeignKey(x => x.UTContactId).OnDelete(DeleteBehavior.Restrict);
            builder.HasIndex(x => new { x.ProjectId, x.CircleCityId });
        }
    }
}