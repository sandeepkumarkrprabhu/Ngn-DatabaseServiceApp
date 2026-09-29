
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ngn_DbModels.Models
{
    public class ContactUs
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ContactUsId { get; set; }
        public string Circle { get; set; }
        public string ContactPerson { get; set; }
        public string Designation { get; set; }
        public string ContactNo { get; set; }
        public string EmailId { get; set; }


    }
}
