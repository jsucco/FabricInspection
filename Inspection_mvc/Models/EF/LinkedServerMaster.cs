namespace Inspection_mvc.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LinkedServerMaster")]
    public partial class LinkedServerMaster
    {
        public int id { get; set; }

        [StringLength(30)]
        public string DSN_Type { get; set; }

        [StringLength(50)]
        public string DSN_Identifier { get; set; }

        public int LocationId { get; set; }

        [StringLength(30)]
        public string LinkedServerName { get; set; }

        public virtual LocationMaster LocationMaster { get; set; }
    }
}
