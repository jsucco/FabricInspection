namespace Inspection_mvc.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserActivityLog")]
    public partial class UserActivityLog
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UserActivityLog()
        {
            UserCrudActivityLogs = new HashSet<UserCrudActivityLog>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(100)]
        public string DBOrigin { get; set; }

        [Required]
        [StringLength(100)]
        public string UserID { get; set; }

        public DateTime EntryTimestamp { get; set; }

        [StringLength(100)]
        public string DeviceType { get; set; }

        [StringLength(20)]
        public string IPAddress { get; set; }

        [StringLength(10)]
        public string CID { get; set; }

        [Required]
        [StringLength(30)]
        public string ActivityType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserCrudActivityLog> UserCrudActivityLogs { get; set; }
    }
}
