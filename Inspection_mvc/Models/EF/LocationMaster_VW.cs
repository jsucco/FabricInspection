namespace Inspection_mvc.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class LocationMaster_VW
    {
        [Key]
        [Column(Order = 0)]
        public int id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(30)]
        public string Name { get; set; }

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
    }
}
