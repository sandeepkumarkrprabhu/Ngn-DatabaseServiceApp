
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ngn_DbModels.Models
{
    public class Solution
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SolutionId { get; set; }
        public string CaseId { get; set; }

        public int ProjectId { get; set; }

        [MaxLength(20)]
        public string IPAddress { get; set; }

        [MaxLength(100)]
        public string ProblemType { get; set; }

        [MaxLength(250)]
        public string RMANo { get; set; }

        [MaxLength(50)]
        public string SRNo { get; set; }

        public string Resolution { get; set; }
        public string TechnicalPersonName { get; set; }
        public string TechnicalPersonNo { get; set; }
        public string TechnicalPersonComment { get; set; }
        public string CallStatus { get; set; }
        public int SeverityId { get; set; }

        public DateTime ClosedDateTime { get; set; }
        public int Hours { get; set; }
        public int Minutes { get; set; }
        public string Ageing { get; set; }
        public string BSNLCallStatus { get; set; }
        public string BSNLLoginName { get; set; }
        public string BSNLClosedDate { get; set; }
        public int BSNLClosedHours { get; set; }
        public int BSNLClosedMinutes { get; set; }
        public DateTime ResponseTime { get; set; }
        public DateTime ResolutionTime { get; set; }


    }
}
