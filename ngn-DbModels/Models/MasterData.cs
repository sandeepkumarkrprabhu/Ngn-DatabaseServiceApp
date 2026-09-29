
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ngn_DbModels.Models
{
    public class MasterData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int projectid { get; set; }

        public int CircleCityId { get; set; }
        public string SSA { get; set; }
        public string CityType { get; set; }
        public string SiteName { get; set; }
        public string ExchangeName { get; set; }
        public string SiteAddress { get; set; }
        public string IPAddress { get; set; }
        public string EquipmentName { get; set; }
        public string Pincode { get; set; }

        public int? BSNLContactId { get; set; }
        public int? HCLContactId { get; set; }
        public int UTContactId { get; set; }

        // Navigation properties

        public ProjectMaster? Project { get; set; } = null!;

        public CircleMaster? CircleCity { get; set; } = null!;

        public MasterDatacontact? BSNLContact { get; set; }

        public MasterDatacontact? HCLContact { get; set; }

        public MasterDatacontact? UTContact { get; set; }
    }
}
