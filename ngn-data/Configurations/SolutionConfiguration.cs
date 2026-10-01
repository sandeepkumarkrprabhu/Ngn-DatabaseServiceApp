using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ngn_DbModels.Models;

namespace ngn_data.Configurations
{
    public class SolutionConfiguration : IEntityTypeConfiguration<Solution>
    {
        public void Configure(EntityTypeBuilder<Solution> builder)
        {
            builder.HasKey(x => x.SolutionId);
            builder.Property(x => x.SolutionId).ValueGeneratedOnAdd();
            builder.Property(x => x.IPAddress).HasMaxLength(45);
            builder.Property(x => x.ProblemType).HasMaxLength(100);
            builder.Property(x => x.RMANo).HasMaxLength(250);
            builder.Property(x => x.SRNo).HasMaxLength(50);
        }
    }
}