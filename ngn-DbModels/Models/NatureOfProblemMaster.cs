
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
        public string NatureOfProblemName { get; set; }

        // Navigation property
        [ForeignKey(nameof(ProjectItemId))]
        public ProjectItemDetails ProjectItemId { get; set; }

    }
}
