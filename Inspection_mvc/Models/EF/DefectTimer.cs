namespace Inspection_mvc.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DefectTimer")]
    public partial class DefectTimer
    {
        public int id { get; set; }

        [Required]
        [StringLength(30)]
        public string StatusValue { get; set; }

        public int InspectionJobSummaryId { get; set; }

        public int ButtonTemplateId { get; set; }

        public int DefectID { get; set; }

        [StringLength(100)]
        public string SessionId { get; set; }

        public int ButtonLocationId { get; set; }

        public DateTime Timestamp { get; set; }

        public DateTime? StopTimestamp { get; set; }

        public virtual ButtonTemplate ButtonTemplate { get; set; }

        public virtual DefectMaster DefectMaster { get; set; }

        public virtual InspectionJobSummary InspectionJobSummary { get; set; }
    }
}
