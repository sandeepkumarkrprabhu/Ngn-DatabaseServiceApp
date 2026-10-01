using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ngn_DbModels.Models;

namespace ngn_data.Configurations
{
    public class ProjectGroupConfiguration : IEntityTypeConfiguration<ProjectGroupMaster>
    {
        public void Configure(EntityTypeBuilder<ProjectGroupMaster> builder)
        {
            builder.HasKey(x => x.ProjectGroupId);

            builder.HasIndex(x => x.ProjectGroupName)
                   .IsUnique();

            builder.Property(x => x.ProjectGroupName)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(x => x.ProjectGroupDescription)
                   .HasMaxLength(250);
        }
    }
}