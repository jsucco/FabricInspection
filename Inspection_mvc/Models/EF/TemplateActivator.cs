namespace Inspection_mvc.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TemplateActivator")]
    public partial class TemplateActivator
    {
        public int Id { get; set; }

        public int LocationMasterId { get; set; }

        public int TemplateId { get; set; }

        public bool Status { get; set; }

        public DateTime Inserted_Timestamp { get; set; }

        public virtual TemplateName TemplateName { get; set; }
    }
}
