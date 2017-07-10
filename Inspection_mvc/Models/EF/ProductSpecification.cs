namespace Inspection_mvc.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProductSpecification")]
    public partial class ProductSpecification
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProductSpecification()
        {
            SpecMeasurements = new HashSet<SpecMeasurement>();
        }

        [Key]
        public int SpecId { get; set; }

        public int? POM_Row { get; set; }

        public int? TabTemplateId { get; set; }

        [Required]
        [StringLength(30)]
        public string DataNo { get; set; }

        [Required]
        [StringLength(50)]
        public string ProductType { get; set; }

        [Required]
        public string Spec_Description { get; set; }

        public string HowTo { get; set; }

        public decimal value { get; set; }

        public decimal Upper_Spec_Value { get; set; }

        public decimal Lower_Spec_Value { get; set; }

        public bool GlobalSpec { get; set; }

        public string ME_SessionID { get; set; }

        [Required]
        [StringLength(15)]
        public string SpecSource { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SpecMeasurement> SpecMeasurements { get; set; }
    }
}
