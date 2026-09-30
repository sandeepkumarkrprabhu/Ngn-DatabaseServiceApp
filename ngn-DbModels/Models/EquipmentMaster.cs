using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ngn_DbModels.Models
{
    public class EquipmentMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EquipmentId { get; set; }

        [Required]
        [MaxLength(100)]
        public string EquipmentName { get; set; } = string.Empty;

        [MaxLength(200)]
        public string? EquipmentDescription { get; set; }

        [Required]
        public int ProjectId { get; set; }

        [ForeignKey(nameof(ProjectId))]
        public ProjectMaster Project { get; set; } = null!;

        public bool IsActive { get; set; } = true;

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public ICollection<EquipmentPartNumber> PartNumbers { get; set; }
            = new List<EquipmentPartNumber>();
    }
}