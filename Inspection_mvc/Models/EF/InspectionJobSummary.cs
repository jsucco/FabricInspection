namespace Inspection_mvc.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("InspectionJobSummary")]
    public partial class InspectionJobSummary
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public InspectionJobSummary()
        {
            DefectTimers = new HashSet<DefectTimer>();
            SpecMeasurements = new HashSet<SpecMeasurement>();
            WeaverProductions = new HashSet<WeaverProduction>();
        }

        public int id { get; set; }

        [StringLength(20)]
        public string JobType { get; set; }

        [StringLength(30)]
        public string JobNumber { get; set; }

        [StringLength(50)]
        public string DataNo { get; set; }

        [StringLength(10)]
        public string CID { get; set; }

        public int TemplateId { get; set; }

        public int ItemPassCount { get; set; }

        public int ItemFailCount { get; set; }

        public int? WOQuantity { get; set; }

        public int? WorkOrderPieces { get; set; }

        public decimal? AQL_Level { get; set; }

        [Required]
        [StringLength(15)]
        public string Standard { get; set; }

        public int? SampleSize { get; set; }

        public int? TotalInspectedItems { get; set; }

        public int? RejectLimiter { get; set; }

        public bool? Technical_PassFail { get; set; }

        public DateTime? Technical_PassFail_Timestamp { get; set; }

        public bool? UserConfirm_PassFail { get; set; }

        public DateTime? UserConfirm_PassFail_Timestamp { get; set; }

        public DateTime? Inspection_Started { get; set; }

        public DateTime? Inspection_Finished { get; set; }

        [StringLength(50)]
        public string PRP_Code { get; set; }

        public double? UnitCost { get; set; }

        public string UnitDesc { get; set; }

        public string Comments { get; set; }

        [StringLength(50)]
        public string ProdMachineName { get; set; }

        public int? MajorsCount { get; set; }

        public int? MinorsCount { get; set; }

        public int? RepairsCount { get; set; }

        public int? ScrapCount { get; set; }

        [StringLength(50)]
        public string EmployeeNo { get; set; }

        [StringLength(50)]
        public string CasePack { get; set; }

        [StringLength(10)]
        public string WorkRoom { get; set; }

        public int? RM_XrefId { get; set; }

        [StringLength(20)]
        public string ThreadColor { get; set; }

        public bool? ThreadColorConfirm { get; set; }

        public decimal? RollWidth { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DefectTimer> DefectTimers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SpecMeasurement> SpecMeasurements { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WeaverProduction> WeaverProductions { get; set; }
    }
}
