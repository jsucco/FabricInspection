namespace Inspection_mvc.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ButtonTemplate")]
    public partial class ButtonTemplate
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ButtonTemplate()
        {
            DefectTimers = new HashSet<DefectTimer>();
        }

        public int id { get; set; }

        public int TabTemplateId { get; set; }

        public int ButtonId { get; set; }

        [StringLength(20)]
        public string DefectType { get; set; }

        public bool Hide { get; set; }

        public bool Timer { get; set; }

        public virtual ButtonLibrary ButtonLibrary { get; set; }

        public virtual TabTemplate TabTemplate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DefectTimer> DefectTimers { get; set; }
    }
}
