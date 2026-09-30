using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ngn_DbModels.Models
{
    public class ProjectEmailFeatureMapping
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProjectEmailFeatureMappingId { get; set; }

        public bool IsCallLogged { get; set; }
        public bool IsSLAEscalation { get; set; }
        public bool IsTicketResolvedClosed { get; set; }
        public bool IsCallAssignment { get; set; }
        public bool IsRMAInitiated { get; set; }

        [Required]
        public int ProjectId { get; set; }

        [ForeignKey(nameof(ProjectId))]
        public ProjectMaster Project { get; set; } = null!;
    }
}