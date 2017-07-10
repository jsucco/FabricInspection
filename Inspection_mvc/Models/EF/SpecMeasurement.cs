namespace Inspection_mvc.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SpecMeasurement
    {
        public int id { get; set; }

        public int? TabTemplateId { get; set; }

        public int SpecId { get; set; }

        public int? DefectId { get; set; }

        public int? ItemNumber { get; set; }

        public int? InspectionJobSummaryId { get; set; }

        public int? InspectionId { get; set; }

        public DateTime Timestamp { get; set; }

        public decimal MeasureValue { get; set; }

        public decimal? SpecDelta { get; set; }

        public virtual InspectionJobSummary InspectionJobSummary { get; set; }

        public virtual ProductSpecification ProductSpecification { get; set; }
    }
}
