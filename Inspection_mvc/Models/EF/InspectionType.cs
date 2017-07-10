namespace Inspection_mvc.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class InspectionType
    {
        public int id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(10)]
        public string Abreviation { get; set; }

        public string Description { get; set; }
    }
}
