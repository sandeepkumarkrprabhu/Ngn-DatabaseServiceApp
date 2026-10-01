using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ngn_DbModels.Models;

namespace ngn_data.Configurations
{
    public class ProjectItemConfiguration : IEntityTypeConfiguration<ProjectItemDetails>
    {
        public void Configure(EntityTypeBuilder<ProjectItemDetails> builder)
        {
            builder.HasKey(x => x.ProjectItemId);
            builder.Property(x => x.ItemName).HasMaxLength(100).IsRequired();
            builder.Property(x => x.SubType).HasMaxLength(100);
            builder.Property(x => x.EquipmentName).HasMaxLength(100);
            builder.Property(x => x.ItemDescription).HasMaxLength(500);
            builder.Property(x => x.PartNumber).HasMaxLength(50);

            builder.HasIndex(x => new { x.ProjectId, x.ItemName });

            builder.HasOne(x => x.Project)
                   .WithMany(x => x.ProjectItems)
                   .HasForeignKey(x => x.ProjectId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}