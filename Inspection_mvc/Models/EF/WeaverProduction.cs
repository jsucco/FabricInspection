namespace Inspection_mvc.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("WeaverProduction")]
    public partial class WeaverProduction
    {
        public int Id { get; set; }

        public int? EmployeeNoId { get; set; }

        public int JobSummaryId { get; set; }

        public decimal Yards { get; set; }

        public int ShiftId { get; set; }

        public string OEOP_ADP { get; set; }

        public virtual EmployeeNo EmployeeNo { get; set; }

        public virtual InspectionJobSummary InspectionJobSummary { get; set; }

        public virtual WeaverShift WeaverShift { get; set; }
    }
}
