
namespace ngn_DbModels.Models
{
    public class ProjectGroupMaster
    {
        public int ProjectGroupId { get; set; }
        public string ProjectGroupName { get; set; } = string.Empty;
        public string? ProjectGroupDescription { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public ICollection<ProjectMaster> Projects { get; set; } = new List<ProjectMaster>();
    }
}