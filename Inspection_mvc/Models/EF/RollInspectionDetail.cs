namespace Inspection_mvc.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RollInspectionDetail")]
    public partial class RollInspectionDetail
    {
        public int id { get; set; }

        public int RollInspectionSummaryId { get; set; }

        public int TemplateId { get; set; }

        public int? ButtonTemplateId { get; set; }

        public int ShiftNumber { get; set; }

        public decimal DHY { get; set; }

        public int? DefectCount { get; set; }

        public virtual RollInspectionSummary RollInspectionSummary { get; set; }

        public virtual TemplateName TemplateName { get; set; }
    }
}
