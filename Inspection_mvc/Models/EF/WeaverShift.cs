namespace Inspection_mvc.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class WeaverShift
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public WeaverShift()
        {
            WeaverProductions = new HashSet<WeaverProduction>();
        }

        public int Id { get; set; }

        public int Shift { get; set; }

        public DateTime Start { get; set; }

        public DateTime? Finish { get; set; }

        public string Comments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WeaverProduction> WeaverProductions { get; set; }
    }
}
