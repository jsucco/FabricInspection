namespace Inspection_mvc.Models.EF
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class AprSTTcontext : DbContext
    {
        public AprSTTcontext()
            : base("name=AprSTTcontext")
        {
        }

        public virtual DbSet<OE_Operators> OE_Operators { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
