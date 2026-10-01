namespace ngn_DbModels.Models
{
    public class CallRegisterForOthers
    {
        public string CaseId { get; set; }
        public int ProjectId { get; set; }
        public string IPAddress { get; set; }
        public string BSNLContactName { get; set; }
        public string BSNLContactNumber { get; set; }
        public string LandlineNumber { get; set; }
        public string EmailId { get; set; }
        public string CallType { get; set; }
        public int NatureOfProblenId { get; set; }
        public string ProblemDescription { get; set; }
        public string LoginName { get; set; }
        public DateTime LoginDate { get; set; }
        public DateTime CreateDate { get; set; }
        public int LoginTimeHours { get; set; }
        public int LoginTimeMinutes { get; set; }
        public string PartNo { get; set; }
        public string SiteName { get; set; }
        public int EquipmentId { get; set; }
        public int SeverityId { get; set; }
        public int DocketStatusId { get; set; }

        public ProjectMaster ProjectNavId { get; set; }
        public EquipmentMaster EquipmentNavId { get; set; }
        public StatusMaster StatusMasterNavId { get; set; }
    }
}