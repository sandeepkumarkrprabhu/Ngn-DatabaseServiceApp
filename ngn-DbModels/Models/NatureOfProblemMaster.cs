using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ngn_DbModels.Models
{
    public class NatureOfProblemMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NatureOfProblemId { get; set; }

        [Required]
        [MaxLength(200)]
        public string NatureOfProblemName { get; set; } = string.Empty;

        public int? ProjectItemId { get; set; }

        [ForeignKey(nameof(ProjectItemId))]
        public ProjectItemDetails? ProjectItem { get; set; }
    }
}