using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ngn_DbModels.Models
{
    public class StatusMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StatusMasterId { get; set; }

        [Required]
        [MaxLength(50)]
        public string StatusCode { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string StatusName { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? ManagementType { get; set; }

        public bool IsTerminalFinalStatus { get; set; }

        [MaxLength(100)]
        public string? BadgeVisualStyle { get; set; }

        [MaxLength(100)]
        public string? Remarks { get; set; }

        public bool IsActive { get; set; } = true;
    }
}