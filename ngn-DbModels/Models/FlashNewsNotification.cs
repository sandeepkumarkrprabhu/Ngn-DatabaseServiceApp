
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ngn_DbModels.Models
{
    public class FlashNewsNotification
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(50)]
        public string Code { get; set; } = null!;

        [MaxLength(100)]
        public string Title { get; set; } = null!;

        [MaxLength(200)]
        public string Message { get; set; } = null!;

        [MaxLength(50)]
        public string Priority { get; set; }

        [MaxLength(100)]
        public string Category { get; set; }

        [MaxLength(200)]
        public string TargetAudience { get; set; }

        [MaxLength(100)]
        public string? TargetCircle { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool IsTicker { get; set; }

        public bool IsModalAlert { get; set; }

        public bool Status { get; set; }

        public string CreatedBy { get; set; } = null!;

        public DateTime CreatedAt { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
