using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ngn_DbModels.Models;

namespace ngn_data.Configurations
{
    public class NatureOfProblemConfiguration : IEntityTypeConfiguration<NatureOfProblemMaster>
    {
        public void Configure(EntityTypeBuilder<NatureOfProblemMaster> builder)
        {
            builder.HasKey(x => x.NatureOfProblemId);
            builder.Property(x => x.NatureOfProblemName).HasMaxLength(200).IsRequired();
            builder.HasIndex(x => new { x.ProjectItemId, x.NatureOfProblemName });

            builder.HasOne(x => x.ProjectItem)
                   .WithMany()
                   .HasForeignKey(x => x.ProjectItemId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}