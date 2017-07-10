namespace Inspection_mvc.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserCrudActivityLog")]
    public partial class UserCrudActivityLog
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string oper { get; set; }

        public int UserActivityLogId { get; set; }

        public DateTime Timestamp { get; set; }

        [Required]
        public string OriginalObject { get; set; }

        public string FinalObject { get; set; }

        [StringLength(50)]
        public string DataBase { get; set; }

        [StringLength(50)]
        public string Table { get; set; }

        public int PrimaryKeyTarget { get; set; }

        public virtual UserActivityLog UserActivityLog { get; set; }
    }
}
