
namespace ngn_DbModels.Models
{
    public class ProjectEmailsMaster
    {
        public int ProjectEmailId { get; set; }
        public string ToEmailAddress { get; set; } = string.Empty;
        public string? CCEmailAddress { get; set; }
        public int ProjectId { get; set; }
        public ProjectMaster Project { get; set; } = null!;

        public bool IsActive { get; set; } = true;

        public bool IsCustomerEmailOverride { get; set; }
    }
}