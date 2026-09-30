using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ngn_DbModels.Models
{
    public class ContactUs
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ContactUsId { get; set; }

        [MaxLength(100)]
        public string? Circle { get; set; }

        [Required]
        [MaxLength(100)]
        public string ContactPerson { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? Designation { get; set; }

        [MaxLength(20)]
        public string? ContactNo { get; set; }

        [MaxLength(150)]
        public string? EmailId { get; set; }
    }
}