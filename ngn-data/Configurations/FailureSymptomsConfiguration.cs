using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ngn_DbModels.Models;

namespace ngn_data.Configurations
{
    public class FailureSymptomsConfiguration : IEntityTypeConfiguration<FailureSymptomsMaster>
    {
        public void Configure(EntityTypeBuilder<FailureSymptomsMaster> builder)
        {
            builder.HasKey(x => x.FailureSymptomsId);

            builder.Property(x => x.FailureSymptomsName)
                   .HasMaxLength(200)
                   .IsRequired();

            builder.HasIndex(x => new
            {
                x.ProjectId,
                x.FailureSymptomsName
            })
            .IsUnique();

            builder.HasOne(x => x.Project)
                   .WithMany(x => x.FailureSymptoms)
                   .HasForeignKey(x => x.ProjectId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
