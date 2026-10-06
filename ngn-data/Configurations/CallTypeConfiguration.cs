using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ngn_DbModels.Models;

namespace ngn_data.Configurations
{
    public class CallTypeConfiguration : IEntityTypeConfiguration<CallTypeMaster>
    {
        public void Configure(EntityTypeBuilder<CallTypeMaster> builder)
        {
            builder.HasKey(x => x.CallTypeId);
            builder.Property(x => x.CallName).HasMaxLength(50).IsRequired();
        }
    }
}