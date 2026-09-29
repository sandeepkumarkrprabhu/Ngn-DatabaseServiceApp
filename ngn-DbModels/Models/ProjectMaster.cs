
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ngn_DbModels.Models
{
    public class ProjectMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProjectId { get; set; }

        [MaxLength(100)]
        [Required]
        public string ProjectName { get; set; }

        // Foreign key
        [Required]
        public int ProjectGroupId { get; set; }

        // Navigation property
        [ForeignKey(nameof(ProjectGroupId))]
        public ProjectGroupMaster ProjectGroup { get; set; }

        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
