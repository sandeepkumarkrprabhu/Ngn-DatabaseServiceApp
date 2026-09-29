
using System.ComponentModel.DataAnnotations.Schema;

namespace ngn_DbModels.Models
{
    public class ProjectEmailFeatureMapping
    {
        public int ProjectEmailFeatureMappingId { get; set; }
        public bool IsCallLogged { get; set; } = false;
        public bool IsSLAEscalation { get; set; } = false;
        public bool IsTicketResolvedClosed { get; set; } = false;
        public bool IsCallAssignment { get; set; } = false;
        public bool IsRMAInitiateed { get; set; } = false;

        // Navigation property
        [ForeignKey(nameof(ProjectId))]
        public ProjectMaster ProjectId { get; set; }
    }
}
