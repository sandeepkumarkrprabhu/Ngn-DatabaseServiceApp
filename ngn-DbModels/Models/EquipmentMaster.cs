using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ngn_DbModels.Models
{
    public class EquipmentMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EquipmentId { get; set; }

        [MaxLength(100)]
        public string EquipmentName { get; set; }

        [MaxLength(200)]
        public string? EquipmentDescription { get; set; }

        public int ProjectId { get; set; }

        public ICollection<EquipmentPartNumber> PartNumbers { get; set; }
            = new List<EquipmentPartNumber>();
    }
}
