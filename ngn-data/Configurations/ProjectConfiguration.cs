using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ngn_DbModels.Models;

namespace ngn_data.Configurations
{
    public class ProjectConfiguration : IEntityTypeConfiguration<ProjectMaster>
    {
        public void Configure(EntityTypeBuilder<ProjectMaster> builder)
        {
            builder.HasKey(x => x.ProjectId);
            builder.HasIndex(x => x.ProjectName).IsUnique();

            builder.Property(x => x.ProjectName).HasMaxLength(100).IsRequired();

            builder.HasOne(x => x.ProjectGroup)
                   .WithMany(x => x.Projects)
                   .HasForeignKey(x => x.ProjectGroupId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}