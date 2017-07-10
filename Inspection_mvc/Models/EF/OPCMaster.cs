namespace Inspection_mvc.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OPCMaster")]
    public partial class OPCMaster
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(20)]
        public string IPaddress { get; set; }

        public int LocationId { get; set; }

        public virtual LocationMaster LocationMaster { get; set; }
    }
}
