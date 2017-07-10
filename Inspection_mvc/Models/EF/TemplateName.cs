namespace Inspection_mvc.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TemplateName")]
    public partial class TemplateName
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TemplateName()
        {
            DefectMasters = new HashSet<DefectMaster>();
            RollInspectionDetails = new HashSet<RollInspectionDetail>();
            TabTemplates = new HashSet<TabTemplate>();
            TemplateActivators = new HashSet<TemplateActivator>();
        }

        [Key]
        public int TemplateId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Owner { get; set; }

        public DateTime? DateCreated { get; set; }

        public bool? Active { get; set; }

        public int? LineTypeId { get; set; }

        [Required]
        [StringLength(10)]
        public string LineType { get; set; }

        public bool? Ins_GriegeBatch { get; set; }

        public bool? Ins_WorkOrderInspection { get; set; }

        public bool? Loc_STT { get; set; }

        public bool? Loc_CAR { get; set; }

        public bool? Loc_STJ { get; set; }

        public bool? Loc_SPA { get; set; }

        public bool? Loc_CDC { get; set; }

        public bool? Loc_LINYI { get; set; }

        public bool? Loc_PCE { get; set; }

        public bool? Loc_FSK { get; set; }

        public bool? Loc_FNL { get; set; }

        public bool? Loc_FPC { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DefectMaster> DefectMasters { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RollInspectionDetail> RollInspectionDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TabTemplate> TabTemplates { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TemplateActivator> TemplateActivators { get; set; }
    }
}
