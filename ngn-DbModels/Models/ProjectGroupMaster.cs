using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ngn_DbModels.Models
{
    public class ProjectGroupMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProjectGroupId { get; set; }

        [Required]
        [MaxLength(100)]
        public string ProjectGroupName { get; set; } = string.Empty;

        [MaxLength(250)]
        public string? ProjectGroupDescription { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public ICollection<ProjectMaster> Projects { get; set; } = new List<ProjectMaster>();
    }
}