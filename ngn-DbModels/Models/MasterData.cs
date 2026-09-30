using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ngn_DbModels.Models
{
    public class MasterData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int ProjectId { get; set; }

        [Required]
        public int CircleCityId { get; set; }

        [MaxLength(100)]
        public string? SSA { get; set; }

        [MaxLength(50)]
        public string? CityType { get; set; }

        [MaxLength(100)]
        public string? SiteName { get; set; }

        [MaxLength(100)]
        public string? ExchangeName { get; set; }

        [MaxLength(500)]
        public string? SiteAddress { get; set; }

        [MaxLength(45)]
        public string? IPAddress { get; set; }

        [MaxLength(100)]
        public string? EquipmentName { get; set; }

        [MaxLength(10)]
        public string? Pincode { get; set; }

        public int? BSNLContactId { get; set; }
        public int? HCLContactId { get; set; }
        public int? UTContactId { get; set; }

        [ForeignKey(nameof(ProjectId))]
        public ProjectMaster Project { get; set; } = null!;

        [ForeignKey(nameof(CircleCityId))]
        public CircleMaster CircleCity { get; set; } = null!;

        [ForeignKey(nameof(BSNLContactId))]
        public MasterDatacontact? BSNLContact { get; set; }

        [ForeignKey(nameof(HCLContactId))]
        public MasterDatacontact? HCLContact { get; set; }

        [ForeignKey(nameof(UTContactId))]
        public MasterDatacontact? UTContact { get; set; }
    }
}