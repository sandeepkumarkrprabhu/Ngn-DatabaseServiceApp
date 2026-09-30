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
        [MaxLength(254)]
        public string ToEmailAddress { get; set; } = string.Empty;

        [MaxLength(254)]
        public string? CCEmailAddress { get; set; }

        [Required]
        public int ProjectId { get; set; }

        [ForeignKey(nameof(ProjectId))]
        public ProjectMaster Project { get; set; } = null!;

        public bool IsActive { get; set; } = true;

        public bool IsCustomerEmailOverride { get; set; }
    }
}