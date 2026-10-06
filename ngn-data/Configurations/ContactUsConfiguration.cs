using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ngn_DbModels.Models;

namespace ngn_data.Configurations
{
    public class ContactUsConfiguration : IEntityTypeConfiguration<ContactUs>
    {
        public void Configure(EntityTypeBuilder<ContactUs> builder)
        {
            builder.HasKey(x => x.ContactUsId);
            builder.Property(x => x.Circle).HasMaxLength(100);
            builder.Property(x => x.ContactPerson).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Designation).HasMaxLength(100);
            builder.Property(x => x.ContactNo).HasMaxLength(20);
            builder.Property(x => x.EmailId).HasMaxLength(150);
        }
    }
}