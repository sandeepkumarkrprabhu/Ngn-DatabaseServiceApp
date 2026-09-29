using Microsoft.EntityFrameworkCore;
using ngn_DbModels.Models;

namespace ngn_data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProjectMaster>(entity =>
            {
                entity.HasOne(p => p.ProjectGroup)
                      .WithMany(g => g.Projects)
                      .HasForeignKey(p => p.ProjectGroupId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<EquipmentPartNumber>(entity =>
            {
                entity.HasOne(e => e.Equipment)
                      .WithMany(e => e.PartNumbers)
                      .HasForeignKey(e => e.EquipmentId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(e => new { e.EquipmentId, e.PartNumber })
                      .IsUnique();

                entity.HasIndex(e => new { e.EquipmentId, e.IsActive });
            });

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning));
        }

        public DbSet<ProjectGroupMaster> ProjectGroups { get; set; }
        public DbSet<ProjectMaster> ProjectMasters { get; set; }
        public DbSet<ProjectEmailsMaster> ProjectEmails { get; set; }
        public DbSet<ProjectItemDetails> ProjectItemDetails { get; set; }
        public DbSet<ProjectEmailFeatureMapping> ProjectEmailFeatureMappings { get; set; }
        public DbSet<CircleByZoneMaster> CircleByZones { get; set; }
        public DbSet<CircleMaster> CircleMaster { get; set; }
        public DbSet<StatusMaster> StatusMasters { get; set; }
        public DbSet<NatureOfProblemMaster> NatureOfProblem { get; set; }
        public DbSet<FlashNewsNotification> FlashNewsNotification { get; set; }
        public DbSet<EquipmentMaster> Equipment { get; set; }
        public DbSet<EquipmentPartNumber> EquipmentPartNumbers { get; set; }
        public DbSet<ContactUs> ContactUs { get; set; }
        public DbSet<CallTypeMaster> CallTypeMaster { get; set; }
        public DbSet<MasterData> MasterData { get; set; }
        public DbSet<MasterDatacontact> MasterDatacontact { get; set; }

        public DbSet<CallRegister> CallRegister { get; set; }
        public DbSet<CallRegisterForOthers> CallRegisterForOthers { get; set; }
        public DbSet<Solution> Solutions { get; set; }
        public DbSet<CaseTechnicalComments> CaseTechnicalComments { get; set; }
    }
}
