namespace ngn_DbModels.Models
{
    public class CaseTechnicalComments
    {
        public int Id { get; set; }
        public int SolutionId { get; set; }
        public string TechnicalPersonName { get; set; }
        public string TechnicalPersonNo { get; set; }
        public string TechnicalComments { get; set; }
    }
}