
namespace ngn_DbModels.Models
{
    public class StatusMaster
    {
        public int StatusMasterId { get; set; }
        public string StatusCode { get; set; } = string.Empty;
        public string StatusName { get; set; } = string.Empty;
        public string? ManagementType { get; set; }

        public bool IsTerminalFinalStatus { get; set; }
        public string? BadgeVisualStyle { get; set; }
        public string? Remarks { get; set; }

        public bool IsActive { get; set; } = true;
    }
}