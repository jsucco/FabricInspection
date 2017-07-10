namespace Inspection_mvc.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SPC_ApplicationLog
    {
        public int id { get; set; }

        public DateTime date_added { get; set; }

        [StringLength(50)]
        public string type { get; set; }

        [Required]
        [StringLength(100)]
        public string Target { get; set; }

        public string Message { get; set; }

        [Required]
        [StringLength(150)]
        public string application_name { get; set; }

        public int? UserPK { get; set; }

        public int? Error_Number { get; set; }
    }
}
