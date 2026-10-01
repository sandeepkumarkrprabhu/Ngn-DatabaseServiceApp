using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ngn_DbModels.Models;

namespace ngn_data.Configurations
{
    public class MasterDatacontactConfiguration : IEntityTypeConfiguration<MasterDatacontact>
    {
        public void Configure(EntityTypeBuilder<MasterDatacontact> builder)
        {
            builder.HasKey(x => x.ContactID);
            builder.Property(x => x.ContactName).HasMaxLength(100).IsRequired();
            builder.Property(x => x.ContactNumber).HasMaxLength(20);
        }
    }
}