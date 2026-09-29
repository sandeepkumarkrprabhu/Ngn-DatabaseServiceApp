
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ngn_DbModels.Models
{
    public class CircleMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CircleCityId { get; set; }

        [MaxLength(5)] 
        [Required]
        public string circleCode { get; set; }

        [MaxLength(100)]
        public string circleName { get; set; }

        [MaxLength(5)]
        [Required]
        public string CityCode { get; set; }

        [MaxLength(100)]
        public string CityName { get; set; }

    }
}
