
namespace ngn_DbModels.Models
{
    public class EquipmentPartNumber
    {
        public int EquipmentPartNumberId { get; set; }

        public int EquipmentId { get; set; }
        public string PartNumber { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }
        public EquipmentMaster Equipment { get; set; } = null!;
    }
}
