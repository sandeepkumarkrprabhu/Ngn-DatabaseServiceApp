
namespace ngn_DbModels.Models
{
    public class NatureOfProblemMaster
    {
        public int NatureOfProblemId { get; set; }
        public string NatureOfProblemName { get; set; } = string.Empty;

        public int? ProjectItemId { get; set; }
        public ProjectItemDetails? ProjectItem { get; set; }
    }
}