
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ngn_DbModels.Models
{
    public class ProjectGroupMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProjectGroupId { get; set; }

        [MaxLength(100)]
        [Required]
        public string ProjectGroupName { get; set; }

        [MaxLength(250)]
        public string ProjectGroupDescription { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        // Inverse navigation (one group -> many projects)
        public ICollection<ProjectMaster> Projects { get; set; } = new List<ProjectMaster>();
    }
}
