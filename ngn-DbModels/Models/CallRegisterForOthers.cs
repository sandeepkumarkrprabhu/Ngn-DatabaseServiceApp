using System.ComponentModel.DataAnnotations;

namespace ngn_DbModels.Models
{
    public class CallRegisterForOthers
    {
        [Key]
        public string CaseId { get; set; }

        public int ProjectId { get; set; }

        [MaxLength(20)]
        public string IPAddress { get; set; }

        [MaxLength(100)]
        public string BSNLContactName { get; set; }

        [MaxLength(20)]
        public string BSNLContactNumber { get; set; }

        [MaxLength(20)]
        public string LandlineNumber { get; set; }

        [MaxLength(100)]
        public string EmailId { get; set; }

        [MaxLength(100)]
        public string CallType { get; set; }


        public int NatureOfProblenId { get; set; }

        [MaxLength(200)]
        public string ProblemDescription { get; set; }


        [MaxLength(100)]
        public string LoginName { get; set; }

        public DateTime LoginDate { get; set; }

        public DateTime CreateDate { get; set; }

        public int LoginTimeHours { get; set; }

        public int LoginTimeMinutes { get; set; }

        [MaxLength(100)]
        public string PartNo { get; set; }

        [MaxLength(100)]
        public string SiteName { get; set; }

        public int EquipmentId { get; set; }

        public int SeverityId { get; set; }

        public int DocketStatusId { get; set; }



        //navigationProperty
        public ProjectMaster ProjectNavId { get; set; }
        public EquipmentMaster EquipmentNavId { get; set; }

        public StatusMaster StatusMasterNavId { get; set; }
    }
}
