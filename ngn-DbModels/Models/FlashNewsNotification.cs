

namespace ngn_DbModels.Models
{
    public class FlashNewsNotification
    {
        public int Id { get; set; }
        public string Code { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Message { get; set; } = null!;
        public string Priority { get; set; }
        public string Category { get; set; }
        public string TargetAudience { get; set; }
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
