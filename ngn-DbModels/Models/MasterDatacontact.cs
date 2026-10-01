
namespace ngn_DbModels.Models
{
    public class MasterDatacontact
    {
        public int ContactID { get; set; }
        public string ContactName { get; set; } = string.Empty;
        public string? ContactNumber { get; set; }
    }
}