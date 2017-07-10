namespace Inspection_mvc.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OperatorShiftSummary")]
    public partial class OperatorShiftSummary
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string OEOP_ADP { get; set; }

        [StringLength(30)]
        public string LoomNumber { get; set; }

        public int? ThreadConfirm { get; set; }

        [StringLength(20)]
        public string ThreadColor { get; set; }

        [StringLength(50)]
        public string RMNumber { get; set; }

        public DateTime? InspectionStarted { get; set; }

        public DateTime? InspectionFinished { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(50)]
        public string OEOP_Operator { get; set; }

        [Key]
        [Column(Order = 3)]
        public DateTime Shift_Started { get; set; }

        public DateTime? Shift_Finished { get; set; }

        public string Comments { get; set; }

        public decimal? Yards_Pieces { get; set; }

        public int? HOLES { get; set; }

        public int? SOIL_OIL_GREASE { get; set; }

        public int? STARTMARK_BROKENPICK { get; set; }

        public int? ENDOUT { get; set; }

        public int? PILEPULL_MATTUP { get; set; }

        public int? MISDRAW { get; set; }

        public int? SLUB_THIN_KINKY_MISSING { get; set; }

        public int? TAILS { get; set; }

        public int? OTHER { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Weight1 { get; set; }

        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Weight2 { get; set; }

        [Key]
        [Column(Order = 6)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Weight3 { get; set; }

        [Key]
        [Column(Order = 7)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Weight4 { get; set; }

        [Key]
        [Column(Order = 8)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Weight5 { get; set; }

        [Key]
        [Column(Order = 9)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Weight6 { get; set; }

        [Key]
        [Column(Order = 10)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Weight7 { get; set; }

        [Key]
        [Column(Order = 11, TypeName = "numeric")]
        public decimal Weight8 { get; set; }

        [Key]
        [Column(Order = 12)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Weight9 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? total { get; set; }
    }
}
