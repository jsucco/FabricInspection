namespace Inspection_mvc.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OE_Operators
    {
        [Key]
        public int OEOP_ID { get; set; }

        public int OEOP_EID { get; set; }

        [Required]
        [StringLength(10)]
        public string OEOP_ADP { get; set; }

        [Required]
        [StringLength(50)]
        public string OEOP_Operator { get; set; }

        [Required]
        [StringLength(10)]
        public string OEOP_JobCode { get; set; }

        [Required]
        [StringLength(50)]
        public string OEOP_JobDesc { get; set; }

        [Required]
        [StringLength(10)]
        public string OEOP_Company { get; set; }

        [Required]
        [StringLength(50)]
        public string OEOP_CompanyDesc { get; set; }

        [Required]
        [StringLength(10)]
        public string OEOP_Department { get; set; }

        [Required]
        [StringLength(50)]
        public string OEOP_DepartmentDesc { get; set; }

        public int OEOP_SID { get; set; }

        [Required]
        [StringLength(50)]
        public string OEOP_Supervisor { get; set; }

        public bool OEOP_Active { get; set; }

        [Required]
        [StringLength(10)]
        public string OEOP_MCU { get; set; }
    }
}
