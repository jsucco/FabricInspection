namespace Inspection_mvc.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TabTemplate")]
    public partial class TabTemplate
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TabTemplate()
        {
            ButtonTemplates = new HashSet<ButtonTemplate>();
        }

        public int TabTemplateId { get; set; }

        public int TemplateId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public int TabNumber { get; set; }

        public bool ProductSpecs { get; set; }

        public DateTime? Updated { get; set; }

        public bool Active { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ButtonTemplate> ButtonTemplates { get; set; }

        public virtual TemplateName TemplateName { get; set; }
    }
}
