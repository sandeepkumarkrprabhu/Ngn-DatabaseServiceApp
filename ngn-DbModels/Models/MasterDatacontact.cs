using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ngn_DbModels.Models
{
    public class MasterDatacontact
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ContactID { get; set; }

        [Required]
        [MaxLength(100)]
        public string ContactName { get; set; } = string.Empty;

        [MaxLength(20)]
        public string? ContactNumber { get; set; }
    }
}