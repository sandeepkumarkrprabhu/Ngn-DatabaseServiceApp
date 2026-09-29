
using System.ComponentModel.DataAnnotations;

namespace ngn_DbModels.Models
{
    public class CaseTechnicalComments
    {
        [Key]
        public int Id { get; set; }

        public int SolutionId { get; set; }

        [MaxLength(100)]
        public string TechnicalPersonName { get; set; }

        [MaxLength(100)] 
        public string TechnicalPersonNo { get; set; }


        [MaxLength(400)]
        public string TechnicalComments { get; set; }
    }
}
