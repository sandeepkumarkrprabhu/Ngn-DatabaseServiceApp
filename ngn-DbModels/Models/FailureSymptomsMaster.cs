namespace ngn_DbModels.Models
{
    public class FailureSymptomsMaster
    {
        public int FailureSymptomsId { get; set; }

        public string FailureSymptomsName { get; set; } = string.Empty;

        public int ProjectId { get; set; }

        public ProjectMaster Project { get; set; } = null!;
    }
}
