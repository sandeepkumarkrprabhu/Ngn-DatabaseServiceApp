
namespace ngn_DbModels.Models
{
    public class ProjectEmailFeatureMapping
    {
        public int ProjectEmailFeatureMappingId { get; set; }

        public bool IsCallLogged { get; set; }
        public bool IsSLAEscalation { get; set; }
        public bool IsTicketResolvedClosed { get; set; }
        public bool IsCallAssignment { get; set; }
        public bool IsRMAInitiated { get; set; }
        public int ProjectId { get; set; }
        public ProjectMaster Project { get; set; } = null!;
    }
}