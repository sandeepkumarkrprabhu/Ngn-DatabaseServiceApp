
namespace ngn_DbModels.Models
{
    public class CircleMaster
    {
        public int CircleCityId { get; set; }
        public string CircleCode { get; set; } = string.Empty;
        public string CircleName { get; set; } = string.Empty;
        public string CityCode { get; set; } = string.Empty;
        public string CityName { get; set; } = string.Empty;

        public ICollection<MasterData> MasterData { get; set; } = new List<MasterData>();
    }
}