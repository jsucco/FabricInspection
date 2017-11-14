namespace Inspection_mvc.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DefectMaster")]
    public partial class DefectMaster
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DefectMaster()
        {
            DefectTimers = new HashSet<DefectTimer>();
        }

        [Key]
        public int DefectID { get; set; }

        public DateTime? DefectTime { get; set; }

        [StringLength(50)]
        public string DefectDesc { get; set; }

        [StringLength(50)]
        public string POnumber { get; set; }

        [StringLength(50)]
        public string DataNo { get; set; }

        [StringLength(50)]
        public string EmployeeNo { get; set; }

        [StringLength(50)]
        public string AQL { get; set; }

        [StringLength(50)]
        public string ThisPieceNo { get; set; }

        [StringLength(50)]
        public string SampleSize { get; set; }

        public int? RejectLimiter { get; set; }

        [StringLength(50)]
        public string TotalLotPieces { get; set; }

        [StringLength(50)]
        public string Product { get; set; }

        [StringLength(50)]
        public string DefectClass { get; set; }

        public DateTime? MergeDate { get; set; }

        [StringLength(50)]
        public string Tablet { get; set; }

        [StringLength(50)]
        public string WorkOrder { get; set; }

        [StringLength(50)]
        public string LotNo { get; set; }

        [StringLength(50)]
        public string Location { get; set; }

        [StringLength(50)]
        public string DataType { get; set; }

        [StringLength(50)]
        public string Dimensions { get; set; }

        [StringLength(50)]
        public string Comment { get; set; }

        [StringLength(50)]
        public string LoomNo { get; set; }

        [StringLength(50)]
        public string DefectPoints { get; set; }

        [StringLength(50)]
        public string GriegeNo { get; set; }

        [StringLength(50)]
        public string RollNo { get; set; }

        [StringLength(50)]
        public string Operation { get; set; }

        public int TemplateId { get; set; }

        public int InspectionId { get; set; }

        public byte[] DefectImage { get; set; }

        [StringLength(100)]
        public string DefectImage_Filename { get; set; }

        [StringLength(100)]
        public string DefectImage_ContentType { get; set; }

        public int? ButtonTemplateId { get; set; }

        [StringLength(50)]
        public string Inspector { get; set; }

        [StringLength(50)]
        public string ItemNumber { get; set; }

        [StringLength(20)]
        public string InspectionState { get; set; }

        public decimal? CasePackConv { get; set; }

        [StringLength(40)]
        public string WorkRoom { get; set; }

        public int? InspectionJobSummaryId { get; set; }

        public int? WeaverShiftId { get; set; }

        public virtual TemplateName TemplateName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DefectTimer> DefectTimers { get; set; }
    }
}
