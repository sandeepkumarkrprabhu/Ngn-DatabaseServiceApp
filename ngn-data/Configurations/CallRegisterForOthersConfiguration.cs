using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ngn_DbModels.Models;

namespace ngn_data.Configurations
{
    public class CallRegisterForOthersConfiguration : IEntityTypeConfiguration<CallRegisterForOthers>
    {
        public void Configure(EntityTypeBuilder<CallRegisterForOthers> builder)
        {
            builder.HasKey(x => x.CaseId);
            builder.Property(x => x.CaseId).HasMaxLength(50);
            builder.Property(x => x.IPAddress).HasMaxLength(45);
            builder.Property(x => x.BSNLContactName).HasMaxLength(100);
            builder.Property(x => x.BSNLContactNumber).HasMaxLength(20);
            builder.Property(x => x.LandlineNumber).HasMaxLength(20);
            builder.Property(x => x.EmailId).HasMaxLength(100);
            builder.Property(x => x.CallType).HasMaxLength(100);
            builder.Property(x => x.ProblemDescription).HasMaxLength(200);
            builder.Property(x => x.LoginName).HasMaxLength(100);
            builder.Property(x => x.PartNo).HasMaxLength(100);
            builder.Property(x => x.SiteName).HasMaxLength(100);

            builder.HasOne(x => x.ProjectNavId).WithMany().HasForeignKey(x => x.ProjectId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.EquipmentNavId).WithMany().HasForeignKey(x => x.EquipmentId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.StatusMasterNavId).WithMany().HasForeignKey(x => x.DocketStatusId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}