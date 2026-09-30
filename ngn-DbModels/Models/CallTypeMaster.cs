using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ngn_DbModels.Models
{
    public class CallTypeMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CallTypeId { get; set; }

        [Required]
        [MaxLength(50)]
        public string CallName { get; set; } = string.Empty;
    }
}