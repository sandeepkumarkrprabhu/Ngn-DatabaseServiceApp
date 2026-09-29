
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ngn_DbModels.Models
{
    public class ProjectEmailsMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProjectEmailId { get; set; }

        [Required]
        [MaxLength(100)]
        public string ToEmailAddress { get; set; } = string.Empty;

        [MaxLength(100)]
        public string CCEmailAddress { get; set; } = string.Empty;

        // Navigation property
        [ForeignKey(nameof(ProjectId))]
        public ProjectMaster ProjectId { get; set; }

        public bool isActive { get; set; } = true;
        public bool IsCustomerEmailOverride { get; set; } = false;
    }
}
