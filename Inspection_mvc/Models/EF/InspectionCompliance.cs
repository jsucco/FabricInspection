namespace Inspection_mvc.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("InspectionCompliance")]
    public partial class InspectionCompliance
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string WorkOrder { get; set; }

        [Required]
        [StringLength(30)]
        public string DataNo { get; set; }

        public string WADL01 { get; set; }

        [StringLength(50)]
        public string Location { get; set; }

        [StringLength(30)]
        public string WAUPMJ { get; set; }

        [StringLength(30)]
        public string WADCTO { get; set; }

        [StringLength(10)]
        public string WASRST { get; set; }

        [StringLength(20)]
        public string Prp_Code { get; set; }
    }
}
