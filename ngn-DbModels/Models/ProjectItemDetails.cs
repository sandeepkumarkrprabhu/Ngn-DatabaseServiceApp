
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ngn_DbModels.Models
{
    public class ProjectItemDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProjectItemId { get; set; }

        [MaxLength(100)]
        public string ItemName { get; set; }

        [MaxLength(100)]
        public string SubType { get; set; }

        [MaxLength(50)]
        public string EquipmentName { get; set; }

        [MaxLength(255)]
        public string ItemDescription { get; set; }

        [MaxLength(50)]
        public string PartNumber { get; set; }

        // Navigation property
        [ForeignKey(nameof(ProjectId))]
        public ProjectMaster ProjectId { get; set; }
    }
}
