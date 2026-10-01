
namespace ngn_DbModels.Models
{
    public class EquipmentMaster
    {
        public int EquipmentId { get; set; }
        public string EquipmentName { get; set; } = string.Empty;
        public string? EquipmentDescription { get; set; }
        public int ProjectId { get; set; }
        public ProjectMaster Project { get; set; } = null!;

        public bool IsActive { get; set; } = true;

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public ICollection<EquipmentPartNumber> PartNumbers { get; set; }
            = new List<EquipmentPartNumber>();
    }
}