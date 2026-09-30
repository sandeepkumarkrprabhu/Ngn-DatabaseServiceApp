using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ngn_DbModels.Models
{
    public class SeverityMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SeverityId { get; set; }

        [Required]
        [MaxLength(10)]
        public string SeverityCode { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string SeverityName { get; set; } = string.Empty;

        [MaxLength(30)]
        public string? Level { get; set; }

        public decimal ResponseSLA { get; set; }

        public decimal ResolutionSLA { get; set; }

        [MaxLength(100)]
        public string? Description { get; set; }
    }
}