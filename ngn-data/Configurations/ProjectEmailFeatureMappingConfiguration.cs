using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ngn_DbModels.Models;

namespace ngn_data.Configurations
{
    public class ProjectEmailFeatureMappingConfiguration : IEntityTypeConfiguration<ProjectEmailFeatureMapping>
    {
        public void Configure(EntityTypeBuilder<ProjectEmailFeatureMapping> builder)
        {
            builder.HasKey(x => x.ProjectEmailFeatureMappingId);

            builder.HasIndex(x => x.ProjectId).IsUnique();

            builder.HasOne(x => x.Project)
                   .WithMany(x => x.EmailFeatureMappings)
                   .HasForeignKey(x => x.ProjectId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}