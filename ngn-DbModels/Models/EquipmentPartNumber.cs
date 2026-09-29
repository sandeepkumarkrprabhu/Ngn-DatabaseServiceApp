using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ngn_DbModels.Models
{
    public class EquipmentPartNumber
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EquipmentPartNumberId { get; set; }

        public int EquipmentId { get; set; }

        [Required]
        [MaxLength(50)]
        public string PartNumber { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        [ForeignKey(nameof(EquipmentId))]
        public EquipmentMaster Equipment { get; set; } = null!;
    }
}
