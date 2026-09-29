using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ngn_DbModels.Models
{
    public class CircleByZoneMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CircleZoneId { get; set; }

        [MaxLength(50)]
        public string Zone { get; set; }

        [MaxLength(100)]
        public string CircleName { get; set; }
    }
}
