using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ngn_DbModels.Models;

namespace ngn_data.Configurations
{
    public class EquipmentConfiguration : IEntityTypeConfiguration<EquipmentMaster>
    {
        public void Configure(EntityTypeBuilder<EquipmentMaster> builder)
        {
            builder.HasKey(x => x.EquipmentId);
            builder.Property(x => x.EquipmentName).HasMaxLength(100).IsRequired();
            builder.Property(x => x.EquipmentDescription).HasMaxLength(200);

            builder.HasIndex(x => new { x.ProjectId, x.EquipmentName }).IsUnique();

            builder.HasOne(x => x.Project)
                   .WithMany(x => x.Equipment)
                   .HasForeignKey(x => x.ProjectId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}