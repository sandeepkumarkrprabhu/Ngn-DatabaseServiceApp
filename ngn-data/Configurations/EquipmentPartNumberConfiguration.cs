using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ngn_DbModels.Models;

namespace ngn_data.Configurations
{
    public class EquipmentPartNumberConfiguration : IEntityTypeConfiguration<EquipmentPartNumber>
    {
        public void Configure(EntityTypeBuilder<EquipmentPartNumber> builder)
        {
            builder.HasKey(x => x.EquipmentPartNumberId);
            builder.Property(x => x.PartNumber).HasMaxLength(50).IsRequired();
            builder.HasIndex(x => new { x.EquipmentId, x.PartNumber }).IsUnique();
            builder.HasIndex(x => new { x.EquipmentId, x.IsActive });

            builder.HasOne(x => x.Equipment)
                   .WithMany(x => x.PartNumbers)
                   .HasForeignKey(x => x.EquipmentId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}