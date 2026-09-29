
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ngn_DbModels.Models
{
    public class StatusMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int StatusMasterId { get; set; }

        [MaxLength(50)]
        public string StatusCode { get; set; } = string.Empty;

        [MaxLength(100)]
        public string StatusName { get; set; } = string.Empty;
        
        [MaxLength(100)]
        public string ManagementType { get; set; } = string.Empty;

        public bool IsTerminalFinalStatus { get; set; } = false;

        [MaxLength(100)]
        public string BadgevisualStype { get; set; } = string.Empty;

        [MaxLength(100)]
        public string Remarks { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;
    }
}
