using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ngn_DbModels.Models;

namespace ngn_data.Configurations
{
    public class ProjectEmailConfiguration : IEntityTypeConfiguration<ProjectEmailsMaster>
    {
        public void Configure(EntityTypeBuilder<ProjectEmailsMaster> builder)
        {
            builder.HasKey(x => x.ProjectEmailId);
            builder.Property(x => x.ToEmailAddress).HasMaxLength(254).IsRequired();
            builder.Property(x => x.CCEmailAddress).HasMaxLength(254);

            builder.HasIndex(x => new { x.ProjectId, x.IsActive });
            builder.HasOne(x => x.Project)
                   .WithMany(x => x.ProjectEmails)
                   .HasForeignKey(x => x.ProjectId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}