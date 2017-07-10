namespace Inspection_mvc.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class RollRM_Xref
    {
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string RMin { get; set; }

        [Required]
        [StringLength(30)]
        public string RMout { get; set; }

        public bool IDThread { get; set; }

        [StringLength(20)]
        public string IDThreadColor { get; set; }

        public decimal YardCoefficient { get; set; }
    }
}
