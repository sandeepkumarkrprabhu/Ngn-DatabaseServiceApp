using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ngn_DbModels.Models;

namespace ngn_data.Configurations
{
    public class CaseTechnicalCommentsConfiguration : IEntityTypeConfiguration<CaseTechnicalComments>
    {
        public void Configure(EntityTypeBuilder<CaseTechnicalComments> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.TechnicalPersonName).HasMaxLength(100);
            builder.Property(x => x.TechnicalPersonNo).HasMaxLength(100);
            builder.Property(x => x.TechnicalComments).HasMaxLength(400);
            builder.HasOne<Solution>()
                   .WithMany()
                   .HasForeignKey(x => x.SolutionId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}