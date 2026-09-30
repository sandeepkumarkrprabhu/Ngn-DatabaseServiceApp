using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ngn_DbModels.Models
{
    public class ProjectMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProjectId { get; set; }

        [Required]
        [MaxLength(100)]
        public string ProjectName { get; set; } = string.Empty;

        [Required]
        public int ProjectGroupId { get; set; }

        [ForeignKey(nameof(ProjectGroupId))]
        public ProjectGroupMaster ProjectGroup { get; set; } = null!;

        public bool IsActive { get; set; } = true;

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public ICollection<ProjectEmailsMaster> ProjectEmails { get; set; } = new List<ProjectEmailsMaster>();
        public ICollection<ProjectEmailFeatureMapping> EmailFeatureMappings { get; set; } = new List<ProjectEmailFeatureMapping>();
        public ICollection<ProjectItemDetails> ProjectItems { get; set; } = new List<ProjectItemDetails>();
        public ICollection<EquipmentMaster> Equipment { get; set; } = new List<EquipmentMaster>();
        public ICollection<MasterData> MasterData { get; set; } = new List<MasterData>();
    }
}