
namespace ngn_DbModels.Models
{
    public class ContactUs
    {
        public int ContactUsId { get; set; }
        public string? Circle { get; set; }
        public string ContactPerson { get; set; } = string.Empty;
        public string? Designation { get; set; }
        public string? ContactNo { get; set; }
        public string? EmailId { get; set; }
    }
}