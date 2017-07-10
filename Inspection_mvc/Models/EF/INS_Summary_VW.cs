namespace Inspection_mvc.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class INS_Summary_VW
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        [StringLength(20)]
        public string JobType { get; set; }

        [StringLength(30)]
        public string JobNumber { get; set; }

        [StringLength(50)]
        public string INSDataNum { get; set; }

        [StringLength(10)]
        public string LOCCID { get; set; }

        [StringLength(30)]
        public string LOCName { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int INStemplateID { get; set; }

        [StringLength(50)]
        public string TMPName { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ItemPassCount { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ItemFailCount { get; set; }

        public int? WOQuantity { get; set; }

        public int? WorkOrderPieces { get; set; }

        public decimal? AQL_Level { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(15)]
        public string Standard { get; set; }

        public int? INSSampleSize { get; set; }

        public int? TotalInspectedItems { get; set; }

        public int? INSRejectLimiter { get; set; }

        public bool? Technical_PassFail { get; set; }

        public DateTime? Technical_PassFail_Timestamp { get; set; }

        public bool? UserConfirm_PassFail { get; set; }

        public DateTime? UserConfirm_PassFail_Timestamp { get; set; }

        public DateTime? Inspection_Started { get; set; }

        public DateTime? Inspection_Finished { get; set; }

        public double? UnitCost { get; set; }

        public string UnitDesc { get; set; }

        public string Comments { get; set; }

        [StringLength(50)]
        public string ProdMachineName { get; set; }

        public int? MajorsCount { get; set; }

        public int? MinorsCount { get; set; }

        public int? RepairsCount { get; set; }

        public int? ScrapCount { get; set; }

        public int? DefectID { get; set; }

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

        public int? TemplateId { get; set; }

        public int? InspectionId { get; set; }

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

        [StringLength(10)]
        public string WorkRoom { get; set; }

        public int? InspectionJobSummaryId { get; set; }

        public int? TMPTemplateID { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Owner { get; set; }

        public DateTime? DateCreated { get; set; }

        public bool? Active { get; set; }

        [StringLength(10)]
        public string LineType { get; set; }

        public bool? Ins_GriegeBatch { get; set; }

        public bool? Ins_WorkOrderInspection { get; set; }

        public bool? Loc_STT { get; set; }

        public bool? Loc_CAR { get; set; }

        public bool? Loc_STJ { get; set; }

        public bool? Loc_SPA { get; set; }

        public bool? Loc_CDC { get; set; }

        public bool? Loc_LINYI { get; set; }

        public int? LOCID { get; set; }

        [StringLength(10)]
        public string Abreviation { get; set; }

        [StringLength(30)]
        public string DBname { get; set; }

        [StringLength(10)]
        public string CID { get; set; }

        [StringLength(30)]
        public string ConnectionString { get; set; }

        public bool? InspectionResults { get; set; }

        public bool? ProductionResults { get; set; }

        public bool? AS400_Connection { get; set; }

        [StringLength(50)]
        public string AS400_Abr { get; set; }

        public bool? Loc_PCE { get; set; }

        public bool? Loc_FSK { get; set; }

        public bool? Loc_FNL { get; set; }

        public bool? Loc_FPC { get; set; }
    }
}
