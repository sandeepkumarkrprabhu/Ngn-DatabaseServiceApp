using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ngn_DbModels.Models
{
    public class CircleMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CircleCityId { get; set; }

        [Required]
        [MaxLength(5)]
        public string CircleCode { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string CircleName { get; set; } = string.Empty;

        [Required]
        [MaxLength(5)]
        public string CityCode { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string CityName { get; set; } = string.Empty;

        public ICollection<MasterData> MasterData { get; set; } = new List<MasterData>();
    }
}