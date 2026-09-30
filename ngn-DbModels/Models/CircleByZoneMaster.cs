using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ngn_DbModels.Models
{
    public class CircleByZoneMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CircleZoneId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Zone { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string CircleName { get; set; } = string.Empty;
    }
}