
namespace ngn_DbModels.Models
{
    public class ProjectItemDetails
    {
        public int ProjectItemId { get; set; }
        public string ItemName { get; set; } = string.Empty;
        public string? SubType { get; set; }
        public string? EquipmentName { get; set; }
        public string? ItemDescription { get; set; }
        public string? PartNumber { get; set; }
        public int ProjectId { get; set; }
        public ProjectMaster Project { get; set; } = null!;
    }
}