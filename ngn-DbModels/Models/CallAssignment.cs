
using System.ComponentModel.DataAnnotations;

namespace ngn_DbModels.Models
{
    public class CallAssignment
    {
        [Key]
        public string CaseId { get; set; }

        public int ProjectId { get; set; }

        [MaxLength(20)]
        public string IPAddress { get; set; }


    }
}
 