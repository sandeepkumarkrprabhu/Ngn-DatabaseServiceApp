namespace ngn_DbModels.Models
{
    public class ProjectMaster
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; } = string.Empty;
        public int ProjectGroupId { get; set; }
        public ProjectGroupMaster ProjectGroup { get; set; } = null!;

        public bool IsActive { get; set; } = true;

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public ICollection<ProjectEmailsMaster> ProjectEmails { get; set; } = new List<ProjectEmailsMaster>();
        public ICollection<ProjectEmailFeatureMapping> EmailFeatureMappings { get; set; } = new List<ProjectEmailFeatureMapping>();
        public ICollection<ProjectItemDetails> ProjectItems { get; set; } = new List<ProjectItemDetails>();
        public ICollection<EquipmentMaster> Equipment { get; set; } = new List<EquipmentMaster>();
        public ICollection<MasterData> MasterData { get; set; } = new List<MasterData>();
        public ICollection<SeverityMaster> SeverityMasters { get; set; } = new List<SeverityMaster>();
    }
}