namespace Inspection_mvc.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RollInspectionSummary")]
    public partial class RollInspectionSummary
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RollInspectionSummary()
        {
            RollInspectionDetails = new HashSet<RollInspectionDetail>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(10)]
        public string LoomNo { get; set; }

        [Required]
        [StringLength(50)]
        public string RollNumber { get; set; }

        [StringLength(50)]
        public string Style { get; set; }

        public decimal Yards_Inspected { get; set; }

        public int DefectiveYards { get; set; }

        public decimal? DHY { get; set; }

        public DateTime RollStartTimestamp { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RollInspectionDetail> RollInspectionDetails { get; set; }
    }
}
