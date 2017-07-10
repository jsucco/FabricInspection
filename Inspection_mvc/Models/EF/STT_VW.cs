namespace Inspection_mvc.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class STT_VW
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string Machine { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(50)]
        public string WorkOrder { get; set; }

        [StringLength(50)]
        public string OperatorNo { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? FinishTime { get; set; }

        [StringLength(50)]
        public string DataNo { get; set; }

        [StringLength(50)]
        public string GreigeNo { get; set; }

        [StringLength(50)]
        public string CutLengthSpec { get; set; }

        public decimal? FinishLength { get; set; }

        public decimal? JobYds { get; set; }

        public int? JobSheets { get; set; }

        public decimal? JobOverLengthInches { get; set; }

        public decimal? ScheduledTime { get; set; }

        public decimal? DownTime { get; set; }

        public decimal? RunTime { get; set; }

        public decimal? Efficiency { get; set; }

        public decimal? AvgSheetsPerHour { get; set; }

        [StringLength(30)]
        public string RunPass { get; set; }

        public decimal? PH { get; set; }

        public decimal? Whiteness { get; set; }

        public decimal? ExitWidth { get; set; }

        public decimal? Absorbency { get; set; }

        public decimal? RollTicketYds { get; set; }

        public decimal? YieldYds { get; set; }

        public int? WOQUANTITY { get; set; }

        public int? JDECOMP { get; set; }

        public int? JDESCRAP { get; set; }

        public int? JDETOTREC { get; set; }

        public int? DIFF_OVER_UNDER { get; set; }

        public decimal? DIFF_PERC { get; set; }

        public DateTime? TimeStamp_Trans { get; set; }

        public DateTime? Updated { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int HourID { get; set; }

        public DateTime? HourBegin { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProductCount { get; set; }

        public decimal? OverLengthInches { get; set; }

        public decimal? HP_Cut_Length_Spec { get; set; }

        public decimal? AvgYdsPmin { get; set; }

        public decimal? HourlyYds { get; set; }

        public decimal? Variance { get; set; }

        public decimal? HP_RunTime { get; set; }

        public decimal? HP_DownTime { get; set; }

        [StringLength(50)]
        public string WorkOrderID { get; set; }

        public bool? MaintenanceStatus { get; set; }

        [StringLength(10)]
        public string Location { get; set; }

        public DateTime? Last_Timestamp { get; set; }
    }
}
