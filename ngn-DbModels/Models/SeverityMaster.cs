using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ngn_DbModels.Models
{
    public class SeverityMaster
    {
        [Key]
        [DatabaseGenerated(databaseGeneratedOption: DatabaseGeneratedOption.Identity)]
        public int SeverityId { get; set; }
        public string SeverityCode { get; set; }
        public string SeverityName { get; set; }
        public string Level { get; set; }
        public decimal ResponseSLA { get; set; }
        public decimal ResolutionSLA { get; set; }
        public string Description { get; set; }
    }
}
